using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //Player Dies so Restart Level
            Debug.Log("Player Die due to enemy contact");
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.PlayerDie();
        }
    }
}
