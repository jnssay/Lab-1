using UnityEngine;

public class WrenchPickup : MonoBehaviour
{
    private Vector3 originalPosition;
    public JumpOverGoomba jumpOverGoomba;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSkills playerSkills = other.GetComponent<PlayerSkills>();
            if (playerSkills != null)
            {
                playerSkills.hasWrench = true;

                gameObject.SetActive(false);
            }
        }
    }


    public void ResetWrench()
    {
        transform.position = originalPosition;
        gameObject.SetActive(true);
    }
}
