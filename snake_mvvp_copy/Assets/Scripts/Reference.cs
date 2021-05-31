using UnityEngine;

namespace Snake
{
    internal sealed class Reference
    {
        private ListControllers _listControllers;
        internal Reference(ListControllers listControllers)
        {
            _listControllers = listControllers;
        }

        private Transform _maze;
        internal Transform maze => _maze != null ? _maze : _maze = GameObject.FindObjectOfType<TagMaze>().transform;

        private Transform _canvas;
        internal Transform canvas => _canvas != null ? _canvas : _canvas = GameObject.FindObjectOfType<TagCanvas>().transform;


        private ViewAddFood _viewAddFood;
        internal ViewAddFood viewAddFood=>_viewAddFood!=null ? _viewAddFood : _viewAddFood = GameObject.FindObjectOfType<ViewAddFood>();
    }
}