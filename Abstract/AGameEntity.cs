using FedoraDev.GameEntity.Implementations;
using UnityEngine;

namespace FedoraDev.GameEntity.Abstract
{
    public abstract class AGameEntity : MonoBehaviour, IGameEntity
    {
        public virtual void OnActivate() { }
        public virtual void OnDeactivate() { }
        public virtual void OnPause() { }
        public virtual void OnUnpause() { }
        public virtual void Tick() { }
        public virtual void PhysicsTick() { }
        public virtual void PausedTick() { }
        public virtual void PausedPhysicsTick() { }

        public void OnEnable()
        {
            OnActivate();
            FindObjectOfType<GameEntityPoolBehaviour>().Register(this);
        }

        public void OnDisable()
        {
            OnDeactivate();
            FindObjectOfType<GameEntityPoolBehaviour>().Unregister(this);
        }
	}
}
