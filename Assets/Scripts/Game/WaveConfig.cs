using UnityEngine;

namespace Assets.Scripts.Game
{
    [CreateAssetMenu(menuName = "Enemy Wave Config")]
    public class WaveConfig: ScriptableObject
    {
        [SerializeField] private GameObject _enemyToSpawn;
        [SerializeField] private float _timeBetweenSpawns = .5f;
        [SerializeField] private int _numberOfEnemies = 5;
        [SerializeField] private float _moveSpeed = 2f;

        public GameObject GetEnemyPrefab()
        {
            return _enemyToSpawn;
        }

        public float GetTimeBetweenSpawns()
        {
            return _timeBetweenSpawns;
        }

        public int GetNumberOfEnemies()
        {
            return _numberOfEnemies;
        }

        public float GetMoveSpeed()
        {
            return _moveSpeed;
        }
}
}
