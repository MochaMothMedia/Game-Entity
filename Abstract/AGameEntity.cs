using FedoraDev.GameEntity.Implementations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FedoraDev.GameEntity.Abstract
{
    public abstract class AGameEntity : SerializedMonoBehaviour, IGameEntity
    {
        [SerializeField, HideLabel, HorizontalGroup("Unique Entity ID/UEID"), BoxGroup("Unique Entity ID")] ScriptableUEID _uniqueID;

        GameEntityPoolBehaviour _gameEntityPool;

        public uint UniqueID => _uniqueID.ID;

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
            _gameEntityPool = FindObjectOfType<GameEntityPoolBehaviour>();

			try
			{
                _gameEntityPool.Register(this);
			}
            catch (System.NullReferenceException)
			{
                Debug.LogError("An instance of GameEntityPoolBehvaiour was not found." +
                    "If you are using another implementation of IGameEntityPool, you must also reimplement IGameEntity instead of inheriting AGameEntity.");
                return;
            }

            OnActivate();
        }

        public void OnDisable()
        {
            if (_gameEntityPool != null)
                _gameEntityPool.Unregister(this);
            OnDeactivate();
        }

#if UNITY_EDITOR
        [Button, HorizontalGroup("Unique Entity ID/UEID")]
        private void CreateUEID()
		{
            ScriptableUEID ueid = ScriptableObject.CreateInstance<ScriptableUEID>();
            ueid.name = name;
            UnityEditor.AssetDatabase.CreateAsset(ueid, $"Assets/{ueid.name} UEID.asset");
            _uniqueID = ueid;
		}
#endif
    }
}
