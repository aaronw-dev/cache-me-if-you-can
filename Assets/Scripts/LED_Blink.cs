using System.Collections;
using UnityEngine;

public class LED_Blink : MonoBehaviour
{
    public float blinkFrequency = 1;
    public Material ledOnMaterial;
    public Material ledOffMaterial;
    private Renderer ledRenderer;

    private void Start()
    {
        ledRenderer = GetComponent<Renderer>();
        StartCoroutine(BlinkCoroutine());
    }

    public IEnumerator BlinkCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        while (true)
        {
            Material[] mats = ledRenderer.materials;
            mats[1] = ledOnMaterial;
            ledRenderer.materials = mats;
            float onTime = blinkFrequency * Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(onTime);

            mats[1] = ledOffMaterial;
            ledRenderer.materials = mats;
            float offTime = blinkFrequency * Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(offTime);
        }
    }
}
