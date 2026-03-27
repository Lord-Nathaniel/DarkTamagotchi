using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<InputManager>();
    }
}
