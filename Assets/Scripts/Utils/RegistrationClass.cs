using CatMergeService;
using Spawner;
using UnityEngine;

namespace Utils
{
    public class RegistrationClass : MonoBehaviour
    {
        [SerializeField] private TapController tapController;
        [SerializeField] private CatSpawner catSpawner;
        [SerializeField] private CatMerger catMerger;
        private void Awake()
        {
            ServiceLocator.Add(tapController);
            ServiceLocator.Add(catSpawner);
            ServiceLocator.Add(catMerger);

            catSpawner.SpawnEvent += catMerger.SubscribeCat;
            catSpawner.SpawnCat();
        }
    }
}