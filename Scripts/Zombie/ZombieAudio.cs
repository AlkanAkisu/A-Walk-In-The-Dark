using UnityEngine;

class ZombieAudio : MonoBehaviour
{
  #region Serialize Fields
  [SerializeField] AudioSource onSeenAudio, onNoiseAudio, idle;

  #endregion

  #region Private Fields
  Zombie zombie;
  float idleVolume;
  #endregion

  #region Public Properties


  #endregion

  void Awake()
  {
    zombie = GetComponent<Zombie>();
    idleVolume = idle.volume;
  }
  [MyBox.ButtonMethod]
  public void onSeen()
  {
    stopIdle();
    Invoke(nameof(playIdle), 10f);
    onSeenAudio.Play();
  }

  [MyBox.ButtonMethod]
  public void onHeardNoise()
  {
    stopIdle();
    Invoke(nameof(playIdle), 5f);
    onNoiseAudio.Play();
  }

  private void stopIdle() => idle.volume = 0;
  private void playIdle() => idle.volume = 0.5f;


}