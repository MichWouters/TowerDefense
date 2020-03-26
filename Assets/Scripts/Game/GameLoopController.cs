using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{

    public class GameLoopController : MonoBehaviour
    {
        [SerializeField] private float _startDelay = 2f;
        [SerializeField] private float _endDelay = 5f;

        private WaitForSeconds _startWait;
        private WaitForSeconds _endWait;
        private bool _gameRunning = true;


        public void Start()
        {
        }

        public IEnumerator RoundStarting()
        {
            yield return _startWait;
        }

        private IEnumerator RoundPlaying()
        {
            while (_gameRunning)
            {
                yield return null;
            }
        }

        public IEnumerator RoundEnding()
        {
            yield return _endWait;
        }

        public IEnumerator WaitSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

        private IEnumerator GameLoop()
        {
            // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
            yield return StartCoroutine(WaitSeconds(_startDelay));

            // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
            yield return StartCoroutine(RoundPlaying());

            // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
            StartCoroutine(WaitSeconds(_endDelay));
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
