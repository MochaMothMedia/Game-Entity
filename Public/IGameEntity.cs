namespace FedoraDev.GameEntity
{
    public interface IGameEntity
    {
        uint UniqueID { get; }

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
