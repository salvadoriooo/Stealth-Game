using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public Transform player; // Referencja do transformacji gracza
    public float smoothSpeed = 0.125f; // Współczynnik płynnego poruszania kamery

    private ObjectFader fader;
    private Vector3 offset; // Przesunięcie między kamerą a graczem

    void Start()
    {
        offset = new Vector3 (11.8460083f, 13.7055759f, -9.2022553f);

        // Postawienie kamery 
        transform.position = player.position + offset;
    }

    void FixedUpdate()
    {
        // Oblicz docelową pozycję kamery
        Vector3 desiredPosition = player.position + offset;

        // Interpolacja liniowa między aktualną pozycją kamery a docelową pozycją gracza
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Ustaw nową pozycję kamery
        transform.position = smoothedPosition;
    }

    void Update()
    {
       GameObject player = GameObject.FindGameObjectWithTag("Player");
       if (player != null)
       {

                Vector3 dir = player.transform.position - transform.position;
                Ray ray = new Ray(transform.position, dir);

                
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == null) return;
                    if (hit.collider.gameObject == player)
                    {
                  
                        // Jeśli hit trafiony jest w gracza, zatrzymaj fader
                        if (fader != null)
                        {
                    
                        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
                        fader.StopFading();
                        }
                    }
                    else
                    {
                        // Jeśli trafiony jest inny obiekt, sprawdź warstwę i uruchom fader
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                        {
                   
                            fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                            if (fader != null)
                            {
                        
                            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
                            fader.StartFading();
                            }
                        }
                    }
                }
       }
    }
}


