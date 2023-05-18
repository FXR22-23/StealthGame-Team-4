using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    [SerializeField] EventReference diamond;
    [SerializeField] EventReference chest;

    public void playDiamond()
    {
        RuntimeManager.PlayOneShot(diamond);
    }

    public void playChest()
    {
        RuntimeManager.PlayOneShot(chest);
    }
}
