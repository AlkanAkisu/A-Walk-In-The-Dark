using UnityEngine;
using UnityEngine.SceneManagement;

class StartText : MonoBehaviour
{


  [SerializeField] RectTransform text;

  void Start()
  {

    StartCoroutine(transition());
    text.localScale = Vector3.zero;

  }

  System.Collections.IEnumerator transition()
  {
    var seconds = 2f;
    var scale = 0f;
    var scaleVector = Vector3.zero;
    while (scale < 1)
    {
      scale += Time.deltaTime / seconds;
      text.localScale = new Vector3(scale, scale, 0f);
      yield return null;
    }
    yield return new WaitForSeconds(3f);
    scale = 1f;
    while (scale > 0f)
    {
      scale -= Time.deltaTime / seconds;
      text.localScale = new Vector3(scale, scale, 0f);
      yield return null;
    }
    SceneManager.LoadScene(4, LoadSceneMode.Single);


  }






}
