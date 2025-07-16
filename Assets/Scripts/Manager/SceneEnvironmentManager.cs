using UnityEngine;

public class SceneEnvironmentManager : MonoBehaviour
{
    [SerializeField] private SceneEnvironmentSO environmentSO;

    private void Start()
    {
        if (environmentSO != null)
        {
            PlayerEvents.RaiseChangeMovementMode(environmentSO.defaultMovementMode);
        }
        else
        {
            Debug.LogWarning("SceneEnvironmentSO is not assigned in SceneEnvironmentManager.");
        }
    }
}
