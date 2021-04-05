﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float speed = 0.5f;
    const string ENEMY1 = "StaticEnemy";
    const string ENEMY3 = "SmartEnemy";
    const string ISWALK = "isWalk";
    const string GROUND = "Ground";
    const string DIE = "Die";
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (gameObject.name == ENEMY1)
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
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //Player Dies so Restart Level
            Debug.Log("Player Die due to enemy contact");
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
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

        if (gameObject.name == ENEMY3)
        {
            Vector3 scale = transform.localScale;
            if (position.x < -20)
            {
                speed = -speed;
                scale.x = Mathf.Abs(scale.x);
            }
            else if (position.x > -4)
            {
                speed = -speed;
                scale.x = -1f * Mathf.Abs(scale.x);
            }

            transform.localScale = scale;
        }
    }
}
