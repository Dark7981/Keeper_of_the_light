using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightScript : MonoBehaviour
{

    [SerializeField] private float lightItensity;
    [SerializeField] private float timeLight;
    [SerializeField] private float minItensity;
    [SerializeField] private float minRadius;

    private Light2D light;
    private float timeLeft;

    public float maxItensity;
    public float maxRadius;

    void Start()
    {
        light = gameObject.GetComponent<Light2D>();
        maxItensity = light.intensity;
        maxRadius = light.pointLightOuterRadius;
        StartCoroutine(TimerLight());
    }
    private IEnumerator TimerLight()
    {
        while (timeLeft < timeLight)
        {
            timeLeft += timeLight/ 10;
            if (light.intensity - 0.1f > minItensity)
            {
                light.intensity -= 0.1f;
            }
            if (light.pointLightOuterRadius - 0.2f > minRadius)
            {
                light.pointLightOuterRadius -= 0.2f;
            }
            yield return new WaitForSeconds(timeLight/10);
        }
;       yield return null;
    }
    public void StartTimer()
    {
        light.intensity = 1.5f;
        light.pointLightOuterRadius = 5f;
        timeLeft = 0;
        StartCoroutine(TimerLight());
    }
    
}
