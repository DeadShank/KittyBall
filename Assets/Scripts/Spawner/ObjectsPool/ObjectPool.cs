using System.Collections.Generic;
using UnityEngine;

namespace Spawner.ObjectsPool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] public List<Cat> prefabs;
        
        private readonly List<Cat> cats = new List<Cat>();

        public Cat GetCat(int index, Vector3 position, Transform transform)
        {
            Cat cat;
            if (cats.Count > 0)
            {
                cat = cats[0];
                cats.RemoveAt(0);
            }
            else
            {
                
                cat = Instantiate(prefabs[index], position, transform.rotation);

            }
            cat.Destroyed += ReturnCat;
            return cat;
        }

        public void ReturnCat(IPoolable poolable)
        {
            Cat cat = poolable as Cat;
            if (cat != null)
            {
                cat.Destroyed -= ReturnCat;
                cats.Add(cat);
            }
        }   
    }
}