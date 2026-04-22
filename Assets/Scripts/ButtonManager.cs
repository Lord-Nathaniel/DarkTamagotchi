using DG.Tweening;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject middleButton;
    [SerializeField] GameObject rightButton;
    [SerializeField] GameObject rearButton;

    // Needed services
    private InputManager inputManager;
    private CameraManager cameraManager;

    private void Awake()
    {
        ServiceManager.Register(this);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<ButtonManager>();
    }

    private void Start()
    {
        inputManager = ServiceManager.Get<InputManager>();
        cameraManager = ServiceManager.Get<CameraManager>();

        inputManager.OnClick += ClickHoveredButton;
    }

    private void ClickHoveredButton()
    {
        Debug.Log("clickHoveredButton");
        Transform hoveredButtonTransform = cameraManager.GetTransformPointedByMouse();

        if (hoveredButtonTransform != null)
        {
            OnButtonClicked(hoveredButtonTransform);
        }
    }

    public void OnButtonClicked(Transform buttonTransform)
    {
        GameObject button = buttonTransform.gameObject;
        if (button == leftButton)
            DoLeftButtonAction();

        if (button == middleButton)
            DoMiddleButtonAction();

        if (button == rightButton)
            DoRightButtonAction();

        if (button == rearButton)
            DoRearButtonAction();
    }

    private void DoRearButtonAction()
    {
        ClickRearButtonAction(rearButton.transform);
    }

    private void DoRightButtonAction()
    {
        ClickFaceButtonAction(rightButton.transform);
    }

    private void DoMiddleButtonAction()
    {
        ClickFaceButtonAction(middleButton.transform);
    }

    private void DoLeftButtonAction()
    {
        ClickFaceButtonAction(leftButton.transform);
    }

    private void ClickFaceButtonAction(Transform buttonTransform)
    {
        float ZPosition = buttonTransform.position.z;
        Debug.Log(ZPosition);
        buttonTransform.DOMoveZ(ZPosition + 0.3f, 0.2f)
                       .SetEase(Ease.OutBounce)
                       .OnComplete(() => buttonTransform.DOMoveZ(ZPosition, 0.2f));
    }

    private void ClickRearButtonAction(Transform buttonTransform)
    {
        float ZPosition = buttonTransform.position.z;
        Debug.Log(ZPosition);
        buttonTransform.DOMoveZ(ZPosition - 0.3f, 0.2f)
                       .SetEase(Ease.OutBounce)
                       .OnComplete(() => buttonTransform.DOMoveZ(ZPosition, 0.2f));
    }
}
