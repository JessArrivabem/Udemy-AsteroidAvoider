using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth == null ) {  return; } // if the object doesn't contain PlayerHealth it won't happen

        playerHealth.Crash();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
