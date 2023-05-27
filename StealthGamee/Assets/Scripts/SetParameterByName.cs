using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParameterByName : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;
    FMOD.Studio.EventInstance instanceChase;
    [SerializeField] 
    GameObject target;
    Animator animator;

    public EventReference BGM;
    public EventReference Chase;
    bool isChasePlaying = false;
    bool isBGMPlaying = false;

    


    [SerializeField] [Range(0, 40f)]
    private float distance;


    // Start is called before the first frame update
    void Start()
    {
        instance = RuntimeManager.CreateInstance(BGM);
        instanceChase = RuntimeManager.CreateInstance(Chase);
        instance.start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = 40f - (Vector3.Distance(transform.position, target.transform.position) * 40f / 56f) ;
        instance.setParameterByName("Distance to Goal", distance);
    }

    public void setChase()
    {
        if (!isChasePlaying)
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
            instance = RuntimeManager.CreateInstance(BGM);
            instanceChase.start();
            isChasePlaying=true;
            isBGMPlaying = false;
        }

        
    }


    public void resetToNormal()
    {
        if (!isBGMPlaying)
        {
            Debug.Log("Im in Reset To Normal");
            instanceChase.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instanceChase.release();
            instanceChase = RuntimeManager.CreateInstance(Chase);
            instance.start();
            isBGMPlaying=true;
            isChasePlaying = false;
        }
        
    }
    private void OnDestroy()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instanceChase.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
