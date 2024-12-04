using UnityEngine;

namespace CatMergeService
{
    public class CatMerger : MonoBehaviour
    {
        public void SubscribeCat(Cat cat)
        {
            cat.CollisionEvent += HandleCollision;
        }

        private void HandleCollision(CollisionData collision)
        {
            if (collision.SelfCat.CatType.Equals(collision.OtherCat?.CatType))
            {
                Debug.Log($"merge: {collision.SelfCat.CatType}");
                Destroy(collision.SelfCat.gameObject);
                Destroy(collision.OtherCat.gameObject);
            }
        }
    }
}