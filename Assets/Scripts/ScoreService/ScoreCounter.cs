using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScoreService
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        public List<string> CatDiedList = new List<string>();
        private int score;

        public void AddScore(string catType)
        {
            CatDiedList.Add(catType);
            score = CatDiedList.Count;
            textMeshPro.text = score.ToString();
        }
        
    }
    
}