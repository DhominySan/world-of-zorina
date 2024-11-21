using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private Attack playerAttack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerAttack = collision.GetComponent<Attack>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            playerAttack = null;
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerAttack != null)
            {
                playerAttack.EnableAttack();
                Destroy(gameObject);
            }
        }
    }
}
