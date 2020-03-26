using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        // Enemy stats
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private int _score = 100;
        [SerializeField] private int _coins = 25;
        [SerializeField] private int _damage = 1;

        private EnemyHealth _health;
        private NavMeshAgent _agent;
        private GameManager _gameManager;

        private bool _enemyHasAttacked;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _health = GetComponent<EnemyHealth>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.PlayerHouse)
            {
                Attack();
            }
        }

        /// <summary>
        /// Start walking the enemy towards the destination.
        /// </summary>
        /// <param name="target">The target which the enemy tries to reach.</param>
        public void MoveTowardsTarget(Vector3 target)
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _movementSpeed;
            _agent.destination = target;
        }

        /// <summary>
        /// Enemy has been killed. Add points and remove enemy from game.
        /// </summary>
        public void GrantEnemyResourcesToPlayer()
        {
            _gameManager.UpdateScore(_score);
            _gameManager.UpdateCoins(_coins, Operator.Add);
        }

        /// <summary>
        /// Enemy has reached the destination. Subtract a life from the player's health.
        /// Enemy dies but no points are rewarded.
        /// </summary>
        public void Attack()
        {
            if (_enemyHasAttacked) return;
            _enemyHasAttacked = true;
            _gameManager.UpdatePlayerHealth(_damage, Operator.Subtract);
            _health.Die();
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
    }
}