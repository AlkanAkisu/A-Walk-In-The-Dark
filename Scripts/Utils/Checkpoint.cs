using UnityEngine;

class Checkpoint : MonoBehaviour
{
  [SerializeField] int index;

  public int Index => index;

  void OnTriggerEnter2D(Collider2D other)
  {
    var hero = other.transform.GetComponent<Hero>();
    if (hero != null)
      CheckpointManager.i.checkPointPassed(index);
  }




}