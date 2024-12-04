using UnityEngine;

namespace CatObjects.CatStates
{
    public class CatAim : State<Cat>
    {
        private Transform objectTransform;
        private TapController tapController => StateTarget.TapController;

        public CatAim(Cat stateTarget) : base(stateTarget)
        {
            objectTransform = StateTarget.transform;
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
            var vector3 = objectTransform.position;
            vector3.x = tapController.ValueX;
            objectTransform.position = vector3;
            if (tapController.BlockObject)
            {
                Status = StateStatus.Exit;
            }
        }

        public override void Exit()
        {
        }
    }
}