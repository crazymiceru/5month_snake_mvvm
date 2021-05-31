using UnityEngine;

namespace Snake
{
    internal sealed class BodyData
    {
        internal BodyData(Transform body, Vector3 fromMove, Vector3 toMove)
        {
            this.body = body;
            this.fromMove = fromMove;
            this.toMove = toMove;
        }
        internal Transform body;
        internal Vector3 fromMove;
        internal Vector3 toMove;
    }
}