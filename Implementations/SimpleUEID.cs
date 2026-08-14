using UnityEngine;

namespace MochaMoth.GameEntity.Implementations
{
	public class SimpleUEID : IUniqueEntityID
	{
		public string Name => "Simple UEID";
		public string ID { get => _id; set => _id = value; }

		[SerializeField] string _id;

		public IUniqueEntityID Generate() => new SimpleUEID();
	}
}
