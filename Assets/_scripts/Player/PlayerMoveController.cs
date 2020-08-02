using System;
using DG.Tweening;
using EndlessRunner.Controllers;
using EndlessRunner.Signals;
using UnityEngine;
using Zenject;

namespace EndlessRunner.Player
{
	public class PlayerMoveController : IInitializable ,ITickable
	{
		private PlayerFacade _playerFacade;
		private Settings _settings;
		private StageManager _stageManager;
		private SignalBus _signalBus;
		
		private int _currentLane;
		private bool _isMoving;
		private bool _isJumping;

		private float _jumpStartPosition;

		[Inject]
		public PlayerMoveController(PlayerFacade facade, Settings settings, StageManager stageManager, SignalBus signalBus)
		{
			_playerFacade = facade;
			_settings = settings;
			_stageManager = stageManager;
			_signalBus = signalBus;
		}
		
		public void Initialize()
		{
			SubscribeToSignals();
			_playerFacade.transform.DOMoveX(_stageManager.CurrentStageFacade.GetPlayerSpawnPosition(), 0);
			_currentLane = _stageManager.CurrentStageFacade.GetPlayerSpawnLine();
		}

		public void Tick()
		{
			var currentPosition = _playerFacade.PlayerPosition;
			
			if (_isMoving)
			{
				var forwardMoveChange = Vector3.forward * Time.deltaTime * _settings.Speed;
				_playerFacade.transform.Translate(forwardMoveChange);
				_signalBus.Fire(new ScoreIncreaseSignal(forwardMoveChange.z));
			}

			if (_isJumping)
			{
				if (currentPosition - _jumpStartPosition > _settings.JumpLength)
					StopJump();
			}
		}

		private void SubscribeToSignals()
		{
			_signalBus.Subscribe<GameStartSignal>(StartMoving);
			_signalBus.Subscribe<GameOverSignal>(StopMoving);
		}

		public void HandleInput(InputAction actionType)
		{
			switch (actionType)
			{
				case InputAction.MoveLeft:
					MoveLeft();
					break;
				case InputAction.MoveRight:
					MoveRight();
					break;
				case InputAction.Jump:
					Jump();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
			}
		}

		private void StartMoving()
		{
			_isMoving = true;
		}

		private void StopMoving()
		{
			_isMoving = false;
			StopJump();
		}

		private void MoveRight()
		{
			if (!_isMoving)
				return;

			if (_currentLane == _stageManager.CurrentStageFacade.LaneCount - 1)
				return;

			_currentLane++;
			MoveToCurrentLane();
		}

		private void MoveLeft()
		{
			if (!_isMoving)
				return;

			if (_currentLane == 0)
				return;

			_currentLane--;
			MoveToCurrentLane();
		}

		private void MoveToCurrentLane()
		{
			var offset = _stageManager.CurrentStageFacade.GetLaneOffset(_currentLane);
			_playerFacade.transform.DOMoveX(offset, _settings.MoveTime);
		}

		private void Jump()
		{
			if (!_isMoving)
				return;

			_isJumping = true;
			_jumpStartPosition = _playerFacade.transform.position.z;
			_playerFacade.transform.DOMoveY(_settings.JumpHeight, _settings.JumpSpeed);
		}

		private void StopJump()
		{
			if (!_isJumping)
				return;

			_isJumping = false;
			_playerFacade.transform.DOMoveY(0, _settings.JumpSpeed);
		}


		[Serializable]
		public class Settings
		{
			public float JumpLength = 2.0f; // Distance jumped
			public float JumpHeight = 1.2f;
			public float JumpSpeed = 0.5f;
			public float MoveTime;
			public float Speed;
		}
	}
}