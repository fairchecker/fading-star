using Unity.Cinemachine;
using UnityEngine;

namespace View
{
    public class CameraScaleView : MonoBehaviour
    {
        private CinemachineFollow _cameraFollow;

        private void Awake()
        {
            _cameraFollow = GetComponent<CinemachineFollow>();
        }

        public void Rescale(float newScale)
        {
            if ((_cameraFollow.FollowOffset.y < 30 && newScale < 0) ||
                (_cameraFollow.FollowOffset.y > 5 && newScale > 0))
                _cameraFollow.FollowOffset =
                    new Vector3(0, _cameraFollow.FollowOffset.y - newScale * 150f * Time.deltaTime, -25);
        }
    }
}