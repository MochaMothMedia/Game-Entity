using UnityEngine;

namespace FedoraDev.GameEntity.Implementations
{
	public class SimpleUEID : IUniqueEntityID
	{
		public string Name => "Simple UEID";
		public uint ID { get => _id; set => _id = value; }

		[SerializeField] uint _id;

		public IUniqueEntityID Generate() => new SimpleUEID();
	}
}
