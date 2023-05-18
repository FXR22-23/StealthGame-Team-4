using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParameterByName : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;
    [SerializeField] 
    GameObject target;
    Animator animator;


    public EventReference BGM;


    [SerializeField] [Range(0, 40f)]
    private float distance;

    [SerializeField] [Range(0, 1)]
    private float chase;

    // Start is called before the first frame update
    void Start()
    {
        instance = RuntimeManager.CreateInstance(BGM);
        instance.start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = 40f - (Vector3.Distance(transform.position, target.transform.position) * 40f / 56f) ;
        instance.setParameterByName("Distance to Goal", distance);
        float chase = animator.GetBool("visible")? 1f : 0f;
        instance.setParameterByName("isChasing", chase);
    }
}
