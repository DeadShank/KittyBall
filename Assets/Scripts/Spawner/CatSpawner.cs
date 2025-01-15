using System;
using System.Collections.Generic;
using System.Linq;
using CatObjects.CatStates;
using UnityEngine;
using Random = System.Random;

namespace Spawner
{
    public class CatSpawner : MonoBehaviour
    {
        [SerializeField] private List<Cat> prefabs;
        [SerializeField] private Transform spawnPoint;
        
        private Random random = new Random();

        public event Action<Cat> SpawnEvent;
        
        public void SpawnCat()
        {
         
            int randomIndex = random.Next(prefabs.Count);
            var cat = Instantiate(prefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
            SpawnEvent?.Invoke(cat);
            cat.CollisionEvent += CollisionCallBack;
        }

        private void CollisionCallBack(CollisionData collision)
        {
            SpawnCat();
            collision.SelfCat.CollisionEvent -= CollisionCallBack;
        }

        public void SpawnAfterMerge(string cat, Transform transform)
        {
            int randomIndex = random.Next(prefabs.Count);
            foreach (var i in prefabs.Where(i => i.CatType == cat))
            {
                var counter = 0;
                ++counter;
                var newCat = Instantiate(prefabs[randomIndex], transform.position, transform.rotation);
                SpawnEvent?.Invoke(newCat);
                newCat.AfterMerge = true;
            }
        }
    }
}