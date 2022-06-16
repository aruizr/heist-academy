using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;

        public UnityEvent onLoadScene;
        private AsyncOperation _loadOperation;

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
            _loadOperation = SceneManager.LoadSceneAsync(sceneName);
            onLoadScene?.Invoke();
        }

        private void FixedUpdate()
        {
            if (_loadOperation == null) return;
            progressBar.value = Mathf.Clamp01(_loadOperation.progress / 0.9f);
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