using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void Destroyed();
    public static event Destroyed OnDestroyed;

    private void OnDestroy()
    {
        // Dispatch the event when the enemy object is destroyed
        OnDestroyed?.Invoke();
    }
}