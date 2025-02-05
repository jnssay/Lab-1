using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverOverlay;
    public TextMeshProUGUI scoreText;
    public JumpOverGoomba jumpOverGoomba;
    public Button restartButton;

    private void Start()
    {
        gameOverOverlay.SetActive(false);
    }

    public void Over()
    {
        gameOverOverlay.SetActive(true);
        scoreText.text = "Final Score: " + jumpOverGoomba.score.ToString();
        restartButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
