using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        // Calculate the direction from the object to the player
        Vector3 direction = player.position - transform.position;

        // Calculate the distance from the object to the player
        float distance = direction.magnitude;

        // Normalize the direction vector to get the direction of movement
        direction = direction.normalized;

        // Move the object towards the player if it's not too close
        if (distance > 1f)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}