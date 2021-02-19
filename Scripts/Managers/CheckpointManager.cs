using System;
using System.Collections.Generic;
using UnityEngine;


class CheckpointManager : MonoBehaviour
{
  public static CheckpointManager i { get; private set; }
  [SerializeField] [MyBox.ReadOnly] int currentCheckpoint = 1;
  Hero hero;
  [MyBox.ReadOnly] [SerializeField] Dictionary<int, Transform> checkpoints;

  void Awake()
  {
    i = this;
    checkpoints = new Dictionary<int, Transform>();
  }
  void Start()
  {
    var objects = GameObject.FindGameObjectsWithTag("Checkpoint");
    checkpoints = objects.reduce(
      checkpoint => new KeyValuePair<int, Transform>(checkpoint.GetComponent<Checkpoint>().Index, checkpoint.transform)
    );

    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    EventManager.i.onHeroDiedEvent += heroDied;

  }

  public void checkPointPassed(int index)
  {
    Debug.Log(index);
    currentCheckpoint = Mathf.Max(currentCheckpoint, index);
  }

  public void heroDied()
  {
    hero.HeroDied(getCheckpoint(currentCheckpoint).position);
  }

  private Transform getCheckpoint(int index)
  {
    return checkpoints[index];
  }
}