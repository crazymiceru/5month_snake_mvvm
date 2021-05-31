using System;
using UnityEngine;

namespace Snake
{
    internal interface IFood
    {
        public event Action<(Vector3, float)> evtMove;
        public int GetScores();
    }

}