using MochaMoth.GameEntity.Abstract;
using UnityEngine;

namespace MochaMoth.GameEntity.Example
{
	public class SpawnBehavior : AGameEntity
	{
		[SerializeField] GameObject _spawnObject;
		float _timeSinceDrop = 0.0f;

		public override void PhysicsTick()
		{
			_timeSinceDrop -= Time.fixedDeltaTime;

			if (_timeSinceDrop <= 0)
			{
				SpawnObject();
				_timeSinceDrop = 0.25f;
			}
		}

		private void SpawnObject()
		{
			GameObject obj = Instantiate(_spawnObject);
			ObjectEntityBehavior behavior = obj.GetComponent<ObjectEntityBehavior>();
		}
	}
}
