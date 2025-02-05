using System.Collections;
using UnityEngine;

public class PipeEnter : MonoBehaviour
{
    private PlayerSkills playerSkills;
    private bool isInPipeRange = false;
    private Transform playerTransform;
    private bool isUsingPipe = false;


    private void Start()
    {
        playerSkills = FindObjectOfType<PlayerSkills>();

    }


    private void Update()
    {
        if (isInPipeRange)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (playerSkills != null && playerSkills.hasWrench)
                {

                    StartCoroutine(EnterPipe());
                }
            }
        }
    }

    private IEnumerator EnterPipe()
    {
        isUsingPipe = true;
        Vector3 downPosition = playerTransform.position + Vector3.down * 1f; // Move 1 unit down
        Vector3 originalPosition = playerTransform.position; // Store original position

        // Disable player movement
        playerTransform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerTransform.GetComponent<PlayerMovement>().enabled = false;

        // Get SpriteRenderer component
        SpriteRenderer marioSprite = playerTransform.GetComponent<SpriteRenderer>();

        // Get Collider component
        Collider2D playerCollider = playerTransform.GetComponent<Collider2D>();

        if (marioSprite != null)
        {
            marioSprite.sortingLayerName = "Behind"; // Move behind pipe
        }

        // Disable player collider to prevent interactions while entering pipe
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // Move Down
        float elapsedTime = 0f;
        float duration = 0.5f; // Duration of movement

        while (elapsedTime < duration)
        {
            playerTransform.position = Vector3.Lerp(originalPosition, downPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = downPosition; // Ensure final position is exact

        // Wait inside pipe
        yield return new WaitForSeconds(1f);

        // Move Up
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            playerTransform.position = Vector3.Lerp(downPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = originalPosition; // Ensure final position is exact

        // Reset sorting layer
        marioSprite.sortingLayerName = "Front";

        // Enable player collider back after exiting the pipe
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }

        // Reactivate movement
        playerTransform.GetComponent<PlayerMovement>().enabled = true;
        isUsingPipe = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInPipeRange = true;
            playerTransform = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInPipeRange = false;
        }
    }
}
