using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //Player Dies so Restart Level
            Debug.Log("Player Die as no platform");
            //Restarting Level using LoadScene
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //Restarting Level using transform
            collision.gameObject.transform.position=collision.gameObject.GetComponent<PlayerController>().playerStartPosition;
        }
    }
}
