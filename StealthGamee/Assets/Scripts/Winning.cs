using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{
    [SerializeField] EventReference Win;

    public void WinGame()
    {
        RuntimeManager.PlayOneShot(Win);
        SceneManager.LoadScene("WinScene");
    }
}
