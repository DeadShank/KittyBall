using UnityEngine;
using UnityEngine.EventSystems;

public class TapController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool moveObject;
    public bool BlockObject;
    public float ValueX => TakePositionTap();

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
        moveObject = false;
        BlockObject = true;
    }

    private float TakePositionTap()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return worldPosition.x;
    }
}