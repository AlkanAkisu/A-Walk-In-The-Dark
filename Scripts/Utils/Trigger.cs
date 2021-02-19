using UnityEngine;
using UnityEngine.Events;

class Trigger : MonoBehaviour
{
  [SerializeField] UnityEvent onEnterEvent, onExitEvent, onStayEvent;


  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Hero"))
      onEnterEvent?.Invoke();
  }
  void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Hero"))
      onExitEvent?.Invoke();
  }
  void OnTriggerStay2D(Collider2D other)
  {
    if (other.CompareTag("Hero"))
      onStayEvent?.Invoke();
  }




}