using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool moveObject;
    public bool BlockObject;
    public float ValueX => TakePositionTap();

    public event Action ButtonUpEvent;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        moveObject = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Reset()
    {
        ButtonUpEvent?.Invoke();
        moveObject = false;
        BlockObject = true;
    }

    private float TakePositionTap()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float leftBorder = -0.85f;
        float rightBorder = 0.85f;
        worldPosition.x = Mathf.Clamp(worldPosition.x, leftBorder, rightBorder);
        return worldPosition.x;
    }
}