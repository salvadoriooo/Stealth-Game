using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameManager : MonoBehaviour
{
    public RawImage sb_rawImage; // nasz obiekt do przesunięcia
    public float sb_speed = 1.0f; // prędkość przesunięcia
    public float sb_targetX = 0.5f; // docelowa wartość na osi X

    void Start ()
    {
        StartCoroutine (MoveRawImage ());
    }

    IEnumerator MoveRawImage ()
    {
        Rect uvRect = sb_rawImage.uvRect;

        while (Mathf.Abs (uvRect.x - sb_targetX) > 0.01f)
        {
            float newX = Mathf.MoveTowards (uvRect.x, sb_targetX, sb_speed * Time.deltaTime);
            uvRect.x = newX;
            sb_rawImage.uvRect = uvRect;
            yield return null;
        }
    }
}
