namespace MochaMoth.GameEntity
{
	public interface IUniqueEntityID
	{
		string Name { get; }
		string ID { get; set;  }

		IUniqueEntityID Generate();
	}
}
