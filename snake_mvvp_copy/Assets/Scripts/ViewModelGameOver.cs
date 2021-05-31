using System;
using UnityEngine;

namespace Snake
{
    internal sealed class ViewModelGameOver
    {
        internal event Action evtRestartGame;
        ModelGameOver _modelGameOver;

        internal ViewModelGameOver(ModelGameOver modelGameOver)
        {
            _modelGameOver = modelGameOver;
        }

        public void SetGameOver()
        {
            _modelGameOver.isGameOver = true;
            _modelGameOver.timeGameOver = Time.time;
        }

        public void PressAnyKey()
        {            
            if (_modelGameOver.isGameOver && Time.time - _modelGameOver.timeGameOver > 0.1) evtRestartGame.Invoke();
        }
    }
}