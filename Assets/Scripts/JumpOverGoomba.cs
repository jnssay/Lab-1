using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpOverGoomba : MonoBehaviour
{
    // public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    private bool onGroundState;

    [System.NonSerialized]
    public int score = 0;
    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    public Transform enemyParent;

    void Start()
    {

    }

    void Update()
    {


    }

    void FixedUpdate()
    {
        // mario jumps
        if (Input.GetKeyDown("space") && onGroundCheck())
        {
            onGroundState = false;
            countScoreState = true;
        }

        if (!onGroundState && countScoreState)
        {
            foreach (Transform enemy in enemyParent)
            {
                if (Mathf.Abs(transform.position.x - enemy.position.x) < 0.5f)
                {
                    countScoreState = false;
                    scoreText.text = "Score: " + score.ToString();
                    break;
                }
            }
        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }


    // helper
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }



    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
