using System;
using System.Collections.Generic;
using CatObjects.CatStates;
using ScoreService;
using UnityEngine;
using Utils;

public class Cat : FSMBehaviour<Cat>
{
    public string CatType;
    public bool AfterMerge;

    public TapController TapController;
    public ScoreCounter ScoreCounter;
    public event Action<CollisionData> CollisionEvent;

    protected override void InitStates()
    {
        TapController = ServiceLocator.Get<TapController>();
        ScoreCounter = ServiceLocator.Get<ScoreCounter>();

        var idle = new CatIdle(this);
        var aim = new CatAim(this);
        var moving = new CatMoving(this);
        var merge = new CatMerge(this);
        Transitions = new Dictionary<State<Cat>, State<Cat>>();
        Transitions[idle] = aim;
        Transitions[aim] = moving;
        Transitions[moving] = merge;
        if (AfterMerge)
        {
            ChangeState(moving);
        }
        else
        {
            ChangeState(idle);
        }
    }
    
    protected void OnDestroy()
    {
        DisposeCollisionEvent();
        ScoreCounter.AddScore(CatType);
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