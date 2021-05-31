using System;
using UnityEngine;
using TMPro;

namespace Snake
{
    
    internal class ViewModelScores
    {
        public event Action<int> evtScores = delegate { };
        private ModelSnake _modelSnake;
        internal ViewModelScores(ModelSnake modelSnake)
        {
            _modelSnake = modelSnake;
            _modelSnake.evtScores += SetScores;
        }

        private void SetScores(int scores)
        {
            evtScores.Invoke(scores);
        }

    }

    internal class ViewScores : MonoBehaviour
    {
        private ViewModelScores _viewModelScores;
        private TextMeshProUGUI _text;

        internal void Initialising(ViewModelScores viewModelScores)
        {
            _viewModelScores = viewModelScores;
            _viewModelScores.evtScores += SetScores;
            _text = GetComponent<TextMeshProUGUI>();
        }
        private void SetScores(int scores)
        {
            _text.text = scores.ToString();
        }
    }
}
