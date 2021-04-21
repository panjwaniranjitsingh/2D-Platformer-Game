using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //Key collected by Player
            //Debug.Log("Player collects the key");
            PlayerController playerController= collision.gameObject.GetComponent<PlayerController>();
            playerController.KeyPicked();
            Destroy(gameObject);
        }
    }
}
