using CatMergeService;
using ScoreService;
using Spawner;
using UnityEngine;

namespace Utils
{
    public class RegistrationClass : MonoBehaviour
    {
        [SerializeField] private TapController tapController;
        [SerializeField] private CatSpawner catSpawner;
        [SerializeField] private CatMerger catMerger;
        [SerializeField] private ScoreCounter scoreCounter;
        private void Awake()
        {
            ServiceLocator.Add(tapController);
            ServiceLocator.Add(catSpawner);
            ServiceLocator.Add(catMerger);
            ServiceLocator.Add(scoreCounter);
            
            catSpawner.SpawnEvent += catMerger.SubscribeCat;
            catSpawner.SpawnRandomCat();
        }
    }
}