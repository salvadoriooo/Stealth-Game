
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;

    public float speed = 7f;
    public float smoothMoveTime = .1f;
    public float turnSpeed = 8;
    public float additionRotation = 0f;
    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    Vector3 velocity;
    
    Rigidbody rigibody;
    bool disabled;

    Camera mainCamera;

    public event System.Action OnReachedEndOfLevel;

    void Start()
    {
     Guard.OnGuardHasSpottedPlayer += OnDisable;
     rigibody = GetComponent<Rigidbody>();
     animator = GetComponent<Animator>();
     mainCamera = Camera.main;
    }

    
    void Update()

    {
        //Player Movement 
        Vector3 inputDirection = Vector3.zero;
        if (!disabled)
        {
            inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        animator.SetFloat("speed", smoothInputMagnitude);

          //Camera Follow
        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0;
        Quaternion camRotation = Quaternion.LookRotation(camForward);
        Vector3 moveDirection = camRotation * inputDirection;

          //Player Rotation
        float targetAngle = 90 - Mathf.Atan2(moveDirection.z, moveDirection.x) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, turnSpeed * Time.deltaTime * inputMagnitude);

        velocity = transform.forward * speed * smoothInputMagnitude;


       
    }

     void OnTriggerEnter(Collider hitCollider)
    {
        if (hitCollider.tag == "Finish")
        {
            OnDisable(); 
            if (OnReachedEndOfLevel != null)
            {
                OnReachedEndOfLevel();
            }
            animator.SetTrigger ("dance");
        }
    }
    void OnDisable()
    {
        disabled = true;   
    }
    void FixedUpdate()
    {
        rigibody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigibody.MovePosition(rigibody.position + velocity * Time.deltaTime);
    }
    void OnDestroy()
    {
        Guard.OnGuardHasSpottedPlayer -= OnDisable;
    }
}


