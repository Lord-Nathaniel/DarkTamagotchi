using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<GameManager>();
    }
}
