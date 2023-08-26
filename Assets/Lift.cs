using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public LeanTweenType easeType;
    public AnimationCurve curve;
    public Transform[] Points;
    public GameObject Platform;

    public float TransitionTime = 5;

    public int PrevPointIndex = 0;
    public int PointIndex = 0;

    public void MoveToPoint(int index)
    {
        if(easeType == LeanTweenType.animationCurve)
        {
            LeanTween.move(Platform,Points[index].position,5).setEase(curve);
        }
        else
        {
            LeanTween.move(Platform,Points[index].position,5).setEase(easeType);
        }
    }
}
