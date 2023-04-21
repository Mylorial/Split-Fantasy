using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float timeBetweenShots = 0.5f;
    public float score = 0f;
    public GameObject enemyPrefab; // The enemy prefab to instantiate
    public float enemySpawnInterval = 5f; // How often to spawn enemies
    public float enemySpawnRadius = 10f; // Maximum distance from player to spawn enemies
    public GameObject gameOverTextObject; // Reference to the TextMeshPro object that displays the "Game Over" text
    public float gameOverDuration = 1f; // How long to display the "Game Over" text
    public string nextSceneName; // Name of the next scene to load after game over
    public Sprite gameOverImage; // The image to display on game over
    public Color imageColor = Color.white; // The color of the game over image
    public float imageOpacity = 1.0f; // The opacity of the game over image

    private float lastShotTime = 0f;
    private bool gameOver = false;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", enemySpawnInterval, enemySpawnInterval);
    }

    void Update()
    {
        // Shooting
        if (Input.GetMouseButton(0) && Time.time - lastShotTime > timeBetweenShots)
        {
            lastShotTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
        }

        // Health checking
        if (health <= 0)
        {
            // Show "Game Over" message and image
            gameOverTextObject.SetActive(true);
            gameOverTextObject.GetComponent<TextMeshProUGUI>().text = "Game Over";
            GameObject imageObject = new GameObject("Image");
            imageObject.transform.SetParent(transform);
            imageObject.AddComponent<Image>();
            Image imageComponent = imageObject.GetComponent<Image>();
            imageComponent.sprite = gameOverImage;
            imageComponent.color = imageColor;
            imageComponent.color = new Color(imageColor.r, imageColor.g, imageColor.b, imageOpacity);
            RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.sizeDelta = new Vector2(0, 0);

            // Restart the game after a delay
            gameOver = true;
            Invoke("RestartGame", gameOverDuration);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
        }
    }

    public void AddScore(float points)
    {
        score += points;
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = Random.insideUnitCircle.normalized * enemySpawnRadius;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Player collides with enemy, take damage
            TakeDamage(collision.gameObject.GetComponent<EnemyController>().damage);
        }
    }

    private void RestartGame()
    {
        // Load the specified next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Knights Crown");
    }
}