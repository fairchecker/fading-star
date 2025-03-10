using UnityEngine;

namespace Interfaces
{
    public interface IMovable
    {
        public float Speed();
        public void Move(Vector2 direction);
    }
}