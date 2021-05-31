using System;
using UnityEngine;

namespace Snake
{
    internal sealed class ViewModelFoodDestroyEffects
    {
        internal event Action<GameObject> evtDestroy;
        private ModelFood _modelFood;

        internal ViewModelFoodDestroyEffects(ModelFood modelFood)
        {
            _modelFood = modelFood;
        }

        internal void Destroy()
        {
            evtDestroy.Invoke(_modelFood.destroyEffects);
        }
    }

}