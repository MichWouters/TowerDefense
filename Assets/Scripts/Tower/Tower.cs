using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

namespace Assets.Scripts.Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float _range = 7.0f;
        [SerializeField] private float _reloadTime = 5.0f;
        [SerializeField] private int _price = 25;
        [SerializeField] private TowerType _towerType;
        [SerializeField] private GameObject _missile;
        [SerializeField] private Transform _barrel;

        private Queue<GameObject> _targets;
        private float _nextTimeToFire;
        private Transform _targetEnemy;

        private void Start()
        {
            _targets = new Queue<GameObject>();
        }

        private void Update()
        {
            SetTargetEnemy();

            if (_targetEnemy)
            {
                AimAtEnemy();
                Fire();
            }
        }

        private void SetTargetEnemy()
        {
            var allEnemies = FindObjectsOfType<Enemy>();

            if (allEnemies.Length == 0)
            {
                return;
            }

            Transform closestEnemy = allEnemies[0].transform;

            foreach (Enemy enemy in allEnemies)
            {
                closestEnemy = GetClosestEnemy(closestEnemy, enemy.transform);
            }

            _targetEnemy = closestEnemy;
        }

        private Transform GetClosestEnemy(Transform closestEnemy, Transform enemyTransform)
        {
            Vector3 currentPosition = gameObject.transform.position;

            return Vector3.Distance(closestEnemy.transform.position, currentPosition) <
                   Vector3.Distance(enemyTransform.transform.position, currentPosition)
                ? closestEnemy.transform : enemyTransform.transform;
        }


        private void AimAtEnemy()
        {
            if (_targetEnemy)
            {
                transform.LookAt(_targetEnemy);
            }
        }

        private void Fire()
        {
            if (Time.time > _nextTimeToFire)
            {
                GameObject bullet = Instantiate(_missile, _barrel.position, Quaternion.identity);
                Missile missile = bullet.GetComponent<Missile>();

                missile.Target = _targetEnemy.gameObject;
                _nextTimeToFire = Time.time + _reloadTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.Enemy)
            {
                _targets.Enqueue(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != Tags.Enemy) return;
            _targets.Dequeue();
        }

        public TowerType GetTowerType()
        {
            return _towerType;
        }

        public int GetPrice()
        {
            return _price;
        }

    }
}