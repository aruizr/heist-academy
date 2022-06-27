using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Slider progress;

        public UnityEvent onLoadScene;

        private AsyncOperation _loading;

        public void RestartCurrentLevel()
        {
            var scene = SceneManager.GetActiveScene();
            Load(scene.name);
        }

        public void SwitchToScene(string sceneName)
        {
            Load(sceneName);
        }

        private void Load(string sceneName)
        {
            _loading = SceneManager.LoadSceneAsync(sceneName);
            onLoadScene?.Invoke();
            StartCoroutine(DisplayProgress());
        }

        private IEnumerator DisplayProgress()
        {
            progress.value = 0;
            while (!_loading.isDone)
            {
                progress.value = Mathf.Clamp01(_loading.progress / 0.9f);
                yield return null;
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}