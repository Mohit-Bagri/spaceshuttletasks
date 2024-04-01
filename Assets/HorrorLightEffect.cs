using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HorrorLightEffect : MonoBehaviour
{
    public Light pointLight;
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.0f;
    public float blinkSpeed = 1.0f;

    private float targetIntensity;
    private float currentIntensity;

    void Start()
    {
        // Initialize starting intensity
        currentIntensity = pointLight.intensity;
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        
        // Start the blinking coroutine
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // Smoothly transition between current intensity and target intensity
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * blinkSpeed);
            pointLight.intensity = currentIntensity;

            // If the current intensity is very close to the target intensity, set a new random target intensity
            if (Mathf.Abs(currentIntensity - targetIntensity) < 0.1f)
            {
                targetIntensity = Random.Range(minIntensity, maxIntensity);
            }

            // Wait for a short duration before updating intensity again
            yield return null;
        }
    }
}
