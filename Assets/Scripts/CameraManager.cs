using Unity.Cinemachine;
using UnityEngine;

public class CNMCameraManager : MonoBehaviour
{
    [Header("Cameras List")]
    [SerializeField] private CinemachineCamera frontCamera;
    [SerializeField] private CinemachineCamera backCamera;

    private CinemachineCamera currentCamera;

    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<CNMCameraManager>();
    }

    /// <summary>
    /// Activate the camera to see the front of the Egg.
    /// </summary>
    public void ActivateFrontCamera()
    {
        if (currentCamera != frontCamera)
            ActivateCamera(frontCamera);
    }

    /// <summary>
    /// Activate the camera to see the back of the Egg.
    /// </summary>
    public void AcctivateBackCamera()
    {
        if (currentCamera != backCamera)
            ActivateCamera(backCamera);
    }

    private void ActivateCamera(CinemachineCamera targetCam)
    {
        frontCamera.Priority = 0;
        backCamera.Priority = 0;

        targetCam.Priority = 100;
        currentCamera = targetCam;
    }
}
