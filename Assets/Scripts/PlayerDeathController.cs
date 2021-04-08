using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    const string ENEMY = "Enemy";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //Player Dies so Restart Level
            Debug.Log("Player Die as no platform");
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.NoPlatform();
            playerController.PlayerDie();
        }
        if (collision.gameObject.CompareTag(ENEMY))
        {
            Destroy(collision.gameObject);
        }
    }
}
