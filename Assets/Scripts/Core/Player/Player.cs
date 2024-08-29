

using UnityEngine;

public class Player : MonoBehaviour
{
    public event System.Action OnReachedEndOfLevel;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float smoothMoveTime = .1f;
    [SerializeField] private float turnSpeed = 8;

    private Animator animator;
    private float angle;
    private float smoothInputMagnitude;
    private float smoothMoveVelocity;
    private Vector3 velocity;
    private Rigidbody rigibody;
    private bool disabled;
    private Camera mainCamera;
    

    void Start ()
    {
        Guard.OnGuardHasSpottedPlayer += OnDisable;
        StationaryGuard.OnGuardHasSpottedPlayer += OnDisable;
        rigibody = GetComponent<Rigidbody> ();
        animator = GetComponent<Animator> ();
        mainCamera = Camera.main;
    }

    void Update ()
    {
        Vector3 inputDirection = Vector3.zero;
        if (!disabled)
        {
            inputDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
        }
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp (smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);
        animator.SetFloat ("speed", smoothInputMagnitude);

        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0;
        Quaternion camRotation = Quaternion.LookRotation (camForward);
        Vector3 moveDirection = camRotation * inputDirection;

        float targetAngle = 90 - Mathf.Atan2 (moveDirection.z, moveDirection.x) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle (angle, targetAngle, turnSpeed * Time.deltaTime * inputMagnitude);

        velocity = transform.forward * speed * smoothInputMagnitude;
    }

    void OnTriggerEnter(Collider hitCollider)
    {
        if (hitCollider.tag == "Finish")
        {
            FinishPoint finishPoint = hitCollider.GetComponent<FinishPoint>();
            if (finishPoint != null && finishPoint.AreAllCoinsCollected())
            {
                OnDisable();
                OnReachedEndOfLevel?.Invoke();
                animator.SetTrigger("dance");

                // Установка состояния танца для камеры
                MainCamera mainCameraScript = mainCamera.GetComponent<MainCamera>();
                if (mainCameraScript != null)
                {
                    mainCameraScript.SetDancingState(true);
                }
            }
            else
            {
                finishPoint.ShowMessage("Zbierz wszystkie monety, aby ukończyć poziom!");
            }
        }
    }

    void OnDisable ()
    {
        disabled = true;
    }

    void FixedUpdate ()
    {
        rigibody.MoveRotation (Quaternion.Euler (Vector3.up * angle));
        rigibody.MovePosition (rigibody.position + velocity * Time.deltaTime);
    }

    void OnDestroy ()
    {
        Guard.OnGuardHasSpottedPlayer -= OnDisable;
    }
}



