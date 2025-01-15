using Spawner;
using UnityEngine;
using Utils;

namespace CatMergeService
{
    public class CatMerger : MonoBehaviour
    {
        public CatSpawner CatSpawner;
        
        public void SubscribeCat(Cat cat)
        {
            cat.CollisionEvent += HandleCollision;
        }

        private void HandleCollision(CollisionData collision)
        {
            CatSpawner = ServiceLocator.Get<CatSpawner>();

            if (collision.SelfCat.CatType.Equals(collision.OtherCat?.CatType))
            {
                Destroy(collision.SelfCat.gameObject);
                Destroy(collision.OtherCat.gameObject);
                CatSpawner.SpawnAfterMerge(collision.SelfCat.CatType, collision.SelfCat.transform);
                
            }
        }
    }
}