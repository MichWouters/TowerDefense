using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class EnemyHealth : MonoBehaviour
    {
        // Parameters
        [SerializeField] private readonly float _health = 100f;
        [SerializeField] private Image _fillImage;
        [SerializeField] private Slider _slider;
        [SerializeField] private Color _fullHealthColor = Color.green;
        [SerializeField] private Color _zeroHealthColor = Color.red;

        // State
        private float _currentHealth;

        private bool _isAlive = true;

        // Unity components
        private Animator _anim;
        private NavMeshAgent _agent;
        private Enemy _enemy;

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _enemy = GetComponent<Enemy>();
            _currentHealth = _health;

            UpdateHealthUi();
        }

        public void Die()
        {
            if (!_isAlive) return;
            _isAlive = false;

            _anim.SetBool(Tags.Dead, true);
            _agent.updatePosition = false;

            Destroy(gameObject, 3f);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            UpdateHealthUi();

            if (_currentHealth <= 0)
            {
                Die();
                _enemy.GrantEnemyResourcesToPlayer();
            }
        }

        public bool IsAlive()
        {
            return _isAlive;
        }

        private void UpdateHealthUi()
        {
            _slider.value = _currentHealth;
            float healthPercentage = _currentHealth / _health;
            _fillImage.color = Color.Lerp(_zeroHealthColor, _fullHealthColor, healthPercentage);
        }
    }
}