using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name=="Guard") 
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}
