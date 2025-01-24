using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScoreService
{
    public class ScoreCounter : MonoBehaviour
    {
        private Dictionary<string, int> scoreMap = new Dictionary<string, int>
        {
            {"RedCat", 1},
            {"YellowCat", 2},
            {"BlueCat", 3},
            {"BrownCat", 4},
            {"WhiteCat", 5}
        };
        private int scoreTotal;
        
        [SerializeField] private TextMeshProUGUI textMeshPro;

        public void AddScore(int score)
        {
            scoreTotal += score;
            
            textMeshPro.text = scoreTotal.ToString();
        }
        
    }
    
}