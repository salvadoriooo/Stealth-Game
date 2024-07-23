using System.Collections;
using UnityEngine;

public class StationaryGuard : MonoBehaviour
{
    Animator animator;
    Rigidbody guardRigidbody;

    public static event System.Action OnGuardHasSpottedPlayer;

    public float speedRotation = 90;
    public float timeToSpotPlayer = .5f;
    public float rotationDelay = 1f; // Czas oczekiwania między rotacjami

    public Light spotLight;
    public float viewDistance;
    public LayerMask viewMask;

    float playerVisibleTimer;
    float viewAngle;

    Transform player;
    Color originalSpotlightColour;

    public Transform[] lookPoints; // Punkty, na które strażnik będzie patrzeć

    int currentLookPointIndex;

    void Start ()
    {
        animator = GetComponent<Animator> ();
        guardRigidbody = GetComponent<Rigidbody> ();

        player = GameObject.FindGameObjectWithTag ("Player").transform;
        viewAngle = spotLight.spotAngle;
        originalSpotlightColour = spotLight.color;

        StartCoroutine (LookAround ());
    }

    void Update ()
    {
        if (CanSeePlayer ())
        {
            playerVisibleTimer += Time.deltaTime;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp (playerVisibleTimer, 0, timeToSpotPlayer);
        spotLight.color = Color.Lerp (originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            if (OnGuardHasSpottedPlayer != null)
            {
                OnGuardHasSpottedPlayer ();
            }
        }

        animator.SetFloat ("speed", guardRigidbody.velocity.magnitude);
    }

    bool CanSeePlayer ()
    {
        if (Vector3.Distance (transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle (transform.forward, dirToPlayer);

            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast (transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator LookAround ()
    {
        while (true)
        {
            // Sprawdzenie, czy punkt patrzenia nadal istnieje
            if (lookPoints[currentLookPointIndex] != null)
            {
                Vector3 targetLookPoint = lookPoints[currentLookPointIndex].position;
                yield return StartCoroutine (TargetRotation (targetLookPoint));
            }
            currentLookPointIndex = (currentLookPointIndex + 1) % lookPoints.Length;
            yield return new WaitForSeconds (rotationDelay); // Czas oczekiwania przed obróceniem się do kolejnego punktu
        }
    }

    IEnumerator TargetRotation (Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = Mathf.Atan2 (dirToLookTarget.x, dirToLookTarget.z) * Mathf.Rad2Deg;

        while (Mathf.Abs (Mathf.DeltaAngle (transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetAngle, speedRotation * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    void OnDrawGizmos ()
    {
        if (lookPoints != null && lookPoints.Length > 0)
        {
            Gizmos.color = Color.yellow;
            foreach (Transform lookPoint in lookPoints)
            {
                if (lookPoint != null)
                {
                    Gizmos.DrawSphere (lookPoint.position, .1f);
                }
            }
        }

        Gizmos.color = Color.red;
        Gizmos.DrawRay (transform.position, transform.forward * viewDistance);
    }
}
