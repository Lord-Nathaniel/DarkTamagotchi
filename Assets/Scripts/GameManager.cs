using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int inputReqToChangeCamera = 3;
    [SerializeField] float maxTimeToChangeCamera = 4f;

    private float time = 0f;
    private int currentInputNb = 0;
    private CountableInput currentInputType;

    // Needed services
    private InputManager inputManager;
    private CameraManager cameraManager;
    private ButtonManager buttonManager;

    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<GameManager>();

        //inputManager.OnLeftClicked -= PushButton;
        //inputManager.OnRightArrowClicked -= SwitchCameraToRight;
        //inputManager.OnLeftArrowClicked -= SwitchCameraToLeft;
    }

    private void Start()
    {
        inputManager = ServiceManager.Get<InputManager>();
        cameraManager = ServiceManager.Get<CameraManager>();
        buttonManager = ServiceManager.Get<ButtonManager>();

        //inputManager.OnLeftClicked += PushButton;
        //inputManager.OnRightArrowClicked += SwitchCameraToRight;
        //inputManager.OnLeftArrowClicked += SwitchCameraToLeft;
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

    private void ResetCounters()
    {
        time = 0f;
        currentInputNb = 0;
    }

    private void SwitchCameraToLeft()
    {
        if (RegisterInput(CountableInput.LeftArrow))
            cameraManager.SwitchFrontAndBackCamera(ActivableCamera.LeftCamera);
    }

    private void SwitchCameraToRight()
    {
        if (RegisterInput(CountableInput.RightArrow))
            cameraManager.SwitchFrontAndBackCamera(ActivableCamera.RightCamera);
    }

    private void PushButton()
    {
        //Transform transform = inputManager.GetTransformPointedByMouse();

        if (transform != null)
        {
            buttonManager.OnButtonClicked(transform);
        }
    }

    private bool RegisterInput(CountableInput inputType)
    {
        if (currentInputType != inputType)
        {
            currentInputNb = 0;
            currentInputType = inputType;
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

public enum CountableInput
{
    RightArrow,
    LeftArrow
}
