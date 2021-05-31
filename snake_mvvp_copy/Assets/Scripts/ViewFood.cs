using UnityEngine;

namespace Snake
{

    internal class ViewFood : MonoBehaviour, IController, IExecute
    {
        private IFood _viewModelFood;
        private Rigidbody _rigidBody;
        private ListControllers _listControllers;
        private ViewModelFoodDestroyEffects _viewModelFoodDestroyEffects;
        internal void Initialisation(IFood viewModelFood,ViewModelFoodDestroyEffects viewModelFoodDestroyEffects, ListControllers listControllers)
        {
            _rigidBody = GetComponent<Rigidbody>();
            if (_rigidBody==null) Debug.LogWarning($"Dont find Rigidbody in {name}");
            _listControllers = listControllers;
            _viewModelFood = viewModelFood;
            _viewModelFood.evtMove += Move;
            _viewModelFoodDestroyEffects = viewModelFoodDestroyEffects;
            _viewModelFoodDestroyEffects.evtDestroy += DestroyEffects;
        }

        internal void DestroyEffects(GameObject prefab)
        {
            var gameObject = Instantiate(prefab, transform.position, prefab.transform.rotation);
            Destroy(gameObject, 1);
        }

        public void DestroyFood()
        {
            _viewModelFoodDestroyEffects.Destroy();
            Destroy(gameObject);
        }

        internal int GetScores()
        {
            return _viewModelFood.GetScores();
        }

        private void Move((Vector3 vectorMove,float maxSpeed) value)
        {
            _rigidBody.AddForce(value.vectorMove);
            if (_rigidBody.velocity.sqrMagnitude > value.maxSpeed * value.maxSpeed)
            {
                _rigidBody.velocity = _rigidBody.velocity.normalized * value.maxSpeed;
            }
        }

        private void OnDisable()
        {
            _viewModelFood.evtMove -= Move;
            _listControllers.Delete(this);
        }

        public void Execute(float deltaTime)
        {
            if (_viewModelFood is IExecute) (_viewModelFood as IExecute).Execute(deltaTime);
        }
    }
}