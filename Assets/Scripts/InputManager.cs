using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class manages the keyboard and mouse input.
/// When the player interact with the game, it calls corresponding Actions.
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask buttonLayerMask;

    [Header("Navigation")]
    public InputActionReference navigateLeft;
    public InputActionReference navigateRight;

    [Header("Interaction")]
    public InputActionReference click;
    public InputActionReference interactLeft;
    public InputActionReference interactMiddle;
    public InputActionReference interactRight;
    public InputActionReference interactBack;

    public event Action OnNavigateLeft, OnNavigateRight;
    public event Action OnClick, OnInteractLeft, OnInteractMiddle, OnInteractRight, OnInteractBack;

    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<GameManager>();
    }

    private void OnEnable()
    {
        navigateLeft.action.started += HandleNavigateLeft;
        navigateRight.action.started += HandleNavigateRight;

        click.action.started += HandleClick;
        interactLeft.action.started += HandleInteractLeft;
        interactMiddle.action.started += HandleInteractMiddle;
        interactRight.action.started += HandleInteractRight;
        interactBack.action.started += HandleInteractBack;
    }

    private void OnDisable()
    {
        navigateLeft.action.started -= HandleNavigateLeft;
        navigateRight.action.started -= HandleNavigateRight;

        click.action.started -= HandleClick;
        interactLeft.action.started -= HandleInteractLeft;
        interactMiddle.action.started -= HandleInteractMiddle;
        interactRight.action.started -= HandleInteractRight;
        interactBack.action.started -= HandleInteractBack;
    }

    private void HandleClick(InputAction.CallbackContext ctx)
        => OnClick?.Invoke();

    private void HandleNavigateLeft(InputAction.CallbackContext ctx)
        => OnNavigateLeft?.Invoke();

    private void HandleNavigateRight(InputAction.CallbackContext ctx)
        => OnNavigateRight?.Invoke();

    private void HandleInteractLeft(InputAction.CallbackContext ctx)
        => OnInteractLeft?.Invoke();

    private void HandleInteractMiddle(InputAction.CallbackContext ctx)
        => OnInteractMiddle?.Invoke();

    private void HandleInteractRight(InputAction.CallbackContext ctx)
        => OnInteractRight?.Invoke();

    private void HandleInteractBack(InputAction.CallbackContext ctx)
        => OnInteractBack?.Invoke();
}
