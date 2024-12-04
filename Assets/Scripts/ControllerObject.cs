using UnityEngine;
using Utils;

public class ControllerObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
     private TapController tapController;
    private float valueX => tapController.ValueX;
    private bool offControllObject => tapController.BlockObject;

    private void Start()
    {
        tapController = ServiceLocator.Get<TapController>();
    }

    private void Update()
    {
        var vector3 = transform.position;
        vector3.x = valueX;
        transform.position = vector3;
        if (offControllObject)
        {
            ActivateRigidbody2D();
        }
    }

    private void ActivateRigidbody2D()
    {
        rigidbody2D.gravityScale = 1;
    }
}