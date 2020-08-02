using EndlessRunner.Player;
using UnityEngine;

namespace EndlessRunner.Controllers.Input
{
    public class StandaloneInputController : InputController
    {
        public StandaloneInputController(PlayerFacade facade) : base(facade)
        {
        }
        
        protected override void HandleInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
            {
            	_playerFacade.PassInput(InputAction.MoveLeft);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
            {
            	_playerFacade.PassInput(InputAction.MoveRight);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
            {
            	_playerFacade.PassInput(InputAction.Jump);
            }
        }
    }
}