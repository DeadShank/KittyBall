using UnityEngine;

namespace CatObjects.CatStates
{
    public class CatMoving : State<Cat>
    {
        private Rigidbody2D rigidbody2D;

        public CatMoving(Cat stateTarget) : base(stateTarget)
        {
            rigidbody2D = stateTarget.gameObject.GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            rigidbody2D.gravityScale = 1;
        }

        public override void Update()
        {
           
        }
        
        public override void Exit()
        {
        }
    }
}