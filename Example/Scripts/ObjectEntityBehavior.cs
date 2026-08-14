using MochaMoth.GameEntity.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MochaMoth.GameEntity.Example
{
    public class ObjectEntityBehavior : AGameEntity
	{
		[SerializeField, InlineProperty, HideLabel] IUniqueEntityID _entityIdFab;
		private float _lifetime = 6f;
		private float _velocity = 0f;
		private float _terminalVelocity = 5f;

		public override void OnActivate()
		{
			_uniqueID = _entityIdFab.Generate();
		}

		public override void PhysicsTick()
		{
			_velocity += Mathf.Min(0.5f * Time.fixedDeltaTime, _terminalVelocity);
			transform.Translate(new Vector3(0, -_velocity, 0));
			_lifetime -= Time.fixedDeltaTime;
            if (_lifetime <= 0f)
            {
				Destroy(gameObject);
			}
		}
    }
}
