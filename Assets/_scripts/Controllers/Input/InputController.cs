using EndlessRunner.Player;
using Zenject;

namespace EndlessRunner.Controllers.Input
{
    public abstract class InputController : ITickable
    {

        protected PlayerFacade _playerFacade;
        
        public InputController(PlayerFacade facade)
        {
            _playerFacade = facade;
        }
        
        public void Tick()
        {
            HandleInput();
        }

        protected abstract void HandleInput();
    }
}

public enum InputAction
{
    MoveLeft = 1,
    MoveRight = 2,
    Jump = 3,
}