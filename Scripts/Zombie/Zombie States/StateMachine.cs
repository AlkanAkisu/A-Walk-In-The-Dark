using UnityEngine;

class StateMachine : MonoBehaviour
{
  public ZombieState State;

  public void SetState(ZombieState state)
  {
    State?.onExitState();

    var stateStr = state.GetType().Name;
    if (State != null)
      Debug.Log($"State Changed. New State -> <b>{stateStr}</b>");

    State = state;
    State.onEnterState();
  }
}


