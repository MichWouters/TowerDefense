using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _coinsText;
        [SerializeField] private Text _healthText;
        [SerializeField] private Text _gameOverText;

        private int _score;
        private int _coins = 100;
        private int _lives = 3;

        private GameLoopController _gameController;

        private void Start()
        {
            InitializeUi();
            _gameController = GetComponent<GameLoopController>();
        }

        private void InitializeUi()
        {
            if (_scoreText == null || _coinsText == null || _healthText == null || _gameOverText == null)
            {
                Debug.LogError("One of the labels was not initialized");
            }

            if (_scoreText != null && _coinsText != null && _healthText != null)
            {
                _scoreText.text = $"Score: {_score}";
                _coinsText.text = $"Coins: {_coins}";
                _healthText.text = $"Lives: {_lives}";
            }
            else
            {
                Debug.LogWarning("One or more UI TextFields have not been initialized or found");
            }

            if (_gameOverText != null) _gameOverText.enabled = false;
        }

        public void UpdateCoins(int value, Operator operation)
        {
            switch (operation)
            {
                case Operator.Add:
                    _coins += value;
                    break;

                case Operator.Subtract:
                    _coins -= value;
                    break;

                default:
                    Debug.Log($"Action is not supported: {value}, {operation}");
                    break;
            }

            _coinsText.text = $"Coins: {_coins}";
        }

        public void UpdatePlayerHealth(int value, Operator operation)
        {
            switch (operation)
            {
                case Operator.Add:
                    _lives += value;
                    break;

                case Operator.Subtract:
                    if (_lives > 0)
                    {
                        _lives -= value;
                    }
                    else
                    {
                        GameOver();
                    }

                    break;

                default:
                    Debug.Log($"Action is not supported: {value}, {operation}");
                    break;
            }

            _healthText.text = $"Lives: {_lives}";
        }

        public void UpdateScore(int points)
        {
            _score += points;
            _scoreText.text = $"Score: {_score}";
        }

        public bool CanPurchaseTower(int price)
        {
            bool result = _coins - price >= 0;
            return result;
        }

        public void GameOver()
        {
            _gameOverText.enabled = true;
            StartCoroutine(WaitForSeconds(5));
        }

        public IEnumerator WaitForSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _gameController.RestartLevel();
        }
    }
}