using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionPrefab; // The explosion prefab
    public float explosionRadius = 1f; // Radius of the explosion
    public float explosionForce = 1000f; // Force of the explosion
    public float timeToExplode = 3f; // Time in seconds before the grenade explodes after being thrown

    private bool exploded = false; // To ensure explosion happens only once
    private float timeSinceActivation = 0f; // Time since the grenade was activated (thrown or held)
    private Rigidbody rb; // Rigidbody component of the grenade

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the grenade prefab.");
        }
    }

    private void Update()
    {
        // If the grenade is activated (thrown or held) and has not exploded yet
        if (timeSinceActivation > 0 && !exploded)
        {
            timeSinceActivation += Time.deltaTime;
            // If the time since activation exceeds timeToExplode, explode
            if (timeSinceActivation >= timeToExplode)
            {
                Explode();
            }
        }
    }

    // Function to handle grenade explosion
    private void Explode()
    {
        exploded = true;

        // Instantiate the explosion prefab at grenade's position
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Apply explosive force to nearby colliders
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        // Destroy the explosion prefab after a short delay to allow explosion effect to be visible
        Destroy(explosion, 1f);

        // Destroy the grenade
        Destroy(gameObject);
    }

    // Function to handle grenade activation (called when the grenade is thrown or held)
    public void ActivateGrenade()
    {
        timeSinceActivation = 0f;
    }
}
