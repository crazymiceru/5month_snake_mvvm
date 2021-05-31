using System;
using UnityEngine;

namespace Snake
{
    internal sealed class ModelSnake
    {
        public event Action<int> evtScores = delegate { };
        public ModelSnake(SnakeData snakeData)
        {
            this.countBody = snakeData.countBody;
            this.speed = snakeData.startSpeed;
            this.addSpeed = snakeData.addSpeed;
        }

        internal int countBody;
        internal float speed;
        internal float addSpeed;
        internal Vector2 control;

        private int _scores;
        internal int scores
        {
            get => _scores;
            set
            {
                _scores = value;
                evtScores.Invoke(_scores);
            }
        }
    }
}