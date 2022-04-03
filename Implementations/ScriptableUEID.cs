using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace FedoraDev.GameEntity.Implementations
{
	[CreateAssetMenu(fileName = "New UEID", menuName = "Unique Entity ID")]
	public class ScriptableUEID : SerializedScriptableObject, IUniqueEntityID
	{
		public string Name => base.name;

		public uint ID
		{
			get => _entityID == null ? 0 : _entityID.ID;
			set
			{
#if UNITY_EDITOR
				_entityID.ID = value;
#endif
			}
		}

		[SerializeField, InlineProperty, HideLabel] IUniqueEntityID _entityID;

#if UNITY_EDITOR
		private void Awake()
		{
			if (ID == 0)
				GenerateID();
		}

		[Button]
		private void GenerateID()
		{
			if (_entityID == null)
				_entityID = Factory.GenerateUEID();
			ScriptableUEID[] instances = GetAllInstances();
			GenerateID(instances);
		}

		private void GenerateID(IUniqueEntityID[] potentialDuplicates)
		{
			System.Random random = new System.Random();
			uint nextID;

			do
			{
				byte[] bytes = new byte[4];
				random.NextBytes(bytes);
				nextID = System.BitConverter.ToUInt32(bytes, 0);
			} while (ContainsDuplicates(nextID, potentialDuplicates));

			ID = nextID;
			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
		}

		private bool ContainsDuplicates(uint id, IUniqueEntityID[] potentialDuplicates)
		{

			foreach (IUniqueEntityID eID in potentialDuplicates)
				if (eID.ID == id)
					return true;

			return false;
		}

		private ScriptableUEID[] GetAllInstances()
		{
			string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(ScriptableUEID).Name}");
			List<ScriptableUEID> instances = new List<ScriptableUEID>();
			for (int i = 0; i < guids.Length; i++)
			{
				string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
				ScriptableUEID asset = UnityEditor.AssetDatabase.LoadAssetAtPath<ScriptableUEID>(path);
				if (asset._entityID != null)
					instances.Add(asset);
			}

			return instances.ToArray();
		}
#endif

		public IUniqueEntityID Generate() => _entityID.Generate();

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
