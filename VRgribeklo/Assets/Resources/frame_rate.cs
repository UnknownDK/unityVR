using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frame_rate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 300;
        OVRPlugin.systemDisplayFrequency = 120.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}