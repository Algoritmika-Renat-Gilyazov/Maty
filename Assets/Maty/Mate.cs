using UnityEngine;
using UnityEngine.AI;

namespace Maty
{
    public class Mate : Character
    {
        public float slapCooldown = 3f;
        public float stepLength = 6f;
        public float maxFindDistance = 50f;
        public float wanderRadius = 10f;
        public bool isSlapping;
        public float speed;

        private float currentSlapCooldown;
        private Player targetPlayer;

        protected override void Start()
        {
            characterName = "Mate";

            agent.speed = moveSpeed;
            agent.acceleration = 200f;
            agent.angularSpeed = 720f;
            agent.autoBraking = false;
            agent.stoppingDistance = 0.1f;
            agent.updatePosition = false;
            agent.updateRotation = false;

            currentSlapCooldown = slapCooldown;

            if (game != null && game.players != null && game.players.Count > 0)
                targetPlayer = game.players[0];

            base.Start();
        }

        protected void Update()
        {
            if (!isSlapping) return;

            currentSlapCooldown -= Time.deltaTime;
            if (currentSlapCooldown <= 0f)
            {
                currentSlapCooldown = slapCooldown;
                MakeStep();
            }
        }

        protected void MakeStep()
        {
            if (targetPlayer is null && game is not null && game.players != null && game.players.Count > 0)
                targetPlayer = game.players[0];

            Vector3 desiredTarget;

            if (targetPlayer is not null &&
                Vector3.Distance(transform.position, targetPlayer.transform.position) <= maxFindDistance)
            {
                Vector3 toPlayer = targetPlayer.transform.position - transform.position;
                toPlayer.y = 0f;

                Vector3 direction = toPlayer.normalized;
                float stepDist = Mathf.Min(stepLength, toPlayer.magnitude);

                desiredTarget = transform.position + direction * stepDist;
            }
            else
            {
                Vector2 rnd = Random.insideUnitCircle * wanderRadius;
                desiredTarget = transform.position + new Vector3(rnd.x, 0f, rnd.y);
            }

            if (NavMesh.SamplePosition(desiredTarget, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                desiredTarget = hit.position;
            else
                return;

            transform.position = desiredTarget;
            agent.nextPosition = desiredTarget;

            Vector3 lookDir = desiredTarget - transform.position;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(lookDir);
        }

        public void StartSlapping()
        {
            isSlapping = true;
            currentSlapCooldown = 0f;
        }

        public void StopSlapping()
        {
            isSlapping = false;
        }
    }
}