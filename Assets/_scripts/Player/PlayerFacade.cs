using UnityEngine;
using Zenject;

namespace EndlessRunner.Player
{
    public class PlayerFacade : MonoBehaviour
    {
        private PlayerMoveController _moveController;
        
        [Inject]
        private void Construct(PlayerMoveController moveController)
        {
            _moveController = moveController;
        }

        public void PassInput(InputAction actionType)
        {
            _moveController.HandleInput(actionType);
        }

        public float PlayerPosition => this.transform.position.z;
    }
}