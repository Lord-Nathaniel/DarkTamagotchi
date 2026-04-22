using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class manages the Cinamachine cameras and its transitions.
/// </summary>
public class CameraManager : MonoBehaviour
{
    [Header("Cameras List")]
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private CinemachineCamera frontCamera;
    [SerializeField] private CinemachineCamera backCamera;
    [SerializeField] private CinemachineCamera cutsceneCamera;
    [SerializeField] private CinemachineCamera rightCamera;
    [SerializeField] private CinemachineCamera leftCamera;

    [SerializeField] private LayerMask buttonLayerMask;

    [SerializeField] private float blendDuration = 0.5f;

    private CinemachineCamera currentCamera;
    private IEnumerator coroutine;
    private List<CinemachineCamera> cameras = new();

    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<CameraManager>();
    }

    private void Start()
    {
        SetupCameras();
    }

    private void SetupCameras()
    {
        cameras.Add(frontCamera);
        cameras.Add(backCamera);
        cameras.Add(cutsceneCamera);
        cameras.Add(rightCamera);
        cameras.Add(leftCamera);

        currentCamera = frontCamera;
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

    private void ActivateCamera(CinemachineCamera targetCam)
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 0;
        }

        targetCam.Priority = 100;
        currentCamera = targetCam;
    }

    /// <summary>
    /// Switch the front camera to the back or vice-versa.
    /// </summary>
    public void SwitchFrontAndBackCamera(DirectionType direction)
    {
        ActivableCamera activableCamera = ActivableCamera.LeftCamera;

        if (direction == DirectionType.Left)
            activableCamera = ActivableCamera.RightCamera;

        StartCoroutine(SwitchRoutine(activableCamera));
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

    /// <summary>
    /// Gives the last mouse position.
    /// -IN- 
    /// </summary>
    public Transform GetTransformPointedByMouse()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, buttonLayerMask))
        {
            return hit.transform;
        }
        return null;
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