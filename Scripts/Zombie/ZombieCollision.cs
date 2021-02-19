using UnityEngine;

class ZombieCollision : MonoBehaviour
{

  Zombie zombie;
  void Start()
  {
    zombie = GetComponent<Zombie>();
  }


  void OnCollisionEnter2D(Collision2D other)
  {

    bool isHero = other.transform.CompareTag("Hero");
    if (!isHero) return;


    bool isAttacking = zombie.State is DashState || zombie.State is ApproachState;

    if (isAttacking)
      EventManager.i.onHeroDied();
  }
}