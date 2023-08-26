using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
  public UnityEvent trigEvent;
  public UnityEvent trigEvent2;
  void OnTriggerEnter2D(Collider2D col)
  {
    trigEvent.Invoke();
    print("yes");
  }
  void OnTriggerExit2D(){
    trigEvent2.Invoke();
    print("no");
  }
}
