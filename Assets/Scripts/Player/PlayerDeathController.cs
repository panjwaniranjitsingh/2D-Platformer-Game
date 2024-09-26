using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    const string ENEMY = "Enemy";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            //Player Dies so Restart Level
            //Debug.Log("Player Die as no platform");
            playerController.NoPlatform();
            playerController.PlayerDie();
        }
        if (collision.gameObject.CompareTag(ENEMY))
        {
            Destroy(collision.gameObject);
        }
    }
}
