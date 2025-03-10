using UnityEngine;
using View;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        private CameraScaleView _scale;

        private void Awake()
        {
            _scale = GetComponent<CameraScaleView>();
        }

        private void Update()
        {
            ReadScroll();
        }

        private void ReadScroll()
        {
            var scroll = ActorController.Actions.Player.Magnitude.ReadValue<float>();
            _scale.Rescale(scroll);
        }
    }
}