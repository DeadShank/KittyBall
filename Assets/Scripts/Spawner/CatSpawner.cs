using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Spawner
{
    public class CatSpawner : MonoBehaviour
    {
        [SerializeField] private List<Cat> prefabs;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Image nextCatImage;

        private Random random = new Random();
        private Dictionary<string, int> catIndexMap = new Dictionary<string, int>();
        private int nextCatIndex;

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
            var cat = Instantiate(prefabs[nextCatIndex], spawnPoint.position, spawnPoint.rotation);
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
            if (newCatIndex < prefabs.Count - 1)
            {
                newCatIndex++;
                var newTransform = transform.position;
                newTransform.y += 1f;
                var newCat = Instantiate(prefabs[newCatIndex], newTransform, transform.rotation);
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
            nextCatIndex = random.Next(prefabs.Count);
            nextCatImage.color = prefabs[nextCatIndex].GetComponent<SpriteRenderer>().color;
            nextCatImage.sprite = prefabs[nextCatIndex].GetComponent<SpriteRenderer>().sprite;
        }
    }
}