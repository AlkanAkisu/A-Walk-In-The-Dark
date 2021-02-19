using UnityEngine;

class CameraFollow : MonoBehaviour
{

  Hero hero;
  [SerializeField] float smoothTime;
  [SerializeField] float minY, maxY, minX, maxX;
  [SerializeField] [MyBox.ReadOnly] Vector3 vel = Vector3.zero;


  void Start()
  {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();

  }


  void Update()
  {
    var vect = Vector3.zero;
    vect = Vector3.SmoothDamp(transform.position, hero.Position, ref vel, smoothTime);
    vect.y = Mathf.Clamp(vect.y, minY, maxY);
    vect.x = Mathf.Clamp(vect.x, minX, maxX);
    vect.z = -10f;
    transform.position = vect;


  }


}