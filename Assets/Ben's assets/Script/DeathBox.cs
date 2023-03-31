using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            // Reload the current scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
    }
    }
}
