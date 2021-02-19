using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class Teddy : MonoBehaviour
{

  [SerializeField] Image panel;

  [MyBox.ButtonMethod]
  private void debug()
  {
    StartCoroutine(transition());
  }


  void OnTriggerEnter2D(Collider2D other)
  {
    GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().CanMove = false;
    StartCoroutine(transition());
  }
  System.Collections.IEnumerator transition()
  {
    var alpha = 0f;
    var seconds = 2f;
    var color = panel.color;
    var t = 0f;
    while (t < 1)
    {
      t += Time.deltaTime / seconds;
      alpha = Mathf.Lerp(0, 1, t);
      color = panel.color;
      color.a = alpha;
      panel.color = color;
      yield return null;
    }
    SceneManager.LoadScene(4, LoadSceneMode.Single);
  }

}