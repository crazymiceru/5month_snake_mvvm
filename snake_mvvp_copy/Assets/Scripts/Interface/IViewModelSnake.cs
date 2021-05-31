using System;
using UnityEngine;

namespace Snake
{
    internal interface IViewModelSnake
    {
        public void SetControl(Vector2 control);
        public event Action<Vector2,float,float> evtMove;
        public event Action<int> evtCreateBody;
        public event Action evtUpdatePositionForMove;
        public void AddScores(int scores);
    }
}