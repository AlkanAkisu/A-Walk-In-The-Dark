using UnityEngine;

class PatrolState : ZombieState
{
  public PatrolState(Zombie zombie) : base(zombie)
  {
  }

  public override void checkTransition()
  {
    bool isExposed = zombie.HandleSeen();
    if (isExposed)
      zombie.SetState(new DashState(zombie));
  }
  public override void changePatrol()
  {
    // change current patrol
    zombie.CurrentIndex = (zombie.CurrentIndex + 1) % zombie.Patrols.Length;
    zombie.Path.startPath(zombie.Patrols[zombie.CurrentIndex].position);

    var vec = (zombie.Patrols[zombie.CurrentIndex].position - zombie.transform.position).normalized;
    zombie.Rb.velocity = vec;
  }


  public override void onEnterState()
  {
    zombie.Path.startPath(zombie.Patrols[zombie.CurrentIndex].position);
    zombie.Speed = zombie.WalkSpeed;

  }

  public override void onStateUpdate()
  {
    zombie.Path.followPath();
    zombie.Anim.Play("Walk");
    base.onStateUpdate();
  }

  public override void onExitState()
  {
  }
}