using UnityEngine;
using UnityEngine.UI;

class LightSlider : MonoBehaviour
{

  Vector3 defaultScale;
  [SerializeField] Slider slider;
  [SerializeField] Vector3 offset;
  private Camera _cam;
  private Hero hero;

  void Awake()
  {
    defaultScale = transform.localScale;
    transform.localScale = Vector3.zero;
    _cam = Camera.main;
  }

  void Start()
  {
    EventManager.i.onLightEvent += onLight;
    EventManager.i.onTurningOnEvent += onSliderValueChanged;
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }
  void Update()
  {
    if (transform.localScale == defaultScale)
      handlePos();
  }

  public void onSliderValueChanged(float value)
  {
    slider.value = value;
  }
  public void onLight(bool isOn)
  {

    transform.localScale = isOn ? defaultScale : Vector3.zero;

    slider.value = 0;
  }

  private void handlePos()
  {
    transform.position = _cam.WorldToScreenPoint(hero.Position + offset);
  }

}