using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    private Vector3 initialPosition;
    public float leftLimit = -10f;
    public float rightLimit = 10f;
    private bool isBouncing = false;
    private List<GameObject> enemyList = new List<GameObject>();
    private bool isTouchingGoomba = false;
    public GameObject gameOverOverlay;




    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;

        // Store all enemies in a list
        foreach (Transform eachChild in enemies.transform)
        {
            if (eachChild.GetComponent<EnemyMovement>() != null)
            {
                enemyList.Add(eachChild.gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
        if (col.gameObject.CompareTag("Platform")) onGroundState = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isTouchingGoomba = true;

            if (isTouchingGoomba && transform.position.y > other.transform.position.y + 0.2f)
            {

                jumpOverGoomba.score += 10;
                scoreText.text = "Score: " + jumpOverGoomba.score.ToString();

                float bounceHeight = upSpeed * 6f;
                float bounceBack = faceRightState ? -3.5f : 3.5f;

                marioBody.velocity = new Vector2(bounceBack, bounceHeight);

                isBouncing = true;
                Invoke("EndBounce", 0.3f);


                other.gameObject.SetActive(false);

                return;
            }


            Time.timeScale = 0.0f;
            FindObjectOfType<GameOverManager>().Over();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isTouchingGoomba = false;
        }
    }



    public void RestartButtonCallback(int input)
    {

        ResetGame();

        Time.timeScale = 1.0f;
    }

    void ResetPosition()
    {
        marioBody.transform.position = initialPosition;
    }

    public void ResetGame()
    {
        ResetPosition();

        faceRightState = true;
        marioSprite.flipX = false;
        gameOverOverlay.SetActive(false);


        scoreText.text = "Score: 0";

        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<EnemyMovement>().ResetEnemy();
        }


        jumpOverGoomba.score = 0;

        WrenchPickup wrench = GameObject.FindObjectOfType<WrenchPickup>(true);
        if (wrench != null)
        {
            wrench.ResetWrench();
        }


        PlayerSkills playerSkills = GetComponent<PlayerSkills>();
        if (playerSkills != null)
        {
            playerSkills.hasWrench = false;
        }

    }




    void FixedUpdate()
    {
        if (!isBouncing)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
            }
            else
            {
                marioBody.velocity = new Vector2(0, marioBody.velocity.y);
            }

            if (Input.GetKeyDown("a") && faceRightState)
            {
                faceRightState = false;
                marioSprite.flipX = true;
            }

            if (Input.GetKeyDown("d") && !faceRightState)
            {
                faceRightState = true;
                marioSprite.flipX = false;
            }

            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                marioBody.velocity = new Vector2(0, marioBody.velocity.y);
            }
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            transform.position.y,
            transform.position.z
        );
    }

    void EndBounce()
    {
        isBouncing = false;
    }

}