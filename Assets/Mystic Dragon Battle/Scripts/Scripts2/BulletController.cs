using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 20f;
    public float lifetime = 2f;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Enemy takes damage
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            
        }
    }
}