using UnityEngine;

public class GameKeyManager : MonoBehaviour
{
    [SerializeField] int inputReqToChangeCamera = 3;
    [SerializeField] float maxTimeToChangeCamera = 4f;

    private float time = 0f;
    private int currentInputNb = 0;
    private DirectionType currentInputDirection;

    // Needed services
    private InputManager inputManager;
    private CameraManager cameraManager;

    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<GameKeyManager>();

        inputManager.OnNavigateDirection -= SwitchCameraByNaviRgationDirection;
    }

    private void Start()
    {
        inputManager = ServiceManager.Get<InputManager>();
        cameraManager = ServiceManager.Get<CameraManager>();

        inputManager.OnNavigateDirection += SwitchCameraByNaviRgationDirection;
    }

    private void Update()
    {
        if (currentInputNb > 0)
        {
            time += Time.deltaTime;
        }

        if (time > maxTimeToChangeCamera)
        {
            ResetCounters();
        }
    }

    private void SwitchCameraByNaviRgationDirection(DirectionType direction)
    {
        if (RegisterInput(direction))
            cameraManager.SwitchFrontAndBackCamera(direction);
    }

    private void ResetCounters()
    {
        time = 0f;
        currentInputNb = 0;
    }

    private bool RegisterInput(DirectionType direction)
    {
        if (currentInputDirection != direction)
        {
            currentInputNb = 0;
            currentInputDirection = direction;
        }
        else if (currentInputNb == 0)
        {
            time = 0f;
        }

        currentInputNb++;

        if (currentInputNb >= inputReqToChangeCamera)
        {
            ResetCounters();
            return true;
        }
        return false;
    }
}