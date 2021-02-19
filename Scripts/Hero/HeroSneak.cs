using System;
using UnityEngine;

class HeroSneak : MonoBehaviour
{

  #region Serialize Fields
  [SerializeField] float whistleRadius;
  [SerializeField] float fastWalkRadius;


  #endregion

  #region Private Fields
  private float defaultSpeed;
  private float slowSpeed;
  private KeyCode slowSpeedKey = KeyCode.LeftShift;
  private KeyCode whistleKey = KeyCode.E;
  private bool wasSlow;
  private float whistleCooldown = 15f;
  private bool isFast;


  #endregion

  #region Public Properties
  public bool IsFast => isFast;

  public float FastWalkRadius => fastWalkRadius;


  #endregion
  Hero hero;


  void Awake()
  {
    hero = GetComponent<Hero>();
    defaultSpeed = hero.Speed;
    slowSpeed = defaultSpeed * Constants.slowSpeedFactor;
    wasSlow = false;
    isFast = true;
  }



  void Update()
  {
    if (slowSpeedKey.key())
    {
      slowWalkOn();
      wasSlow = true;
    }
    else if (wasSlow)
    {
      slowWalkOff();
      wasSlow = false;
    }

    if (whistleKey.up())
      whistle();

    if (isFast)
      makeNoise(fastWalkRadius);

  }




  private float lastWhistle = 0;


  private void whistle()
  {
    if (lastWhistle == 0) lastWhistle = -whistleCooldown;

    if (Time.time - lastWhistle > whistleCooldown)
    {
      lastWhistle = Time.time;
      makeNoise(whistleRadius);
      AudioManager.i.Play("Whistle");
    }

  }


  private void makeNoise(float radius)
  {
    var pos = (Vector2)transform.position;
    var hits = Physics2D.OverlapCircleAll(pos, radius);
    var transforms = hits.map(hit => hit.transform);



    foreach (var hit in hits)
    {
      var zombie = hit.GetComponent<Zombie>();
      if (zombie == null) continue;

      var noise = UnityEngine.Random.insideUnitCircle * 1.5f;
      zombie.NoiseHeard(pos + noise);
    }

  }

  private void slowWalkOn() => slowWalk(true);

  private void slowWalkOff() => slowWalk(false);

  private void slowWalk(bool isOn)
  {
    isFast = !isOn;
    if (isOn)
      hero.Speed = slowSpeed;
    else
      hero.Speed = defaultSpeed;
    EventManager.i.onSlowSpeed(isOn);
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.magenta;
    Gizmos.DrawWireSphere(transform.position, whistleRadius);
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, fastWalkRadius);
  }








}