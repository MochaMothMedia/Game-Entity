using System.Collections.Generic;

namespace FedoraDev.GameEntity
{
    public interface IGameEntityPool
    {
        IEnumerable<IGameEntity> GameEntities { get; }

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
