using System.Collections.Generic;
using Spawner;
using UnityEngine;
using Utils;

namespace CatMergeService
{
    public class CatMerger : MonoBehaviour
    {
        public CatSpawner CatSpawner;
        private Dictionary<string, int> counterCollisions = new();

        public void SubscribeCat(Cat cat)
        {
            cat.CollisionEvent += HandleCollision;
        }

        private void HandleCollision(CollisionData collision)
        {
            CatSpawner = ServiceLocator.Get<CatSpawner>();
            var selfCatType = collision.SelfCat.CatType;

            if (selfCatType.Equals(collision.OtherCat?.CatType))
            {
                counterCollisions.TryAdd(selfCatType, 0);

                counterCollisions[selfCatType] += 1;
                Destroy(collision.SelfCat.gameObject);

                if (counterCollisions[selfCatType] == 2)
                {
                    CatSpawner.SpawnAfterMerge(selfCatType, collision.SelfCat.transform);
                    counterCollisions[selfCatType] = 0;
                }
            }
        }
    }
}