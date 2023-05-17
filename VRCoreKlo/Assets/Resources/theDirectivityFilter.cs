using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class theDirectivityFilter : MonoBehaviour
{
    public float[] ISMPos = new float[3];
    public float[] recPos = new float[3];
    public float[] sourcePos = new float[3];
    public float[] sourceRot = new float[3];

    public Directivity filter;

    bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        ISMPos = new float[3] {transform.position.x, transform.position.z, transform.position.y};
        recPos = new float[3] {Camera.main.transform.position.x, Camera.main.transform.position.z, Camera.main.transform.position.y};
        sourceRot = new float[3] {GameObject.Find("original").transform.eulerAngles.x, GameObject.Find("original").transform.eulerAngles.z, GameObject.Find("original").transform.eulerAngles.y};
        
        if(name == "original")
        {
            filter = new Directivity(new int[2] {-1, -1});
        }
        else
        {
            filter = new Directivity(new int[2] {CreateSources.wallsReflectedOn[int.Parse(name),0], CreateSources.wallsReflectedOn[int.Parse(name),1]});
        }
        
        ISMPos = new float[3] {transform.position.x, transform.position.z, transform.position.y};
        recPos = new float[3] {Camera.main.transform.position.x, Camera.main.transform.position.z, Camera.main.transform.position.y};
        sourcePos = new float[3] {GameObject.Find("original").transform.position.x, GameObject.Find("original").transform.position.z, GameObject.Find("original").transform.position.y};
        sourceRot = new float[3] {GameObject.Find("original").transform.eulerAngles.x, GameObject.Find("original").transform.eulerAngles.z, GameObject.Find("original").transform.eulerAngles.y};
    
        filter.updateFilter(ISMPos, recPos, sourceRot, sourcePos);

        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        ISMPos = new float[3] {transform.position.x, transform.position.z, transform.position.y};
        recPos = new float[3] {Camera.main.transform.position.x, Camera.main.transform.position.z, Camera.main.transform.position.y};
        sourcePos = new float[3] {GameObject.Find("original").transform.position.x, GameObject.Find("original").transform.position.z, GameObject.Find("original").transform.position.y};
        sourceRot = new float[3] {GameObject.Find("original").transform.eulerAngles.x, GameObject.Find("original").transform.eulerAngles.z, GameObject.Find("original").transform.eulerAngles.y};

        filter.updateFilter(ISMPos, recPos, sourceRot, sourcePos);
        
        print(name + ": " + (filter.azimuth *180/Mathf.PI).ToString() + ", " + (filter.elevation *180/Mathf.PI).ToString());
        
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if(!ready)
            return;
        for(int i = 0; i < data.Length; i+=2)
        {
            data[i]     = filter.ProcessSample(data[i]);
            data[i+1]   = filter.ProcessSample(data[i+1]);
        }
    }
}
