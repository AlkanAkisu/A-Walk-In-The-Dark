
using UnityEngine;

class SoundBarrier : MonoBehaviour
{
  RectTransform rect;
  private Camera _cam;
  [SerializeField] float speed;
  [SerializeField] float startRatio = 0.3f;
  [MyBox.ReadOnly] [SerializeField] float radius;
  [MyBox.ReadOnly] [SerializeField] float ratio;
  [MyBox.ReadOnly] [SerializeField] float defaultSize;
  [MyBox.ReadOnly] [SerializeField] float worldWidth;
  Hero hero;
  bool isFastWalking = false;

  void Awake()
  {
    rect = GetComponent<RectTransform>();
    _cam = Camera.main;
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    var sneak = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroSneak>();
    radius = sneak.FastWalkRadius;
    worldWidth = (_cam.aspect * _cam.orthographicSize) * 2;
    ratio = radius / worldWidth;
    defaultSize = ratio * Screen.width;
  }
  void Start()
  {
    EventManager.i.onFastWalkEvent += onFastWalking;
    EventManager.i.onFastWalkCanceledEvent += onFastWalkFinished;
  }
  void OnDisable()
  {
    EventManager.i.onFastWalkEvent -= onFastWalking;
    EventManager.i.onFastWalkCanceledEvent -= onFastWalkFinished;
  }

  void Update()
  {
    if (isFastWalking)
    {
      transform.position = _cam.WorldToScreenPoint(hero.Position);
      transform.localScale = Vector3.one;
    }
    else
      transform.localScale = Vector3.zero;


  }

  private float animSize = 0;
  bool increasing = true;
  private void onFastWalking()
  {
    isFastWalking = true;
    float max = defaultSize;
    float min = defaultSize * startRatio;
    if (increasing)
      animSize = Mathf.Clamp(animSize + speed * Time.deltaTime, min, max);
    else
      animSize = Mathf.Clamp(animSize - speed * Time.deltaTime, min, max);

    if (animSize >= max || animSize <= min)
      increasing = !increasing;
    rect.sizeDelta = new Vector2(animSize * 2, animSize * 2);

  }
  private void onFastWalkFinished()
  {
    isFastWalking = false;
  }
}