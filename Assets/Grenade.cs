using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grenade : MonoBehaviour
{
    public GameObject explosionPrefab; // The explosion prefab
    public float explosionRadius = 5f; // Radius of the explosion
    public float explosionForce = 1000f; // Force of the explosion
    public float timeToExplode = 7f; // Time in seconds before the grenade explodes

    private bool exploded = false; // To ensure explosion happens only once
    private float timeHeld = 0f; // Time the grenade has been held
    private XRGrabInteractable xrGrabInteractable; // Reference to the XR Grab Interactable component

    private void Start()
    {
        // Get reference to XR Grab Interactable component
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
        if (xrGrabInteractable == null)
        {
            Debug.LogError("XR Grab Interactable component is missing from the grenade prefab.");
        }
    }

    // Function to handle grenade explosion
    public void Explode()
    {
        if (!exploded)
        {
            exploded = true;

            // Instantiate the explosion prefab at grenade's position
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

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

            // Disable grenade's renderer and collider to make it disappear
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false; // Disable Mesh Collider

            // Destroy the grenade after a short delay to allow explosion effect to be visible
            Destroy(gameObject, 1f);
        }
    }

    // Function to handle interaction with the grenade
    public void Interact()
    {
        // Increment the timeHeld when the grenade is being held
        timeHeld += Time.deltaTime;

        // Check if the timeHeld has reached the threshold for explosion
        if (timeHeld >= timeToExplode)
        {
            Explode(); // Call the explode function when the grenade has been held for the specified time
        }
    }
}
