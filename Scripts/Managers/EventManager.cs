using System;
using UnityEngine;

class EventManager : MonoBehaviour
{
  #region Serialize Fields


  #endregion

  #region Private Fields


  #endregion

  #region Public Properties
  public static EventManager i { get; private set; }

  #endregion

  void Awake()
  {
    i = this;
  }


  public event Action onZombieStartApproachEvent;
  public void onZombieStartApproach() => onZombieStartApproachEvent?.Invoke();
  public event Action onZombieFinishApproachEvent;
  public void onZombieFinishApproach() => onZombieFinishApproachEvent?.Invoke();
  public event Action<bool> onLightEvent;
  public void onLight(bool dead) => onLightEvent?.Invoke(dead);
  public event Action<float> onTurningOnEvent;
  public void onTurningOn(float turnRate) => onTurningOnEvent?.Invoke(turnRate);
  public event Action<bool> onSlowSpeedEvent;
  public void onSlowSpeed(bool isOn) => onSlowSpeedEvent?.Invoke(isOn);

  public event Action onFastWalkEvent;
  public void onFastWalk() => onFastWalkEvent?.Invoke();

  public event Action onFastWalkCanceledEvent;
  public void onFastWalkCanceled() => onFastWalkCanceledEvent?.Invoke();

  public event Action onHeroDiedEvent;
  [MyBox.ButtonMethod]
  public void onHeroDied() => onHeroDiedEvent?.Invoke();
}