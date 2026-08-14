namespace MochaMoth.GameEntity
{
	public interface IGameEntity
	{
		string UniqueID { get; }
		string Name { get; }

		void OnActivate();
		void OnDeactivate();
		void OnPause();
		void OnUnpause();
		void Tick();
		void PhysicsTick();
		void PausedTick();
		void PausedPhysicsTick();
	}
}
