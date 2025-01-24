using UnityEngine;

namespace LoseLine
{
    public class LoseLineScript : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Cat>().ReadyToLose)
            {
                Debug.Log("Game Over");
            }
        }
    }
}