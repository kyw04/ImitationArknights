using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private Camera m_camera;

    private void Start()
    {
        m_camera = GetComponent<Camera>();
    }
}
