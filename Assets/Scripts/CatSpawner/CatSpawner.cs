using System;
using UnityEngine;

namespace CatSpawner
{
    public class CatSpawner : MonoBehaviour
    {
        [SerializeField] private Cat prefab;
        [SerializeField] private Transform spawnPoint;

        public event Action<Cat> SpawnEvent;

        public void SpawnCat()
        {
            var cat = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            SpawnEvent?.Invoke(cat);
            cat.CollisionEvent += CollisionCallBack;
        }

        private void CollisionCallBack(CollisionData collision)
        {
            SpawnCat();
            collision.SelfCat.CollisionEvent -= CollisionCallBack;
        }
    }
}