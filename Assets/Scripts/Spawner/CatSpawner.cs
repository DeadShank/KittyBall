using System;
using System.Collections.Generic;
using Spawner.ObjectsPool;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Spawner
{
    public class CatSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Image nextCatImage;
        [SerializeField] private ObjectPool objectPool;

        private Random random = new Random();
        private Dictionary<string, int> catIndexMap = new Dictionary<string, int>();
        private int nextCatIndex;

        public event Action<Cat> SpawnEvent;

        private void Awake()
        {
            for (var i = 0; i < objectPool.prefabs.Count; i++)
            {
                catIndexMap.Add(objectPool.prefabs[i].CatType, i);
            }
        }

        public void SpawnRandomCat()
        {
            var cat = objectPool.GetCat(nextCatIndex, spawnPoint.position, spawnPoint);
            SpawnEvent?.Invoke(cat);
            cat.CollisionEvent += CollisionCallBack;
            NextCatImage();
        }

        private void CollisionCallBack(CollisionData collision)
        {
            SpawnRandomCat();
            collision.SelfCat.CollisionEvent -= CollisionCallBack;
        }

        public void SpawnAfterMerge(string cat, Transform transform)
        {
            int newCatIndex = catIndexMap[cat];
            if (newCatIndex < objectPool.prefabs.Count - 1)
            {
                newCatIndex++;
                var newTransform = transform.position;
                newTransform.y += 1f;
                var newCat = objectPool.GetCat(nextCatIndex, newTransform, transform);
                SpawnEvent?.Invoke(newCat);
                newCat.AfterMerge = true;
                ActivateEffects(newCat);
            }
        }

        private static void ActivateEffects(Cat newCat)
        {
            newCat.GetComponent<ParticleSystem>().Play();
            newCat.GetComponent<AudioSource>().Play();
        }

        private void NextCatImage()
        {
            nextCatIndex = random.Next(objectPool.prefabs.Count);
            nextCatImage.color = objectPool.prefabs[nextCatIndex].GetComponent<SpriteRenderer>().color;
            nextCatImage.sprite = objectPool.prefabs[nextCatIndex].GetComponent<SpriteRenderer>().sprite;
        }
    }
}