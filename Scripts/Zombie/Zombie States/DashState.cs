using UnityEngine;

class DashState : ZombieState
{
  public DashState(Zombie zombie) : base(zombie)
  {
  }

  public override void checkTransition()
  {
    if (Time.time - zombie.DashStartTime > zombie.DashSeconds)
    {
      Debug.Log(zombie.DashStartTime);
      EventManager.i.onZombieStartApproach();
      zombie.SetState(new ApproachState(zombie));

    }
  }


  public override void onEnterState()
  {
    zombie.DashStartTime = Time.time;
    Debug.Log(Time.time);
    zombie.Path.cancelPath();

    zombie.ZombieAudio.onSeen();
    base.onStateUpdate();
  }

  public override void onStateUpdate()
  {
    zombie.Anim.Play("Dash");

    float ratio = Mathf.Clamp01((Time.time - zombie.DashStartTime) / zombie.DashSeconds);
    var curve = Mathf.Clamp01(zombie.DashingCurve.Evaluate(ratio));
    zombie.Speed = Mathf.Lerp(zombie.WalkSpeed, zombie.DashSpeed, curve);

    var vel = (zombie.Hero.Position - zombie.transform.position).normalized * zombie.Speed * Time.deltaTime;
    zombie.Rb.velocity = vel;
  }
  public override void onExitState()
  {
  }



}