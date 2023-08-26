using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    void Awake()
    {
        Instance = this;
    }
    public float p_TimeScale = 1;
    public float TimeScale = 1;
    public void SetTimeScale(float scale)
    {
        TimeScale = scale;
        Time.timeScale = TimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    void Update()
    {
        if(p_TimeScale != TimeScale)
        {
            p_TimeScale = TimeScale;
            SetTimeScale(TimeScale);
        }
    }

}
