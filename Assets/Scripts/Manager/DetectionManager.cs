using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private float detectionAngle = 45f;
    [SerializeField] private float detectionFrequency = 0.1f;
    [SerializeField] private int rayCount = 5;

    [Header("Camera Reference")]
    [SerializeField] private Transform playerCamera;

    private float timer;
    private IDetectable currentDetected;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= detectionFrequency)
        {
            timer = 0f;
            PerformDetection();
        }
    }

    private void OnEnable()
    {
        InteractionEvents.OnInteract += HandleInteract;
    }

    private void OnDisable()
    {
        InteractionEvents.OnInteract -= HandleInteract;
    }

    private void HandleInteract()
    {
        TryInteractWithCurrentTarget();
    }

    private void TryInteractWithCurrentTarget()
    {
        if (currentDetected != null)
        {
            currentDetected.Interact();
        }
    }

    private void PerformDetection()
    {
        currentDetected = null;
        bool detectedSomething = false;

        float step = detectionAngle / (rayCount - 1);
        float startAngle = -detectionAngle / 2f;

        for (int i = 0; i < rayCount; i++)
        {
            float angleOffset = startAngle + (step * i);
            Quaternion rotation = Quaternion.Euler(0f, angleOffset, 0f);
            Vector3 direction = rotation * playerCamera.forward;

            if (Physics.Raycast(playerCamera.position, direction, out RaycastHit hit, detectionRange))
            {
                IDetectable detectable = hit.collider.GetComponent<IDetectable>();
                if (detectable != null)
                {
                    currentDetected = detectable;

                    // 🔔 Call centralized event
                    DetectionEvents.RaiseDetect(detectable.GetDisplayName());

                    // If you have a fact system:
                    // if (detectable is Garbage garbage)
                    //     DetectionEvents.RaiseFactDetected(garbage.GetFact());

                    Debug.DrawRay(playerCamera.position, direction * detectionRange, Color.green, 0.1f);
                    return;
                }
            }

            Debug.DrawRay(playerCamera.position, direction * detectionRange, Color.red, 0.1f);
        }

        if (!detectedSomething)
        {
            DetectionEvents.RaiseDetectCleared();
            // DetectionEvents.RaiseFactCleared();
        }
    }

    public IDetectable GetCurrentDetected()
    {
        return currentDetected;
    }

    private void OnDrawGizmosSelected()
    {
        if (Camera.main == null) return;

        Gizmos.color = Color.yellow;
        Vector3 origin = Camera.main.transform.position;
        Vector3 forward = Camera.main.transform.forward;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-detectionAngle / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(detectionAngle / 2, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * forward;
        Vector3 rightRayDirection = rightRayRotation * forward;

        Gizmos.DrawRay(origin, leftRayDirection * detectionRange);
        Gizmos.DrawRay(origin, rightRayDirection * detectionRange);
        Gizmos.DrawWireSphere(origin, detectionRange);
    }
}
