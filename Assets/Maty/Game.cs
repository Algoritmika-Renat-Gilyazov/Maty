using System.Collections.Generic;
using UnityEngine;

namespace Maty
{
    public enum Difficulty
    {
        PEACEFUL = 0,
        EASY = 1,
        NORMAL = 2,
        HARD = 3
    }
    public class Game : MonoBehaviour
    {
        public Difficulty difficulty;

        public byte notebooks;
        
        public List<Player> players;

        void Start()
        {
            if (PlayerPrefs.HasKey("session.difficulty"))
            {
                difficulty = (Difficulty)PlayerPrefs.GetInt("session.difficulty");
            }
            else
            {
                difficulty = Difficulty.NORMAL;
            }
        }
    }
}