using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxHealth = 50f;
    public float health = 50f;
    public float damage = 10f;
    public float scoreValue = 10f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            player.GetComponent<PlayerController>().AddScore(scoreValue);
            Instantiate(gameObject, transform.position + new Vector3(5f, 5f, 0f), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player takes damage
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
