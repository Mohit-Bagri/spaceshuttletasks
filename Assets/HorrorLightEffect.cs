using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HorrorLightEffect : MonoBehaviour
{
    public Light pointLight;
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.0f;
    public float minBlinkSpeed = 0.5f;
    public float maxBlinkSpeed = 2.0f;

    private float targetIntensity;
    private float currentIntensity;
    private float blinkSpeed;

    void Start()
    {
        // Initialize starting intensity and blink speed
        currentIntensity = pointLight.intensity;
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        blinkSpeed = Random.Range(minBlinkSpeed, maxBlinkSpeed);

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

            // If the current intensity is very close to the target intensity, set a new random target intensity and blink speed
            if (Mathf.Abs(currentIntensity - targetIntensity) < 0.1f)
            {
                targetIntensity = Random.Range(minIntensity, maxIntensity);
                blinkSpeed = Random.Range(minBlinkSpeed, maxBlinkSpeed);
            }

            // Wait for a short duration before updating intensity again
            yield return null;
        }
    }
}
