using EndlessRunner.Player;
using UnityEngine;

namespace EndlessRunner.Controllers.Input
{
    public class MobileInputController : InputController
    {
        private Vector2 _startingTouch;
        private bool _isSwiping;

        public MobileInputController(PlayerFacade facade) : base(facade)
        {
        }
        
        protected override void HandleInput()
        {
            if (UnityEngine.Input.touchCount == 1)
            {
                if (_isSwiping)
                {
                    Vector2 diff = UnityEngine.Input.GetTouch(0).position - _startingTouch;

                    diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

                    if (diff.magnitude > 0.01f) //we set the swip distance to trigger movement to 1% of the screen width
                    {
                        if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                        {
                            if (diff.y < 0)
                            {
                                //slide
                            }
                            else
                            {
                                _playerFacade.PassInput(InputAction.Jump);
                            }
                        }
                        else
                        {
                            if (diff.x < 0)
                            {
                                _playerFacade.PassInput(InputAction.MoveLeft);
                            }
                            else
                            {
                                _playerFacade.PassInput(InputAction.MoveRight);
                            }
                        }

                        _isSwiping = false;
                    }
                }

                if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _startingTouch = UnityEngine.Input.GetTouch(0).position;
                    _isSwiping = true;
                }
                else if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    _isSwiping = false;
                }
            }
        }
    }
}