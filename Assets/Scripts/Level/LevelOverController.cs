using UnityEngine;

public class LevelOverController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //Level Over Load Next Level
            //Debug.Log("OnTriggerEnter2D with " + collision.name);
            //Debug.Log("Level Over");
            PlayerController playerController=collision.gameObject.GetComponent<PlayerController>();
            //playerController.LevelComplete();
            playerController.LevelCompleted = true;
            LevelManager.Instance.MarkCurrentLevelComplete();
        }
    }
}
