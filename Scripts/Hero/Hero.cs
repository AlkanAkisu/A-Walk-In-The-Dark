using System;
using UnityEngine;

class Hero : MonoBehaviour
{
  #region Serialize Fields
  [SerializeField] float speed;
  [SerializeField] float maxSpeed;
  [SerializeField] float smoothTime;
  [SerializeField] float walkSoundTime;
  [SerializeField] private Animator anim;
  [SerializeField] private float fastPlaybackSpeed;
  [SerializeField] private float slowPlaybackSpeed;

  #endregion

  #region Private Fields
  private bool isDetected;
  private HeroSneak heroSneak;
  private Rigidbody2D rb;
  private SpriteRenderer sp;


  #endregion

  #region Public Properties
  public bool IsDetected => isDetected;
  public float Speed { get => speed; set => speed = value; }
  public bool CanMove { get; set; }
  public Vector3 Position { get;  set; }

  #endregion

  void Awake()
  {
    heroSneak = GetComponent<HeroSneak>();
    CanMove = true;
    rb = GetComponent<Rigidbody2D>();
    sp = GetComponentInChildren<SpriteRenderer>();
  }


  float x = 0, y = 0;


  void Update()
  {
    if (CanMove)
    {
      x = Input.GetAxis("Horizontal");
      y = Input.GetAxis("Vertical");
      Move(x, y);
    }
    Position = transform.position;

  }
  void FixedUpdate()
  {
    if (CanMove)
      Move(x, y);
  }

  private void Move(float horizontal, float vertical)
  {

    Vector3 target = (new Vector2(horizontal, vertical)).normalized * speed * Time.deltaTime;
    Vector2 vel = Vector2.zero;

    Vector2.SmoothDamp(transform.position, transform.position + target, ref vel, smoothTime, maxSpeed);

    rb.velocity = vel;

    if (heroSneak.IsFast && vel.sqrMagnitude > 0.4f)
      EventManager.i.onFastWalk();
    else
      EventManager.i.onFastWalkCanceled();
    handleSoundAndAnim();


    if (rb.velocity.x > 0.3f)
      sp.flipX = false;
    else if (rb.velocity.x < -0.3f)
      sp.flipX = true;


  }

  private bool isMoving => rb.velocity.sqrMagnitude > 0.3f;
  private bool isFast => heroSneak.IsFast;
  private void handleSoundAndAnim()
  {
    if (isMoving && isFast)
      playWalkSound();
    else
      cancelWalkSound();

    if (isMoving)
      playWalkAnim(isFast);
    else
      anim.Play("Idle");
  }

  private void playWalkAnim(bool isFast)
  {
    anim.speed = isFast ? fastPlaybackSpeed : slowPlaybackSpeed;
    anim.Play("Walk");
  }

  float lastTimeWalkPlayed = 0;
  private void playWalkSound()
  {
    if (lastTimeWalkPlayed != 0 && Time.time - lastTimeWalkPlayed <= walkSoundTime)
      return;

    AudioManager.i.Play("Walk");
    lastTimeWalkPlayed = Time.time;
  }
  private void cancelWalkSound()
  {
    //lastTimeWalkPlayed = 0;
  }

  public void HeroDied(Vector2 position){
    transform.position = position;
  }



  public void Seen(bool isSeen)
  {

  }




}