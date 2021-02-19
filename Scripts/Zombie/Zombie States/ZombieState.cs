using UnityEngine;

abstract class ZombieState
{
  public Zombie zombie;
  public ZombieState(Zombie zombie)
  {
    this.zombie = zombie;
  }

  public abstract void onEnterState();
  public virtual void onStateUpdate()
  {
    checkTransition();
  }

  public virtual void changePatrol()
  {
  
  }

  public abstract void onExitState();
  public abstract void checkTransition();
}