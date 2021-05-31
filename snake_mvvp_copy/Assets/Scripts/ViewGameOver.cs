using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snake
{
    internal class ViewGameOver : MonoBehaviour,IController,IExecute
    {
        private Reference _reference;
        private ViewModelGameOver _viewModelGameOver;

        public void Execute(float deltaTime)
        {
            if (Input.anyKeyDown)
            {
                _viewModelGameOver.PressAnyKey();
            }
        }

        internal void GameOver()
        {
            var prefabGameOver = LoadDataObjects.GetValue<GameObject>("GameOver");
            Instantiate(prefabGameOver, _reference.canvas);
            _viewModelGameOver.SetGameOver();
            Time.timeScale = 0.1f;
        }
        internal void Initialisation(ViewModelGameOver viewModelGameOver, Reference reference)
        {
            _reference = reference;
            _viewModelGameOver = viewModelGameOver;
            _viewModelGameOver.evtRestartGame += RestartGame;
        }

        private void RestartGame()
        {            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}