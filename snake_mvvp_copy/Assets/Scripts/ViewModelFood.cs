using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Snake
{
    internal sealed class ViewModelFood : IFood, IExecute
    {
        private ModelFood _modelFood;
        public event Action<(Vector3, float)> evtMove = delegate { };

        internal ViewModelFood(ModelFood modelFood)
        {
            _modelFood = modelFood;
        }
        public int GetScores()
        {
            return _modelFood.addScores;
        }

        public void Execute(float deltaTime)
        {
            evtMove.Invoke((new Vector3(Random.Range(-_modelFood.rndMove, _modelFood.rndMove) * deltaTime, 0, Random.Range(-_modelFood.rndMove, _modelFood.rndMove) * deltaTime), _modelFood.maxSpeed));
        }
    }

}