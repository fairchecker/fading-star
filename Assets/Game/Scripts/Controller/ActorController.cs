using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    public class ActorController : MonoBehaviour
    {
        public static InputSystem_Actions Actions;
        private IMovable _actor;
        [SerializeField] private GunScript gunScript;
        private void Awake()
        {
            Actions = new InputSystem_Actions();
            Actions.Enable();
            Actions.Player.Attack.performed += OnShootPerformed;
            _actor = GetComponent<IMovable>();
        }

        private void FixedUpdate()
        {
            ReadMovement();
            GetMousePosition();
        }

        private void OnShootPerformed(InputAction.CallbackContext ctx)
        {
            gunScript.Shoot();
        }
        
        private void GetMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    Vector3 targetPoint = hit.point;
                    Vector3 direction = targetPoint - transform.position;
                    direction.y = 0;

                    if (direction.sqrMagnitude > 0.01f)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 270, 0);
                        transform.rotation = targetRotation;
                    }
                    break;
                }
            }
        }

        private void ReadMovement()
        {
            var direction = Actions.Player.Move.ReadValue<Vector2>();
            _actor.Move(direction);
        }

    }
}