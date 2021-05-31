using UnityEngine;

namespace Snake
{
    internal sealed class ModelFood
    {
        internal ModelFood(int scores,float rndMove,float maxSpeed, GameObject destroyEffects)
        {
            this.addScores = scores;
            this.rndMove = rndMove;
            this.maxSpeed = maxSpeed;
            this.destroyEffects = destroyEffects;
        }
        public readonly int addScores;
        public readonly float rndMove;
        public readonly float maxSpeed;
        public readonly GameObject destroyEffects;

    }
}