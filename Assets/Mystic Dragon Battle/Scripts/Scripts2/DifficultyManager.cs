using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private float difficultyIncreaseInterval = 30f; // Time in seconds between each difficulty increase
    [SerializeField] private float enemySpawnIntervalDecreaseFactor = 0.9f; // Factor to decrease enemy spawn interval (e.g., 0.9 means 10% decrease)
    [SerializeField] private float enemySpeedIncreaseFactor = 1.1f; // Factor to increase enemy speed (e.g., 1.1 means 10% increase)

    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= difficultyIncreaseInterval)
        {
            elapsedTime = 0f;

            // Decrease enemy spawn interval
            playerControl.enemySpawnInterval *= enemySpawnIntervalDecreaseFactor;

            // Increase enemy speed
            EnemyController[] enemyControllers = FindObjectsOfType<EnemyController>();
            foreach (EnemyController enemyController in enemyControllers)
            {
                enemyController.moveSpeed *= enemySpeedIncreaseFactor;
            }
        }
    }
}