using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class manages the keyboard and mouse inputs.
/// When the player interact with the game, it calls corresponding Actions.
/// </summary>
public class InputManager : MonoBehaviour
{
    [Header("Navigation")]
    public InputActionReference navigateLeft;
    public InputActionReference navigateRight;

    [Header("Interaction")]
    public InputActionReference click;
    public InputActionReference interactLeft;
    public InputActionReference interactMiddle;
    public InputActionReference interactRight;
    public InputActionReference interactBack;

    public event Action<DirectionType> OnNavigateDirection;
    public event Action OnClick;
    public event Action<KeyType> OnDirectKeyPressed;

    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<InputManager>();
    }

    private void OnEnable()
    {
        navigateLeft.action.performed += HandleNavigateLeft;
        navigateRight.action.performed += HandleNavigateRight;

        click.action.performed += HandleClick;
        interactLeft.action.performed += HandleInteractLeft;
        interactMiddle.action.performed += HandleInteractMiddle;
        interactRight.action.performed += HandleInteractRight;
        interactBack.action.performed += HandleInteractBack;
    }

    private void OnDisable()
    {
        navigateLeft.action.performed -= HandleNavigateLeft;
        navigateRight.action.performed -= HandleNavigateRight;

        click.action.performed -= HandleClick;
        interactLeft.action.performed -= HandleInteractLeft;
        interactMiddle.action.performed -= HandleInteractMiddle;
        interactRight.action.performed -= HandleInteractRight;
        interactBack.action.performed -= HandleInteractBack;
    }

    private void HandleClick(InputAction.CallbackContext ctx)
        => OnClick?.Invoke();

    private void HandleNavigateLeft(InputAction.CallbackContext ctx)
        => OnNavigateDirection?.Invoke(DirectionType.Left);

    private void HandleNavigateRight(InputAction.CallbackContext ctx)
        => OnNavigateDirection?.Invoke(DirectionType.Right);

    private void HandleInteractLeft(InputAction.CallbackContext ctx)
        => OnDirectKeyPressed?.Invoke(KeyType.Left);

    private void HandleInteractMiddle(InputAction.CallbackContext ctx)
        => OnDirectKeyPressed?.Invoke(KeyType.Middle);

    private void HandleInteractRight(InputAction.CallbackContext ctx)
        => OnDirectKeyPressed?.Invoke(KeyType.Right);

    private void HandleInteractBack(InputAction.CallbackContext ctx)
        => OnDirectKeyPressed?.Invoke(KeyType.Back);
}

public enum KeyType
{
    Left,
    Middle,
    Right,
    Back
}

public enum DirectionType
{
    Left,
    Right
}
