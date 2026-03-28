using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// This class manages the Cinamachine cameras and its transitions.
/// </summary>
public class CameraManager : MonoBehaviour
{
    [Header("Cameras List")]
    [SerializeField] private CinemachineCamera frontCamera;
    [SerializeField] private CinemachineCamera backCamera;
    [SerializeField] private CinemachineCamera cutsceneCamera;

    private CinemachineCamera currentCamera;

    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<CameraManager>();
    }

    /// <summary>
    /// Activate the selected camera.
    /// </summary>
    public void ActivateCamera(ActivableCamera activableCamera)
    {
        switch (activableCamera)
        {
            case ActivableCamera.FrontCamera:
                if (currentCamera != frontCamera)
                    ActivateCamera(frontCamera);
                break;
            case ActivableCamera.BackCamera:
                if (currentCamera != backCamera)
                    ActivateCamera(backCamera);
                break;
            case ActivableCamera.CutsceneCamera:
                if (currentCamera != cutsceneCamera)
                    ActivateCamera(cutsceneCamera);
                break;
        }
    }

    /// <summary>
    /// Switch the front camera to the back or vice-versa.
    /// </summary>
    public void SwitchFrontAndBackCamera()
    {
        if (currentCamera != frontCamera)
            ActivateCamera(frontCamera);
        else
            ActivateCamera(backCamera);
    }

    private void ActivateCamera(CinemachineCamera targetCam)
    {
        frontCamera.Priority = 0;
        backCamera.Priority = 0;
        cutsceneCamera.Priority = 0;

        targetCam.Priority = 100;
        currentCamera = targetCam;
    }
}

public enum ActivableCamera
{
    FrontCamera,
    BackCamera,
    CutsceneCamera
}