using Interfaces;
using UnityEngine;

namespace View
{
    public class ActorView : MonoBehaviour, IMovable
    {
        private Rigidbody _rigidbody;
        private float _speed;

        public float Speed()
        {
            return _speed;
        }

        public void Move(Vector2 direction)
        {
            _rigidbody.linearVelocity = new Vector3(direction.x * _speed, 0f, direction.y * _speed);
        }

        public void Initialize(float speed)
        {
            _speed = speed;
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}