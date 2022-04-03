using Sirenix.OdinInspector;
using UnityEngine;

namespace FedoraDev.GameEntity.Implementations
{
    public class Factory : SerializedScriptableObject
    {
        const string FACTORY_PATH = "Assets/Fedora Dev/";
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
				    {
                        if (!System.IO.Directory.Exists(FACTORY_PATH))
                            System.IO.Directory.CreateDirectory(FACTORY_PATH);
                        Factory asset = ScriptableObject.CreateInstance<Factory>();
                        UnityEditor.AssetDatabase.CreateAsset(asset, FactoryAsset);
                        _instance = asset;
				    }
                }

                return _instance;
			}
		}

        [SerializeField] IUniqueEntityID _ueidFab = new SimpleUEID();

        static Factory _instance;

        public static IUniqueEntityID GenerateUEID() => Instance._ueidFab.Generate();
    }
}
