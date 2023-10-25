using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesPlugin
{
    public static class AddressablesLoader
    {
        public static List<T> InitAssets<T>(string label)
            where T : ScriptableObject
        {
            var loadedAssets = Addressables.LoadAssetsAsync<T>(label,
                obj => { DebugLogger.SendMessage(obj.name + " is loaded", Color.green); });

            loadedAssets.WaitForCompletion();
            return loadedAssets.Result.ToList();
        }

        public static T InitAsset<T>(string label)
            where T : Object
        {
            var loadedAssets = Addressables.LoadAssetAsync<T>(label);

            loadedAssets.WaitForCompletion();
            return loadedAssets.Result;
        }
    }
}