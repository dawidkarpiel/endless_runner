using System;
using UnityEngine;
using Zenject;

namespace EndlessRunner.Stage.Utils
{
    [RequireComponent(typeof(Collider))]
    public class ColliderHandler : MonoBehaviour
    {
        [Inject]
        public void Construct(IColliderTriggerEnter enter)
        {
            _triggerEnter = enter;
        }
        
        private IColliderTriggerEnter _triggerEnter;
        private void OnTriggerEnter(Collider other)
        {
            _triggerEnter.TriggerEnter(other);
        }
    }

    public interface IColliderTriggerEnter
    {
        void TriggerEnter(Collider other);
    }
}