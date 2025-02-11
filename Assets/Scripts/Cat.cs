using System;
using System.Collections.Generic;
using CatObjects.CatStates;
using ScoreService;
using UnityEngine;
using Utils;
using IPoolable = Spawner.ObjectsPool.IPoolable;

public class Cat : FSMBehaviour<Cat>, IPoolable
{
    public string CatType;
    public int CatScore;
    public bool AfterMerge;
    public bool ReadyToLose;

    public TapController TapController;
    public ScoreCounter ScoreCounter;
    public GameObject GameObject => gameObject;
    public event Action<CollisionData> CollisionEvent;
    public event Action<IPoolable> Destroyed;

    public void Reset()
    {
        Destroyed?.Invoke(this);
    }

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
        ScoreCounter.AddScore(CatScore);
        Reset();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        var collisionData = new CollisionData();
        collisionData.SelfCat = this;
        collisionData.OtherCat = other.gameObject.GetComponent<Cat>();
        CollisionEvent?.Invoke(collisionData);
        ReadyToLose = true;
    }

    public void DisposeCollisionEvent()
    {
        foreach (Delegate d in CollisionEvent.GetInvocationList())
        {
            CollisionEvent -= (Action<CollisionData>)d;
        }
    }
    
}