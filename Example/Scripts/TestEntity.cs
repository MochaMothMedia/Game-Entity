using MochaMoth.GameEntity.Abstract;
using UnityEngine;

namespace MochaMoth.GameEntity.Example
{
	public class TestEntity : AGameEntity
	{
		public override void OnPause()
		{
			Debug.Log("Paused!");
		}

		public override void Tick()
		{
			Debug.Log("Tick!");
		}
	}
}
