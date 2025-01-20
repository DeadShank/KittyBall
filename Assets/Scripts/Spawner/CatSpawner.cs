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
        private Dictionary<string, int> catIndexMap = new Dictionary<string, int>();

        public event Action<Cat> SpawnEvent;

        private void Awake()
        {
            for (var i = 0; i < prefabs.Count; i++)
            {
                catIndexMap.Add(prefabs[i].CatType, i);
            }
        }

        public void SpawnRandomCat()
        {
            int randomIndex = random.Next(prefabs.Count);
            var cat = Instantiate(prefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
            SpawnEvent?.Invoke(cat);
            cat.CollisionEvent += CollisionCallBack;
        }

        private void CollisionCallBack(CollisionData collision)
        {
            SpawnRandomCat();
            collision.SelfCat.CollisionEvent -= CollisionCallBack;
        }

        public void SpawnAfterMerge(string cat, Transform transform)
        {
            int newCatIndex = catIndexMap[cat];
            newCatIndex++;
            var newCat = Instantiate(prefabs[newCatIndex], transform.position, transform.rotation);
            SpawnEvent?.Invoke(newCat);
            newCat.AfterMerge = true;
        }
    }
}