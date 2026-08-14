using Sirenix.OdinInspector;
using UnityEngine;

namespace MochaMoth.GameEntity.Implementations
{
	public class Factory : SerializedScriptableObject
	{
		const string FACTORY_PATH = "Assets/Mocha Moth/";
		const string FACTORY_NAME = "UEID Factory.asset";
		static string FactoryAsset => $"{FACTORY_PATH}{FACTORY_NAME}";

		public static Factory Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = (Factory)UnityEditor.AssetDatabase.LoadAssetAtPath(FactoryAsset, typeof(Factory));
					if (_instance == null)
						_instance = CreateDefaultAsset();
				}

				return _instance;
			}
		}
		
		[BoxGroup("Fab")]
		[InfoBox("Set the implementation that the factory will instantiate for new Scriptable UEIDs.")]
		[SerializeField] IUniqueEntityID _ueidFab = new SimpleUEID();

		static Factory _instance;

		public static IUniqueEntityID GenerateUEID() => Instance._ueidFab.Generate();

		private static Factory CreateDefaultAsset()
		{
			if (!System.IO.Directory.Exists(FACTORY_PATH))
				System.IO.Directory.CreateDirectory(FACTORY_PATH);
			Factory asset = CreateInstance<Factory>();
			UnityEditor.AssetDatabase.CreateAsset(asset, FactoryAsset);
			return asset;
		}
	}
}
