using System.Collections.Generic;

namespace MochaMoth.GameEntity
{
	public interface IGameEntityPool
	{
		IEnumerable<IGameEntity> GameEntities { get; }

		IGameEntity GetEntityByID(string id);
		void Register(IGameEntity entity);
		void Unregister(IGameEntity entity);
		void Pause();
		void Unpause();
		void Tick();
		void PhysicsTick();
		void PausedTick();
		void PausedPhysicsTick();
	}
}
