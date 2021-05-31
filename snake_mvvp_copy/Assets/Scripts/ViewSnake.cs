using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    internal sealed class ViewSnake : MonoBehaviour, IExecute, IController
    {
        private IViewModelSnake _viewModelSnake;
        private List<BodyData> _body = new List<BodyData>();
        private Reference _reference;
        ViewGameOver _viewGameOver;

        public void Execute(float deltaTime)
        {
            var control = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _viewModelSnake.SetControl(control);
            if (_viewModelSnake is IExecute) (_viewModelSnake as IExecute).Execute(deltaTime);
        }

        public void Initialisation(IViewModelSnake vmSnake, Reference reference,ViewGameOver viewGameOver)
        {
            _viewModelSnake = vmSnake;
            _reference = reference;            
            _viewModelSnake.evtMove += Move;
            _viewModelSnake.evtCreateBody += CreateBody;
            _viewModelSnake.evtUpdatePositionForMove += UpdatePosistionForMove;
            _body.Add(new BodyData(transform, Vector3.zero, Vector3.zero));
            if (_viewModelSnake is IInitialization) (_viewModelSnake as IInitialization).Initialization();
            _viewGameOver = viewGameOver;
        }

        private void Move(Vector2 vectorMove,float stage,float speedRotation)
        {
            if (vectorMove != Vector2.zero)
            {
                var addVector = new Vector3(vectorMove.x, 0, vectorMove.y);
                transform.position += addVector * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(addVector), Time.deltaTime * speedRotation);
            }

            for (int i = 1; i < _body.Count; i++)
            {
                _body[i].body.position = Vector3.Lerp(_body[i].fromMove, _body[i].toMove, stage);
            }
        }

        private void UpdatePosistionForMove()
        {
            for (int i = 1; i < _body.Count; i++)
            {
                _body[i].fromMove = _body[i].body.position;
                _body[i].toMove = _body[i - 1].body.position;
            }
        }

        private void CreateBody(int addCountBody)
        {
            GameObject bodyPrefab;
            Vector3 startPos;

            if (_body.Count == 1) bodyPrefab = LoadDataObjects.GetValue<GameObject>("SnakeBody");
            else bodyPrefab = _body[1].body.gameObject;

            for (int i = 0; i < addCountBody; i++)
            {
                startPos = _body[_body.Count - 1].body.transform.position;
                var bodyGameObject = Instantiate(bodyPrefab, _reference.maze);
                bodyGameObject.transform.position = startPos;
                _body.Add(new BodyData(bodyGameObject.transform, startPos, _body[_body.Count - 1].body.position));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ViewFood>(out ViewFood viewFood))
            {
                _viewModelSnake.AddScores(viewFood.GetScores());
                viewFood.DestroyFood();
                _reference.viewAddFood.AddFood(1); 
            }
            if (other.gameObject.TryGetComponent<TagWall>(out TagWall tagWall))
            {
                _viewGameOver.GameOver();
            }
        }
    }
}