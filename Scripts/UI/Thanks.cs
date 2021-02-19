using UnityEngine;

class Thanks : MonoBehaviour
{

  [SerializeField] RectTransform sleepText;
  [SerializeField] RectTransform thanksPanel;

  void Start()
  {

    StartCoroutine(transition());
    sleepText.localScale = Vector3.zero;
    thanksPanel.localScale = Vector3.zero;

  }

  System.Collections.IEnumerator transition()
  {
    var seconds = 2f;
    var scale = 0f;
    var scaleVector = Vector3.zero;
    while (scale < 1)
    {
      scale += Time.deltaTime / seconds;
      sleepText.localScale = new Vector3(scale, scale, 0f);
      yield return null;
    }
    yield return new WaitForSeconds(1.5f);
    scale = 1f;
    while (scale > 0f)
    {
      scale -= Time.deltaTime / seconds;
      sleepText.localScale = new Vector3(scale, scale, 0f);
      yield return null;
    }
    scale = 0f;
    while (scale < 1f)
    {
      scale += Time.deltaTime / seconds;
      thanksPanel.localScale = new Vector3(scale, scale, 0f);
      yield return null;
    }


  }






}