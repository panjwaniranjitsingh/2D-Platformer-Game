using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float speed = 0.5f;
    const string STATICENEMY = "StaticEnemy";
    const string ISWALK = "isWalk";
    const string GROUND = "Ground";
    const string DIE = "Die";
    const string ENEMYBOUND = "EnemyBound";

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (gameObject.name == STATICENEMY)
            animator.SetBool(ISWALK, false);
        else
            animator.SetBool(ISWALK, true);
    }
    private void Update()
    {
        if(animator.GetBool(ISWALK))
            EnemyHorizontalMovement();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            //Player Dies so Restart Level
            //Debug.Log("Player Die due to enemy contact");
            playerController.PlayerDie();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND))
        {
            animator.SetBool(ISWALK, false);
            animator.SetBool(DIE, true);
        }
    }

    private void EnemyHorizontalMovement()
    {
        //Move enemy horizontally
        Vector3 position = transform.position;
        position.x += speed * Time.deltaTime;
        transform.position = position;
    }

    public void ChangeEnemyDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x = -1f * scale.x;
        speed = -speed;
        transform.localScale = scale;
    }
}
