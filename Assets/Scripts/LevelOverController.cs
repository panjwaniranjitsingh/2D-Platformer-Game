using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //Level Over Load Next Level
            Debug.Log("OnTriggerEnter2D with " + collision.name);
           // Debug.Log(SceneManager.GetActiveScene().buildIndex);
            if(SceneManager.GetActiveScene().buildIndex== 1)
                SceneManager.LoadScene(0);
            if (SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene(1);
            
        }
    }
}
