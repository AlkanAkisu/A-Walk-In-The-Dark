using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



class Light : MonoBehaviour
{

  #region Serialize Fields
  [SerializeField] float _maxRadius = 11f;
  [SerializeField] Light2D _light2d;
  [SerializeField] float _currentRadius = 6.0f;
  //[SerializeField] [Range(90f, -90f)] float degree = 0f;
  [SerializeField] [MyBox.ReadOnly] private float turningRate;
  [SerializeField] AnimationCurve _lightDecreaseCurve;
  [SerializeField] float LightSoundTime;
  [SerializeField] private float timeNeedsToDie = 100f;

  #endregion

  #region Private Fields
  private float lastTimeLightDie;
  private bool _isDead;
  private float _decreaseRate = 0.3f;
  private KeyCode lightKey = KeyCode.Q;
  private Camera _cam;
  private float _offset = 90f;

  [MyBox.ReadOnly] [SerializeField] private float timeRemaining;


  #endregion

  #region Public Properties
  public bool IsDead => _isDead;

  public float TurningRate => turningRate;


  #endregion

  void Awake()
  {
    _isDead = false;
    turningRate = 1f;
    lastTimeLightDie = 0f;
    _currentRadius = _maxRadius;
    _cam = Camera.main;
  }

  void Start()
  {
    EventManager.i.onHeroDiedEvent += onHeroDied;
  }
  void OnDisable()
  {
    EventManager.i.onHeroDiedEvent -= onHeroDied;
  }

  void Update()
  {
    if (!_isDead)
      handleLight();
    else
      handleTurningOn();

    var degree = controlLight(transform.position, _cam);
    turnLight(degree + _offset);

  }

  private void turnLight(float degree) => transform.eulerAngles = Vector3.forward * degree;
  private void handleLight()
  {
    _currentRadius = handleRadius();
    if (_currentRadius <= 0f)
      lightDied();
    setRadius(_currentRadius);
  }

  private void setRadius(float radius) => _light2d.pointLightOuterRadius = radius;

  private float handleRadius()
  {
    if (lastTimeLightDie == 0f)
      lastTimeLightDie = Time.time;

    float time = Mathf.Clamp01((Time.time - lastTimeLightDie) / timeNeedsToDie);

    //for log
    timeRemaining = timeNeedsToDie - (Time.time - lastTimeLightDie);

    float radiusRate = _lightDecreaseCurve.Evaluate(time);
    return Mathf.Lerp(0f, _maxRadius, radiusRate);
  }

  private void handleTurningOn()
  {
    if (lightKey.down())
    {
      increaseTurnOn();
      playLightSound();
    }

    turningRate -= Time.deltaTime * _decreaseRate;
    turningRate = Mathf.Clamp01(turningRate);
    setRadius(_maxRadius * turningRate * 0.6f);

    EventManager.i.onTurningOn(turningRate);

    if (turningRate >= 1f)
      lightGetBackToLife();

  }

  private void increaseTurnOn() => turningRate += 1f / Constants.pressTimeForTurningOn;


  [MyBox.ButtonMethod]
  private void lightDied()
  {
    Debug.Log("Light Died");
    _currentRadius = 0f;
    setRadius(_currentRadius);
    _isDead = true;
    turningRate = 0f;
    EventManager.i.onLight(_isDead);
  }
  private void lightGetBackToLife()
  {
    lastTimeLightDie = Time.time;
    _isDead = false;
    turningRate = 1f;
    EventManager.i.onLight(_isDead);
  }

  private void onHeroDied()
  {
    lightGetBackToLife();
  }


  private float controlLight(Vector2 light, Camera cam)
  {
    var point = cam.ScreenToWorldPoint(Input.mousePosition);
    var pos = light;
    var diff = (Vector2)point - pos;
    var degree = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
    return -degree;


  }

  // ** Light Audio **
  float lastTimeLightPlayed = 0;
  private void playLightSound()
  {
    if (lastTimeLightPlayed != 0 && Time.time - lastTimeLightPlayed <= LightSoundTime)
      return;

    AudioManager.i.Play("Light Turning");
    lastTimeLightPlayed = Time.time;
  }



}