using UnityEngine;

namespace Snake
{
    [CreateAssetMenu(menuName = "My/SnakeData")]

    public sealed class SnakeData : ScriptableObject
    {
        public int countBody = 3;
        public float startSpeed = 5;
        public float addSpeed = 1;
    }
}
