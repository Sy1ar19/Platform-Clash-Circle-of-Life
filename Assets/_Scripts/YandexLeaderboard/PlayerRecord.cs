using TMPro;
using UnityEngine;

namespace Assets._Scripts.Leaderboard
{
    public class PlayerRecord : Record
    {
        [SerializeField] private TMP_Text _rank;

        public void SetRank(int rank)
        {
            _rank.text = rank.ToString();
        }
    }
}