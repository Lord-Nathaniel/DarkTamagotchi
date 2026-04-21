using DG.Tweening;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject middleButton;
    [SerializeField] GameObject rightButton;
    [SerializeField] GameObject rearButton;

    private void Awake()
    {
        ServiceManager.Register(this);
        Debug.Log("Left button : " + leftButton.transform.position.y);
        Debug.Log(middleButton.transform.position.y);
        Debug.Log(rightButton.transform.position.y);
        Debug.Log(rearButton.transform.position.y);
    }

    private void OnDestroy()
    {
        ServiceManager.Unregister<ButtonManager>();
    }

    public void OnButtonClicked(Transform buttonTransform)
    {
        if (buttonTransform.gameObject == leftButton)
            DoLeftButtonAction();

        if (buttonTransform.gameObject == middleButton)
            DoMiddleButtonAction();

        if (buttonTransform.gameObject == rightButton)
            DoRightButtonAction();

        if (buttonTransform.gameObject == rearButton)
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
        float YPosition = buttonTransform.position.y;
        Debug.Log(YPosition);
        buttonTransform.DOMoveZ(YPosition + 0.3f, 0.2f)
                       .SetEase(Ease.OutBounce)
                       .OnComplete(() => buttonTransform.DOMoveZ(YPosition, 0.2f));
    }

    private void ClickRearButtonAction(Transform buttonTransform)
    {
        float YPosition = buttonTransform.position.y;
        Debug.Log(YPosition);
        buttonTransform.DOMoveZ(YPosition - 0.3f, 0.2f)
                       .SetEase(Ease.OutBounce)
                       .OnComplete(() => buttonTransform.DOMoveZ(YPosition, 0.2f));
    }
}
