using UnityEngine;
using UnityEngine.SceneManagement;

class TitleScreen : MonoBehaviour
{
  //todo
  public void onStart()
  {
    //goto howto 2
    SceneManager.LoadScene(2, LoadSceneMode.Single);
  }
  public void onCredits()
  {
    //goto credit 1
    SceneManager.LoadScene(1, LoadSceneMode.Single);
  }
  public void onExit()
  {
    //exit game
    Application.Quit();
  }
  public void onCreditsBack()
  {
    //goto start 0
    SceneManager.LoadScene(0, LoadSceneMode.Single);
  }
  public void StartGame()
  {
    //goto game 3
    SceneManager.LoadScene(3, LoadSceneMode.Single);

  }

}