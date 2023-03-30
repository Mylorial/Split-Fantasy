using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public string miniGameSceneName = "Mystic Dragon Battle";

    public void LoadMiniGame()
    {
        SceneManager.LoadScene(miniGameSceneName);
    }
}