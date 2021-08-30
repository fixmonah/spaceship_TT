using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfoPanel : MonoBehaviour
{
    [SerializeField] private RectTransform parentRectTransform;
    [SerializeField] private RectTransform textPanelRectTransform;
    private float parentYRange;
    private float textPanelYRange;
    private float trashDold;

    private RectTransform rectTransform;

    void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        parentYRange = Mathf.Abs(parentRectTransform.rect.yMin);
        textPanelYRange = Mathf.Abs(textPanelRectTransform.rect.yMin);

        trashDold = parentYRange - textPanelYRange;
    }

    public void ShowPopUp(RectTransform objectRectTransform) 
    {
        float newYPosition = objectRectTransform.position.y;
        Vector3 newPosition = rectTransform.position;
        newPosition.y = newYPosition;
        rectTransform.position = newPosition;

        NewPositionTestPanel();
    }

    private void NewPositionTestPanel()
    {
        float positionY = rectTransform.localPosition.y;

        float newY;
        if (positionY > trashDold)
        {
            newY = (positionY - trashDold) * -1;
        }
        else if (positionY < -trashDold)
        {
            newY = -1 * (positionY) - trashDold;
        }
        else
        {
            newY = 0;
        }
        Vector3 pos = textPanelRectTransform.localPosition;
        pos.y = newY;
        textPanelRectTransform.localPosition = pos;
    }
}
