using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyBox;
using UnityEditor;

class Zombie : StateMachine
{

	#region Serialize Fields
	[Foldout("Patrol", true)]
	[SerializeField] Transform patrolsTransform;

	[Foldout("Approach", true)]
	[SerializeField] float approachLeaveDistance;
	[SerializeField] float minApproachSeconds;

	[Foldout("Dash", true)]
	[SerializeField] AnimationCurve dashingCurve;
	[SerializeField] float dashSpeed, walkSpeed, dashSeconds;
	[Foldout("Sneak", true)]
	[SerializeField] float seenRadius, seenDegree, noiseCheckSpeed;
	[Foldout("Other", true)]
	[SerializeField] Rigidbody2D rb;
	[SerializeField] Animator anim;

	#endregion

	#region Private Fields
	private Transform[] patrols;
	private Transform currentPatrol;




	[ReadOnly] [SerializeField] int currentIndex = 0;
	[ReadOnly] [SerializeField] float angle;



	#endregion

	#region Public Properties

	public NpcPath Path { get; private set; }
	public float Speed { get; set; }
	public Animator Anim => anim;
	public float NoiseCheckSpeed => noiseCheckSpeed;
	public float WalkSpeed => walkSpeed;
	public float SeenRadius => seenRadius;
	public float SeenDegree => seenDegree;
	public float NoiseCheckSpeed1 => noiseCheckSpeed;
	public float DashSpeed => dashSpeed;
	public float DashSeconds => dashSeconds;
	public Transform[] Patrols => patrols;
	public float MinApproachSeconds => minApproachSeconds;

	public float ApproachLeaveDistance => approachLeaveDistance;
	public Rigidbody2D Rb => rb;
	public AnimationCurve DashingCurve => dashingCurve;
	public int CurrentIndex { get => currentIndex; set => currentIndex = value; }
	public float DashStartTime { get; set; }
	public Hero Hero { get; private set; }
	public ZombieAudio ZombieAudio { get; private set; }

	public float ApproachStartTime { get; set; }
	public Vector2 CheckingNoisePosition { get; private set; }


	#endregion

	void Awake()
	{
		Path = GetComponent<NpcPath>();
		Path.onPathFinished += onPathFinished;
		rb = GetComponent<Rigidbody2D>();


		ZombieAudio = GetComponent<ZombieAudio>();


		initPatrols();

	}
	private void initPatrols()
	{
		patrols = new Transform[patrolsTransform.childCount];

		for (int i = 0; i < patrolsTransform.childCount; i++)
		{
			patrols[i] = patrolsTransform.GetChild(i);
		}

	}



	void OnDisable() => Path.onPathFinished -= onPathFinished;


	void Start()
	{
		Hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
		EventManager.i.onHeroDiedEvent += zombieReset;
		zombieReset();
		Utils.DrawArrowDebug(transform.position, patrols[currentIndex].position, 5, Color.red, 2);
	}

	private void zombieReset()
	{
		transform.position = patrols[0].position;
		SetState(new PatrolState(this));
		State.changePatrol();
	}
	void Update()
	{
		State.onStateUpdate();

		Path.Speed = Speed;

		handleTurn();



	}

	public void NoiseHeard(Vector2 position)
	{

		CheckingNoisePosition = position;
		if (State is PatrolState)
			SetState(new NoiseState(this));
	}

	private void onPathFinished()
	{
		// when path finished change patrol location
		if (State is PatrolState)
			State.changePatrol();
		else if (State is NoiseState)
		{
			State.onExitState();
			SetState(new PatrolState(this));

		}
	}

	[ButtonMethod]
	private void toApproach() => SetState(new ApproachState(this));
	[ButtonMethod]
	private void toPatrol() => SetState(new PatrolState(this));
	[ButtonMethod]
	private void toDash() => SetState(new DashState(this));

	private void handleTurn() => transform.localScale = new Vector3(-Mathf.Sign(rb.velocity.x), 1f, 1f);


	public bool HandleSeen()
	{
		var hits = Physics2D.OverlapCircleAll(transform.position, seenRadius);

		Predicate<Collider2D> isHero = hit => hit.GetComponent<Hero>() != null;
		Transform hero = Array.Find(hits, isHero)?.transform;

		if (hero == null) return false;


		if (getAngleToHero() > seenDegree) return false;
		var rayHits = Physics2D.RaycastAll(transform.position, toHeroVector(), toHeroVector().magnitude);

		return !isCoverOnWay(rayHits);

	}

	private bool isCoverOnWay(RaycastHit2D[] rayHits)
	{
		Action<Transform> drawLine = hit => Debug.DrawLine(transform.position, hit.position, Color.red);

		bool coverFound = false;
		Transform cover = null;
		foreach (var hit in rayHits)
		{
			cover = hit.transform;
			coverFound = hit.transform.CompareTag("Cover");
			if (coverFound) break;
		}

		if (coverFound)
			drawLine(cover);

		return coverFound;
	}

	private float getAngleToHero() => Vector2.Angle(rb.velocity, toHeroVector());

	private Vector2 toHeroVector() => (Hero.Position - transform.position);

	public void InvokeFollowHero(Action followHero, float seconds)
	{
		Path.cancelPath();
		followHero();
		Invoke(nameof(InvokeFollowHero), seconds);
	}
	public void CancelFollowHero() => CancelInvoke(nameof(InvokeFollowHero));


	void OnDrawGizmos()
	{
		if (patrols == null)
			initPatrols();

		Handles.DrawDottedLine(transform.position, transform.position + transform.up * 10f, 4f);

		Gizmos.color = Color.white;
		Vector3 to, from = transform.position;
		if (Application.isPlaying)
		{
			to = rb.velocity.normalized;
		}
		else
		{

			to = (patrols[1].position - patrols[0].position).normalized;
		}

		Utils.DrawCone(from, to, seenDegree * 2, seenRadius, true);

		Gizmos.color = Color.red;


		var length = patrols.Length;
		Color[] colors = { Color.red, Color.blue, Color.magenta, Color.green, Color.yellow };
		for (int i = 0; i < length; i++)
		{
			Color color;
			if (length == 2 && i == 1)
			{
				from = patrols[i].position;
				to = patrols[(i + 1) % length].position;
				Vector3 perp = Vector2.Perpendicular(to - from).normalized;
				float diff = 0.3f;
				color = colors[i % colors.Length];
				Utils.DrawArrow(from + perp * diff, to + perp * diff, 6, color);

				break;
			}
			from = patrols[i].position;
			to = patrols[(i + 1) % length].position;
			color = colors[i % colors.Length];
			Utils.DrawArrow(from, to, 6, color);

		}

		


	}





}