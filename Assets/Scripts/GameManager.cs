using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public GameObject player;
  public static GameManager Instance;
  int GameState = 0;
  public Transform CheckPoint;
  void Awake()
  {
    Instance = this;
  }

  void Update()
  {
    if(Input.GetKeyDown(KeyCode.Escape))
    {
      TogglePause();
    }
  }

  public void HandleDeath()
  {
    SetGameState(1);
    TimeManager.Instance?.SetTimeScale(0.05f);
  }

  public void ResetToLastCheckPoint()
  {
    TimeManager.Instance?.SetTimeScale(1);
    player.transform.position = CheckPoint.position;
    SetGameState(0);
  }

  public void SetCheckPoint(Transform point)
  {
    CheckPoint = point;
  }

  public void TogglePause()
  {
    if(GetGameState() == 0)
    {
      TimeManager.Instance?.SetTimeScale(0.05f);
      SetGameState(2);
    }
    else if(GetGameState() == 2)
    {
      TimeManager.Instance?.SetTimeScale(1);
      SetGameState(0);
    }
  }

  public int GetGameState()//0 means active , 1 means player died , 2 pause
  {
    return GameState;
  }
  public void SetGameState(int state)
  {
    GameState = state;
  }
}
