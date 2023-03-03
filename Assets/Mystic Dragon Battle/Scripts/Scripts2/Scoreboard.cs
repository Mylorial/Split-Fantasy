using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    private void Start()
    {
        // Listen for enemy destroyed events
        Enemy.OnDestroyed += IncreaseScore;
    }

    private void OnDestroy()
    {
        // Stop listening for events when object is destroyed
        Enemy.OnDestroyed -= IncreaseScore;
    }

    private void IncreaseScore()
    {
        // Increase score and update display
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}