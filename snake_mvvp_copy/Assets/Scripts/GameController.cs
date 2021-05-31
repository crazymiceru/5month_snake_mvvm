using UnityEngine;

namespace Snake
{
    internal sealed class GameController : MonoBehaviour
    {
        private ListControllers _listControllers = new ListControllers();
        private Reference _reference;
        private ViewGameOver _viewGameOver;

        private void Awake()
        {
            Time.timeScale = 1;
            _reference = new Reference(_listControllers);

            var modelSnake = new ModelSnake(LoadDataObjects.GetValue<SnakeData>("Data/SnakeData"));

            var prefabGameover = LoadDataObjects.GetValue<GameObject>("ViewGameOver");
            if (Instantiate(prefabGameover).TryGetComponent<ViewGameOver>(out ViewGameOver viewGameOver))
            {
                viewGameOver.Initialisation(new ViewModelGameOver(new ModelGameOver()), _reference);
                _viewGameOver = viewGameOver;
                _listControllers.Add(viewGameOver);
            }


            var viewScores = FindObjectOfType<ViewScores>();
            if (viewScores != null)
            {
                viewScores.Initialising(new ViewModelScores(modelSnake));
            }
            else Debug.LogWarning($"Dont find ViewScores object");

            var viewModelSnake = new ViewModelSnake(modelSnake);
            var headPrefab = LoadDataObjects.GetValue<GameObject>("SnakeHead");
            if (Instantiate(headPrefab, _reference.maze).TryGetComponent<ViewSnake>(out ViewSnake viewSnake))
            {
                viewSnake.Initialisation(viewModelSnake, _reference,_viewGameOver);
                _listControllers.Add(viewSnake);
            }

            _reference.viewAddFood.Initialization(_reference, _listControllers);
            _reference.viewAddFood.AddFood(10);
        }

        private void Start()
        {
            _listControllers.Initialization();
        }

        private void Update()
        {
            _listControllers.Execute(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _listControllers.LateExecute();
        }
    }
}