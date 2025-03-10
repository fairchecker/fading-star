using Unity.Cinemachine;
using UnityEngine;
using Random = System.Random;

namespace View
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject actor;
        [SerializeField] private CinemachineCamera cineCamera;
        private float _timeElapsed;

        private void Awake()
        {
            var actorObject = Instantiate(actor);
            actorObject.transform.position = transform.position;
            actorObject.GetComponent<ActorView>().Initialize(10f);
            cineCamera.Follow = actorObject.transform;
            cineCamera.LookAt = actorObject.transform;
        }
    }
}