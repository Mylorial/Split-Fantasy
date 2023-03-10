using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameOverTrigger : MonoBehaviour
{
    public float restartDelay = 2.0f; // Delay before restarting the game
    public TextMeshProUGUI gameOverText; // Reference to the TextMeshPro object
    public GameObject triggerObject; // The game object to trigger the scene reload

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == triggerObject)
        {
            // Reload the current scene
            Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.buildIndex);

            // Set the game over text object active and display "Game Over"
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over";

            // Restart the game after a delay
            Invoke("RestartScene", restartDelay);
        }
    }

    private void RestartScene()
    {
        // Reload the current scene again to restart the game
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.buildIndex);
    }
}