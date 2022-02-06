using Sirenix.OdinInspector;
using UnityEngine;

namespace FedoraDev.GameEntity.Implementations
{
	[CreateAssetMenu(fileName = "New UEID", menuName = "Unique Entity ID")]
	public class ScriptableUEID : SerializedScriptableObject, IUniqueEntityID
	{
		[SerializeField] uint _id;

		public string Name => base.name;

		public uint ID
		{
			get => _id;
			set
			{
#if UNITY_EDITOR
				_id = value;
#endif
			}
		}

#if UNITY_EDITOR
		private void Awake()
		{
			GenerateID();
		}

		[Button]
		private void GenerateID()
		{
			System.Random random = new System.Random();
			uint nextID;

			do
			{
				byte[] bytes = new byte[4];
				random.NextBytes(bytes);
				nextID = System.BitConverter.ToUInt32(bytes, 0);
			} while (ContainsDuplicates(nextID));

			ID = nextID;
		}

		private bool ContainsDuplicates(uint id)
		{

			ScriptableUEID[] instances = GetAllInstances();
			foreach (ScriptableUEID eID in instances)
				if (eID.ID == id)
					return true;

			return false;
		}

		private ScriptableUEID[] GetAllInstances()
		{
			string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(ScriptableUEID).Name}");
			ScriptableUEID[] instances = new ScriptableUEID[guids.Length];
			for (int i = 0; i < guids.Length; i++)
			{
				string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
				instances[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<ScriptableUEID>(path);
			}

			return instances;
		}
#endif

		public static bool operator ==(ScriptableUEID lhs, ScriptableUEID rhs) => lhs.ID == rhs.ID;
		public static bool operator !=(ScriptableUEID lhs, ScriptableUEID rhs) => lhs.ID != rhs.ID;

		public override bool Equals(object obj) => obj is ScriptableUEID eID && base.Equals(obj) && ID == eID.ID;

		public override int GetHashCode()
		{
			int hashCode = 2082127350;
			hashCode = (hashCode * -1521134295) + base.GetHashCode();
			hashCode = (hashCode * -1521134295) + ID.GetHashCode();
			return hashCode;
		}
	}
}
