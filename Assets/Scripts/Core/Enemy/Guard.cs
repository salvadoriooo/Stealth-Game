using System.Collections;
using UnityEngine;


public class Guard : MonoBehaviour
{
   
    Rigidbody guardRigidbody;

    public static event System.Action OnGuardHasSpottedPlayer;

    public float speed = 5;
    public float speedRotation = 90;
    public float waitTime = .3f;
    public float timeToSpotPlayer = .5f;

    

    public Light spotLight;
    public float viewDistance;
    public LayerMask viewMask;


    float playerVisibleTimer;
    float viewAngle;


    public Transform pathHolder;
    Transform player;
    Color originalSpotlightColour;

  
    void Start()
    {
        
        guardRigidbody = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        originalSpotlightColour = spotLight.color;

        Vector3[] wayPoints = new Vector3[pathHolder.childCount];
       
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = pathHolder.GetChild(i).position; 
            
        }
       
        

        StartCoroutine(FollowPath(wayPoints));
    }

    void Update()
    {
        
        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
        } else
        {
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotLight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            if(OnGuardHasSpottedPlayer != null)
            {
                OnGuardHasSpottedPlayer();
            }
        }

      

       
    }

    bool CanSeePlayer()
    {
       

        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);

            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if(!Physics.Linecast(transform.position,player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator FollowPath(Vector3[] wayPoints)
    {
       transform.position = wayPoints[0];
        int targetWayPointIndex = 1;
        Vector3 targetWayPoint = wayPoints[targetWayPointIndex];
        transform.LookAt(targetWayPoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            if (transform.position == targetWayPoint)
            {
               targetWayPointIndex = (targetWayPointIndex + 1) % wayPoints.Length; 
               targetWayPoint = wayPoints[targetWayPointIndex];
            yield return new WaitForSeconds (waitTime);
            yield return StartCoroutine(TargetRotation(targetWayPoint));
            yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    

    IEnumerator TargetRotation(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90-Mathf.Atan2(dirToLookTarget.z,dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetAngle ,speedRotation * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }
    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
     foreach(Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .1f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
   
       

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
    

}
