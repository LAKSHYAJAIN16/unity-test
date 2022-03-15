using UnityEngine;

public class WInDetector : MonoBehaviour
{
    public GameObject winScreen;
    public PlayerControls playerControls;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Debug.Log("You Won!");
            Win();
        }
    }

    private void Win()
    {
        winScreen.SetActive(true);
        playerControls.enabled = false;
    }
}
