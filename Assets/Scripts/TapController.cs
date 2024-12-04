using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class TapController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool moveObject;
    public bool BlockObject;
    public float ValueX => TakePositionTap();

    private CatSpawner.CatSpawner _catSpawner;

    private void Start()
    {
        _catSpawner = ServiceLocator.Get<CatSpawner.CatSpawner>();
    }

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
        if (moveObject)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            return worldPosition.x;
        }

        return 0f;
    }
}