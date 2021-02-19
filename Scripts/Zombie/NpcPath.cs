using UnityEngine;
using Pathfinding;
using System;

public delegate void VoidEvent();
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rigidbody2D))]
public class NpcPath : MonoBehaviour
{
  /* #region  Fields */
  [SerializeField] float speed;

  private Seeker _seeker;
  private Rigidbody2D rb;

  private Path _path;

  private int currentWaypoint;
  private float nextWaypointDistance = 0.2f;
  private Vector2 dest;
  public event VoidEvent onPathFinished;
  public Path Path { get => _path; private set => _path = value; }
  public float Speed { get => speed; set => speed = value; }

  /* #endregion */

  void Awake()
  {
    currentWaypoint = 0;

    _path = null;
    _seeker = GetComponent<Seeker>();
    rb = GetComponent<Rigidbody2D>();

  }




  public void startPath(Vector2 dest)
  {
    this.dest = dest;
    _path = null;
    currentWaypoint = 0;
    cancelPath();
    _seeker.StartPath(transform.position, dest, onPathCompleted);
  }
  public void cancelPath()
  {
    _path = null;
    currentWaypoint = 0;
  }
  public void onPathCompleted(Path p)
  {
    _path = p;
  }

  public void followPath()
  {
    if (_path == null) return;

    float finalDist = ((Vector2)transform.position - this.dest).sqrMagnitude;
    if (finalDist < nextWaypointDistance || currentWaypoint >= _path.vectorPath.Count)
    {
      pathFinished();
      return;
    }

    float distance = (transform.position - _path.vectorPath[currentWaypoint]).sqrMagnitude;


    Vector2 direction = (_path.vectorPath[currentWaypoint] - transform.position).normalized;

    rb.velocity = direction * speed * Time.fixedDeltaTime;

    if (distance < nextWaypointDistance)
      currentWaypoint++;

  }

  public void pathFinished()
  {
    rb.velocity = Vector2.zero;
    _path = null;
    currentWaypoint = 0;
    onPathFinished?.Invoke();
  }

}
