using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    private float fadeSpeed = 10f;
    private float fadeAmount = 0.3f;

    private Renderer[] childRenderers;
    private Color[] originalColors;
    public bool doFade = false;

    void Start()
    {
        // Pobierz wszystkie renderery dzieci
        childRenderers = GetComponentsInChildren<Renderer>();

        // Zapisz oryginalne kolory
        originalColors = new Color[childRenderers.Length];
        for (int i = 0; i < childRenderers.Length; i++)
        {
            originalColors[i] = childRenderers[i].material.color;
        }
    }

    void Update()
    {
        if (doFade)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }

    void FadeNow()
    {
        foreach (Renderer renderer in childRenderers)
        {
            Color currentColor = renderer.material.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
            renderer.material.color = smoothColor;
        }
    }

    void ResetFade()
    {
        int i = 0;
        foreach (Renderer renderer in childRenderers)
        {
            renderer.material.color = Color.Lerp(renderer.material.color, originalColors[i], fadeSpeed * Time.deltaTime);
            i++;
        }
    }
    public void StartFading()
    {
        doFade = true;
    }

    public void StopFading()
    {
        doFade = false;
    }
}
