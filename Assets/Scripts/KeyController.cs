using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            //Key collected by Player
            //Debug.Log("Player collects the key");
            playerController.KeyPicked();
            Destroy(gameObject);
        }
    }
}
