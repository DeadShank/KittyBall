using System;
using System.Collections.Generic;
using CatObjects.CatStates;
using UnityEngine;
using Utils;

public class Cat : FSMBehaviour<Cat>
{
    public string CatType;

    public TapController TapController;
    public event Action<CollisionData> CollisionEvent;

    protected override void InitStates()
    {
        TapController = ServiceLocator.Get<TapController>();

        var idle = new CatIdle(this);
        var aim = new CatAim(this);
        var moving = new CatMoving(this);
        var merge = new CatMerge(this);
        Transitions = new Dictionary<State<Cat>, State<Cat>>();
        Transitions[idle] = aim;
        Transitions[aim] = moving;
        Transitions[moving] = merge;
        ChangeState(idle);
    }
        
    protected void OnDestroy()
    {
        DisposeCollisionEvent();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var collisionData = new CollisionData();
        collisionData.SelfCat = this;
        collisionData.OtherCat = other.gameObject.GetComponent<Cat>();
        CollisionEvent?.Invoke(collisionData);
    }

    public void DisposeCollisionEvent()
    {
        foreach (Delegate d in CollisionEvent.GetInvocationList())
        {
            CollisionEvent -= (Action<CollisionData>)d;
        }
    }
}