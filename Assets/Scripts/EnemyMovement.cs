using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatrolTime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private bool isChangingDirection = false;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();

        startPosition = transform.position;

        originalX = transform.position.x;

        RandomizeMovement();

        ComputeVelocity();
    }

    public void ResetEnemy()
    {
        transform.position = startPosition;
        gameObject.SetActive(true);


        RandomizeMovement();

        ComputeVelocity();
    }

    void RandomizeMovement()
    {
        maxOffset = UnityEngine.Random.Range(3.0f, 7.0f);
        enemyPatrolTime = UnityEngine.Random.Range(1.5f, 3.5f);
        moveRight = UnityEngine.Random.value > 0.5f ? 1 : -1;
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatrolTime, 0);
    }

    void MoveGoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {

            MoveGoomba();
        }
        else if (!isChangingDirection)
        {

            StartCoroutine(ChangeDirectionWithDelay());
        }
    }

    IEnumerator ChangeDirectionWithDelay()
    {
        isChangingDirection = true;

        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2.0f));

        moveRight *= -1;
        ComputeVelocity();
        MoveGoomba();

        isChangingDirection = false;
    }
}
