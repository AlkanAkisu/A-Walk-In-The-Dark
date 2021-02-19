using UnityEngine;

class NoiseState : ZombieState
{

  public NoiseState(Zombie zombie) : base(zombie)
  {

  }



  public override void onEnterState()
  {
    zombie.Path.cancelPath();
    Debug.DrawLine(zombie.Hero.Position, zombie.CheckingNoisePosition, Color.yellow, 3f);
    zombie.Path.startPath(zombie.CheckingNoisePosition);

    zombie.Speed = zombie.NoiseCheckSpeed;


  }
  public override void onStateUpdate()
  {
    zombie.Path.followPath();
    zombie.Anim.Play("Walk");
    base.onStateUpdate();
  }
  public override void checkTransition()
  {
    bool isExposed = zombie.HandleSeen();
    if (isExposed)
      zombie.SetState(new DashState(zombie));
  }
  public override void onExitState()
  {


  }
}
