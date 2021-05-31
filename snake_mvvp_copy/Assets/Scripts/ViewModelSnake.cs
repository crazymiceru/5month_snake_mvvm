using System;
using UnityEngine;

namespace Snake
{
    internal sealed class ViewModelSnake : IViewModelSnake,IExecute,IInitialization
    {
        ModelSnake _modelSnake;
        public event Action<Vector2, float,float> evtMove = delegate { };
        public event Action evtUpdatePositionForMove = delegate { };
        public event Action<int> evtCreateBody = delegate { };
        private float stage = 0;

        public ViewModelSnake(ModelSnake modelSnake)
        {
            _modelSnake = modelSnake;
            _modelSnake.scores = 0;
        }

        public void SetControl(Vector2 control)
        {
            if (control != Vector2.zero && _modelSnake.control!=-control)
            {
                _modelSnake.control = control;
            }
        }

        public void AddScores(int scores)
        {
            _modelSnake.scores += scores;
            evtCreateBody.Invoke(scores);
            _modelSnake.speed += (float)scores * _modelSnake.addSpeed;

        }

        public void Execute(float deltaTime)
        {
            stage += _modelSnake.speed * deltaTime;
            if (stage > 1) 
            {
                stage -= 1;
                evtUpdatePositionForMove();
            }

            evtMove(_modelSnake.control.normalized * _modelSnake.speed/2,stage, _modelSnake.speed);
        }

        public void Initialization()
        {
            evtCreateBody(_modelSnake.countBody);
        }
    }
}