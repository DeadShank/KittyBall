using UnityEngine;

namespace CatObjects.CatStates
{
    public class CatIdle : State<Cat>
    {
        private Rigidbody2D rigidbody2D;
        private TapController tapController => StateTarget.TapController;

        public CatIdle(Cat stateTarget) : base(stateTarget)
        {
            rigidbody2D = stateTarget.gameObject.GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            rigidbody2D.gravityScale = 0;
        }

        public override void Update()
        {
            if (tapController.moveObject)
            {
                Status = StateStatus.Exit;
            }
        }

        public override void Exit()
        {
        }
    }
}