using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private void Update()
    {
        if (InputManager.Instance.Interact)
        {
            InteractionEvents.RaiseInteract();
        }
    }
}
