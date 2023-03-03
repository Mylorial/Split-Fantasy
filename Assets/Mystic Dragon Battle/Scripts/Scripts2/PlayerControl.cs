using UnityEngine;

public class PlayerControl : MonoBehaviour
{
   
    public float maxHealth = 100f;
    public float health = 100f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float timeBetweenShots = 0.5f;
    public float score = 0f;

    private float lastShotTime = 0f;

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
            // Reload the scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Player died!");
        }
    }

    public void AddScore(float points)
    {
        score += points;
    }
}