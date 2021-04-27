using UnityEngine;

public class EnemyBoundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2d");
        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.ChangeEnemyDirection();
        }

    }
}
