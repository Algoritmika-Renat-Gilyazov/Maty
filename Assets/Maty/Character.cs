using UnityEngine;
using UnityEngine.AI;

namespace Maty
{
    public class Character : MonoBehaviour
    {
        public Game game;
        public string characterName;
        [SerializeField]
        protected Sprite characterSprite;
        [SerializeField]
        protected Rigidbody rb;
        [SerializeField]
        protected NavMeshAgent agent;

        public float moveSpeed;
        
        protected virtual void Start()
        {
            print($"Character '{characterName}' Start");
        }
    }
}