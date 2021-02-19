using System;
using UnityEngine;

class ApproachState : ZombieState
{

  public ApproachState(Zombie zombie) : base(zombie)
  {
  }

  public override void checkTransition()
  {
    bool minSecondsPast = Time.time - zombie.ApproachStartTime > zombie.MinApproachSeconds;
    bool leftArea = (zombie.Hero.Position - zombie.transform.position).magnitude > zombie.ApproachLeaveDistance;

    if (minSecondsPast && leftArea)
    {
      zombie.SetState(new PatrolState(zombie));
    }
  }


  public override void onEnterState()
  {
    zombie.ApproachStartTime = Time.time;
    zombie.InvokeFollowHero(followHero, 0.5f);

  }

  private void followHero() => zombie.Path.startPath(zombie.Hero.Position);


  public override void onStateUpdate()
  {
    zombie.Path.followPath();
    base.onStateUpdate();
  }

  public override void onExitState()
  {
    zombie.CancelFollowHero();
    EventManager.i.onZombieFinishApproach();
  }


}