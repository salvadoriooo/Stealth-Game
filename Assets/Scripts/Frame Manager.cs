using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameManager : MonoBehaviour
{
    public RawImage rawImage;
    public float speed = 1.0f;
    public float targetX = 0.5f; // Целевое значение по оси X

    void Start ()
    {
        StartCoroutine (MoveRawImage ());
    }

    IEnumerator MoveRawImage ()
    {
        Rect uvRect = rawImage.uvRect;

        while (Mathf.Abs (uvRect.x - targetX) > 0.01f)
        {
            float newX = Mathf.MoveTowards (uvRect.x, targetX, speed * Time.deltaTime);
            uvRect.x = newX;
            rawImage.uvRect = uvRect;
            yield return null;
        }
    }
}
