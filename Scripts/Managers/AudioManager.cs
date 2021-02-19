using UnityEngine;
using UnityEngine.Audio;

class AudioManager : MonoBehaviour
{

  [SerializeField] Sound[] sounds;
  public static AudioManager i { get; private set; }
  void Awake()
  {
    i = this;
  }

  void Start()
  {
    foreach (var sfx in sounds)
    {
      if (sfx.clip == null) continue;
      var source = gameObject.AddComponent<AudioSource>();
      source.volume = sfx.volume;
      source.pitch = sfx.pitch;
      sfx.source = source;
      source.clip = sfx.clip;
    }
  }

  public void Play(string name)
  {
    var sound = System.Array.Find(sounds, sfx => sfx.name == name);
    sound.source.Play();

  }

	[SerializeField]int i;
	[Button]private void Play()=> Play(sounds[i]);


}

[System.Serializable]
class Sound
{
  public string name;
  public AudioClip clip;
  public AudioSource source;
  [Range(0f, 1f)]public float volume;
  [Range(0.1f, 1.5f)]public float pitch=0.7f;

}