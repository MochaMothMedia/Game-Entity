using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace FedoraDev.GameEntity.Implementations
{
	public class GameEntityPoolBehaviour : SerializedMonoBehaviour, IGameEntityPool
	{
		public IEnumerable<IGameEntity> GameEntities => _gameEntities.ToArray();

		[SerializeField] List<IGameEntity> _gameEntities = new List<IGameEntity>();
		bool _paused = false;

		public virtual void Update()
		{
			if (_paused)
				PausedTick();
			else
				Tick();
		}

		public virtual void FixedUpdate()
		{
			if (_paused)
				PausedPhysicsTick();
			else
				PhysicsTick();
		}

		[Button, ShowIf("@!_paused")]
		public virtual void Pause()
		{
			_paused = true;
			_gameEntities.ForEach(gameEntity => gameEntity.OnPause());
		}

		[Button, ShowIf("_paused")]
		public virtual void Unpause()
		{
			_paused = false;
			_gameEntities.ForEach(gameEntity => gameEntity.OnUnpause());
		}

		public virtual void Tick() => _gameEntities.ForEach(gameEntity => gameEntity.Tick());
		public virtual void PausedTick() => _gameEntities.ForEach(gameEntity => gameEntity.PausedTick());

		public virtual void PhysicsTick() => _gameEntities.ForEach(gameEntity => gameEntity.PhysicsTick());
		public virtual void PausedPhysicsTick() => _gameEntities.ForEach(gameEntity => gameEntity.PausedPhysicsTick());

		public virtual void Register(IGameEntity entity)
		{
			if (!_gameEntities.Contains(entity))
				_gameEntities.Add(entity);
		}

		public virtual void Unregister(IGameEntity entity)
		{
			if (_gameEntities.Contains(entity))
				_gameEntities.Remove(entity);
		}
	}
}
