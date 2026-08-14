using Sirenix.OdinInspector;
using System;
using UnityEditor;
using UnityEngine;

namespace MochaMoth.GameEntity.Implementations
{
	[CreateAssetMenu(fileName = "New UEID", menuName = "Unique Entity ID")]
	public class ScriptableUEID : SerializedScriptableObject, IUniqueEntityID
	{
		public string Name => base.name;

		public string ID
		{
			get => _entityID == null ? string.Empty : _entityID.ID;
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
			if (string.IsNullOrEmpty(ID))
				GenerateID();
		}

		[Button]
		private void GenerateID()
		{
			_entityID ??= Factory.GenerateUEID();
			ID = Guid.NewGuid().ToString();
			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssetIfDirty(this);
		}
#endif

		public IUniqueEntityID Generate() => _entityID.Generate();
	}
}
