using UnityEngine;

public class LevelOverController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            //Level Over Load Next Level
            //Debug.Log("OnTriggerEnter2D with " + collision.name);
            //Debug.Log("Level Over");
            //playerController.LevelComplete();
            playerController.LevelCompleted = true;
            LevelManager.Instance.MarkCurrentLevelComplete();
        }
    }
}
