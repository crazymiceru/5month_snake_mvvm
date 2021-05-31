using UnityEngine;

namespace Snake
{
    public class ViewAddFood : MonoBehaviour
    {
        [SerializeField] private Vector2 zoneAddFood;
        private Reference _reference;
        ListControllers _listControllers;

        public void AddFood(int count)
        {
            var prefabFood = LoadDataObjects.GetValue<GameObject>("Food");

            for (int i = 0; i < count; i++)
            {
                var gameObjectFood = Instantiate(prefabFood, _reference.maze);
                gameObjectFood.transform.position = new Vector3(Random.Range(-zoneAddFood.x, zoneAddFood.x), 0.5f, Random.Range(-zoneAddFood.y, zoneAddFood.y));
                if (gameObjectFood.TryGetComponent<ViewFood>(out ViewFood viewFood))
                {
                    var score = Random.Range(1, 4);
                    var size = (float)score / 3 +0.5f;
                    gameObjectFood.transform.localScale = new Vector3(size, size, size);

                    var modelFood = new ModelFood(score, 300, 20, LoadDataObjects.GetValue<GameObject>("Effects"));
                    var viewModelFood = new ViewModelFood(modelFood);
                    var viewModelEffectsFood = new ViewModelFoodDestroyEffects(modelFood);                    
                    viewFood.Initialisation(viewModelFood,viewModelEffectsFood,_listControllers);
                    _listControllers.Add(viewFood);
                }
            }
        }

        internal void Initialization(Reference reference,ListControllers listControllers)
        {
            _reference = reference;
            _listControllers = listControllers;
        }
    }
}