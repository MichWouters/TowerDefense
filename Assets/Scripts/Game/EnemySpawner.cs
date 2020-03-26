using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> _waveSpawners;
        [SerializeField] private bool _isLooping = false;
        [SerializeField] private Transform _destination;
        [SerializeField] private float _secondsBetweenWaves = 20;

        private IEnumerator Start()
        {
            do
            {
                yield return StartCoroutine(SpawnAllWaves());
            } while (_isLooping);
        }

        private IEnumerator SpawnAllWaves()
        {
            foreach (WaveConfig currentWave in _waveSpawners)
            {
                yield return StartCoroutine(SpawnAllEnemiesInWaves(currentWave));
                yield return new WaitForSeconds(_secondsBetweenWaves);
            }
        }

        private IEnumerator SpawnAllEnemiesInWaves(WaveConfig wave)
        {
            for (int i = 0; i < wave.GetNumberOfEnemies(); i++)
            {
                var enemy = Instantiate(
                    wave.GetEnemyPrefab(),
                    transform.position,
                    Quaternion.identity);

                Enemy enemyController = enemy.GetComponent<Enemy>();
                enemyController.MoveTowardsTarget(_destination.position);

                yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
            }

        }
    }
}