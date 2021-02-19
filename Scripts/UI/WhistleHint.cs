using UnityEngine;

class WhistleHint : MonoBehaviour
{

  private RectTransform rect;
  Vector3 defaultScale;
  bool dontOpenAgain = false;

  void Awake()
  {
    rect = GetComponent<RectTransform>();
    defaultScale = rect.localScale;
    rect.localScale = Vector3.zero;
    dontOpenAgain = false;
  }

  public void Open()
  {
    if (!dontOpenAgain)
      rect.localScale = defaultScale;
  }

  public void Close()
  {
    rect.localScale = Vector3.zero;
    dontOpenAgain = true;
  }

}