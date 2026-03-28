using System.Collections;
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
    [SerializeField] private CinemachineCamera rightCamera;
    [SerializeField] private CinemachineCamera leftCamera;

    [SerializeField] private float blendDuration = 0.5f;

    private CinemachineCamera currentCamera;
    private IEnumerator coroutine;

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
            case ActivableCamera.RightCamera:
                if (currentCamera != rightCamera)
                    ActivateCamera(rightCamera);
                break;
            case ActivableCamera.LeftCamera:
                if (currentCamera != leftCamera)
                    ActivateCamera(leftCamera);
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
    public void SwitchFrontAndBackCamera(ActivableCamera activableCamera)
    {
        StartCoroutine(SwitchRoutine(activableCamera));
    }

    private void ActivateCamera(CinemachineCamera targetCam)
    {
        frontCamera.Priority = 0;
        backCamera.Priority = 0;
        cutsceneCamera.Priority = 0;
        rightCamera.Priority = 0;
        leftCamera.Priority = 0;

        targetCam.Priority = 100;
        currentCamera = targetCam;
    }

    private IEnumerator SwitchRoutine(ActivableCamera activableCamera)
    {
        ActivableCamera endTransitionCamera;
        if (currentCamera != frontCamera)
            endTransitionCamera = ActivableCamera.FrontCamera;
        else
            endTransitionCamera = ActivableCamera.BackCamera;

        ActivateCamera(activableCamera);

        yield return new WaitForSeconds(blendDuration);

        ActivateCamera(endTransitionCamera);
    }
}

public enum ActivableCamera
{
    FrontCamera,
    BackCamera,
    CutsceneCamera,
    RightCamera,
    LeftCamera
}