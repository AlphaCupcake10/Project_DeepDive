using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraZoom : MonoBehaviour
{
    public static CameraZoom Instance;
    public CinemachineVirtualCamera cam;
    public float Smoothing = 4;
    float defaultZoom;
    float TargetZoom;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        defaultZoom = cam.m_Lens.OrthographicSize;
        TargetZoom = defaultZoom;
    }

    void Update()
    {
        cam.m_Lens.OrthographicSize += (TargetZoom - cam.m_Lens.OrthographicSize)/Smoothing;
    }

    public void SetZoom(float zoom)
    {
        TargetZoom = defaultZoom * zoom;
    }

    
}
