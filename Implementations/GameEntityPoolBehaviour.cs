using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FedoraDev.GameEntity.Implementations
{
	public class GameEntityPoolBehaviour : SerializedMonoBehaviour, IGameEntityPool
	{
		public IEnumerable<IGameEntity> GameEntities => _gameEntities.Values.ToArray();

		[SerializeField] Dictionary<uint, IGameEntity> _gameEntities = new Dictionary<uint, IGameEntity>();
		bool _paused = false;

		public virtual IGameEntity GetEntityByID(uint id)
		{
			if (_gameEntities.ContainsKey(id))
				return _gameEntities[id];

			return null;
		}

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
			_ = GameEntities.ForEach(gameEntity => gameEntity.OnPause());
		}

		[Button, ShowIf("_paused")]
		public virtual void Unpause()
		{
			_paused = false;
			_ = GameEntities.ForEach(gameEntity => gameEntity.OnUnpause());
		}

		public virtual void Tick() => GameEntities.ForEach(gameEntity => gameEntity.Tick());
		public virtual void PausedTick() => GameEntities.ForEach(gameEntity => gameEntity.PausedTick());

		public virtual void PhysicsTick() => GameEntities.ForEach(gameEntity => gameEntity.PhysicsTick());
		public virtual void PausedPhysicsTick() => GameEntities.ForEach(gameEntity => gameEntity.PausedPhysicsTick());

		public virtual void Register(IGameEntity entity)
		{
			if (!_gameEntities.ContainsKey(entity.UniqueID))
				_gameEntities.Add(entity.UniqueID, entity);
		}

		public virtual void Unregister(IGameEntity entity)
		{
			if (_gameEntities.ContainsKey(entity.UniqueID))
				_gameEntities.Remove(entity.UniqueID);
		}
	}
}
