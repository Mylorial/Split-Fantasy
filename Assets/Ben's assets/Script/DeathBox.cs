using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the DeathBox has a Health component
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            // If the object has a Health component, decrease its health to 0
        
        }
    }
}
