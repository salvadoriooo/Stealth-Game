using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player; 
    public float smoothSpeed = 0.125f; 
    [SerializeField] float rotationSpeed = 70f;
    private ObjectFader fader;
    private Vector3 offset;
    private Vector3 danceOffset = new Vector3(5f, 7f, -5f); 
    private bool isDancing = false;

    void Start()
    {
        offset = new Vector3(11f, 13f, -9f);

      
        transform.position = player.position + offset;
    }

    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.Q))
        {
            RotationAroundPlayer(Vector3.up);
        }
        if (Input.GetKey(KeyCode.E))
        {
            RotationAroundPlayer(-Vector3.up);
        }

        Vector3 desiredPosition = player.position + (isDancing ? danceOffset : offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(player);
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
                
                    if (fader != null)
                    {
                        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
                        fader.StopFading();
                    }
                }
                else
                {
                   
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

    private void RotationAroundPlayer(Vector3 direction)
    {
        Quaternion rotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, direction);
        offset = rotation * offset;
        transform.position = player.position + offset;
    }

   
    public void SetDancingState(bool isDancing)
    {
        this.isDancing = isDancing;
    }
}
