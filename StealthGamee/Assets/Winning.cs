using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{
  private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Goblet")
        {
            SceneManager.LoadScene("WinScene");
        }
        
    }
}
