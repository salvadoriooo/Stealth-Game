

using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event System.Action OnReachedEndOfLevel; // Zdarzenie wywoływane po osiągnięciu końca poziomu

    [SerializeField] private float sb_speed = 3.5f; // Prędkość ruchu postaci
    [SerializeField] private float sb_smoothMoveTime = .1f; // Czas wygładzania ruchu
    [SerializeField] private float sb_turnSpeed = 8; // Prędkość obrotu postaci

    private Animator sb_animator; // Referencja do komponentu 
    private float sb_angle; // Aktualny kąt obrotu postaci
    private float sb_smoothInputMagnitude; // Wygładzona wartość wejścia (ruchu)
    private float sb_smoothMoveVelocity; // Prędkość wygładzania ruchu
    private Vector3 sb_velocity; // Prędkość poruszania się postaci
    private Rigidbody sb_rigibody; // Komponent Rigidbody, odpowiadający za fizykę postaci
    private bool sb_disabled; // Flaga określająca, czy postać jest zablokowana
    private Camera sb_mainCamera; // Referencja do głównej kamery w grze

    // Metoda wywoływana przy starcie gry
    void Start ()
    {
        // Subskrypcja na zdarzenie wykrycia gracza przez strażników
        Guard.OnGuardHasSpottedPlayer += OnDisable;
        StationaryGuard.OnGuardHasSpottedPlayer += OnDisable;
        // Inicjalizacja komponentów Rigidbody, Animator i przypisanie głównej kamery
        sb_rigibody = GetComponent<Rigidbody> ();
        sb_animator = GetComponent<Animator> ();

        // zmienna, która przechowuje referencję do głównej kamery, aby można było jej używać w dalszej części kodu 
        sb_mainCamera = Camera.main;
    }

    // Metoda Update wywoływana w każdej klatce
    void Update ()
    {
        // Kierunek ruchu gracza, domyślnie zerowy wektor
        Vector3 sb_inputDirection = Vector3.zero;

        // Sprawdzenie, czy gracz nie jest zablokowany
        if (!sb_disabled)
        {
            // Pobieranie kierunku ruchu na podstawie wejścia użytkownika
            sb_inputDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
        }

        // Obliczanie wielkości ruchu na podstawie kierunku wejścia
        float sb_inputMagnitude = sb_inputDirection.magnitude;

        // Wygładzanie ruchu, aby przejścia były płynniejsze
        sb_smoothInputMagnitude = Mathf.SmoothDamp (sb_smoothInputMagnitude, sb_inputMagnitude, ref sb_smoothMoveVelocity, sb_smoothMoveTime);
        // Przypisanie wartości do parametru 'speed' w Animatorze, aby kontrolować animację
        sb_animator.SetFloat ("speed", sb_smoothInputMagnitude);

        // Obliczanie kierunku ruchu względem kamery
        Vector3 sb_camForward = sb_mainCamera.transform.forward;
        sb_camForward.y = 0; // Ignorowanie osi Y, aby ruch był płaski (2D)
        Quaternion sb_camRotation = Quaternion.LookRotation (sb_camForward);
        Vector3 sb_moveDirection = sb_camRotation * sb_inputDirection;

        // Obliczanie kąta obrotu na podstawie kierunku ruchu
        float sb_targetAngle = 90 - Mathf.Atan2 (sb_moveDirection.z, sb_moveDirection.x) * Mathf.Rad2Deg;
        sb_angle = Mathf.LerpAngle (sb_angle, sb_targetAngle, sb_turnSpeed * Time.deltaTime * sb_inputMagnitude);

        // Wyliczenie prędkości na podstawie kierunku i prędkości ruchu
        sb_velocity = transform.forward * sb_speed * sb_smoothInputMagnitude;
    }

    // Metoda wywoływana, gdy postać wejdzie w kolizję z innym obiektem
    void OnTriggerEnter(Collider hitCollider)
    {
        // Sprawdzenie, czy gracz dotarł do punktu końcowego
        if (hitCollider.tag == "Finish")
        {
            FinishPoint sb_finishPoint = hitCollider.GetComponent<FinishPoint>();
            if (sb_finishPoint != null && sb_finishPoint.AreAllCoinsCollected())
            {
                // Wyłączenie ruchu gracza
                OnDisable();
                // Wywołanie zdarzenia zakończenia poziomu
                OnReachedEndOfLevel?.Invoke();
                // Wyzwolenie animacji tańca
                sb_animator.SetTrigger("dance");

                // Ustawienie stanu tańca dla kamery
                MainCamera sb_mainCameraScript = sb_mainCamera.GetComponent<MainCamera>();
                if (sb_mainCameraScript != null)
                {
                    sb_mainCameraScript.SetDancingState(true);
                }
            }
            else
            {
                sb_finishPoint.ShowMessage("Zbierz wszystkie monety, aby ukończyć poziom!");
            }
        }
    }

    // Metoda wywoływana, gdy gracz zostanie wyłączony
    void OnDisable ()
    {
        sb_disabled = true; // Zablokowanie możliwości sterowania postacią
    }

    void FixedUpdate ()
    {
        // Obrót postaci na podstawie wyliczonego kąta
        sb_rigibody.MoveRotation (Quaternion.Euler (Vector3.up * sb_angle));
        // Przesunięcie postaci na podstawie prędkości
        sb_rigibody.MovePosition (sb_rigibody.position + sb_velocity * Time.deltaTime);
    }

    // Metoda wywoływana, gdy obiekt gracza zostaje zniszczony
    void OnDestroy ()
    {
        // Odsubskrybowanie zdarzeń, aby uniknąć błędów
        Guard.OnGuardHasSpottedPlayer -= OnDisable;
        StationaryGuard.OnGuardHasSpottedPlayer -= OnDisable;
    }
}



