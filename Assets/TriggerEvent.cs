using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
  public UnityEvent trigEvent;
  void OnTriggerEnter2D(Collider2D col)
  {
    trigEvent.Invoke();
  }
}
