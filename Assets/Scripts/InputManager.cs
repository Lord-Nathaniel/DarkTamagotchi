using System;
using UnityEngine;

/// <summary>
/// This class manages the keyboard and mouse input.
/// When the player interact with the game, it calls corresponding Actions.
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask buttonLayerMask;

    private Vector3 lastPosition;

    /// <summary>
    /// -IN- GameManager from Start() and OnDestroy()
    /// </summary>
    public event Action OnLeftClicked, OnRightArrowClicked, OnLeftArrowClicked;

    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnLeftClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            OnRightArrowClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            OnLeftArrowClicked?.Invoke();

        Transform transform = GetTransformPointedByMouse();
    }

    /// <summary>
    /// Gives the last mouse position.
    /// -IN- 
    /// </summary>
    public Transform GetTransformPointedByMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, buttonLayerMask))
        {
            //Debug.Log("[InputManager] Button hit !");
            //Debug.Log("Object hit : " + hit.transform.name);
            return hit.transform;
        }
        return null;
    }
}
