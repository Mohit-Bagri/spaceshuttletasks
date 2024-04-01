using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HorrorLightEffect : MonoBehaviour
{
    public Light pointLight;
    public float minFlickerDelay = 0.05f;
    public float maxFlickerDelay = 0.2f;

    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            // Turn the light off
            pointLight.enabled = false;

            // Wait for a random short duration
            yield return new WaitForSeconds(Random.Range(minFlickerDelay, maxFlickerDelay));

            // Turn the light on
            pointLight.enabled = true;

            // Wait for another random short duration
            yield return new WaitForSeconds(Random.Range(minFlickerDelay, maxFlickerDelay));
        }
    }
}
