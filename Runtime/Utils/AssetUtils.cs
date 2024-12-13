using System.IO;
using UnityEditor;
using UnityEngine;

namespace StateLogic.Utils {
    /// <summary>
    /// Utilities for handling asset creation.
    /// </summary>
    public static class AssetUtils {
        /// <summary>
        /// Create an asset in a given path.
        /// </summary>
        /// <param name="asset">Asset to be created.</param>
        /// <param name="path">Asset path starting with <b>Assets</b>, also including asset name and extension. </param>
        public static void CreateAsset(Object asset, string path) {
            CreateDirectory(path);
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Load an asset.
        /// </summary>
        /// <typeparam name="T">Asset type.</typeparam>
        /// <param name="path">Asset path.</param>
        /// <returns>Loaded asset.</returns>
        public static T LoadAsset<T>(string path) where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        /// <summary>
        /// Create a directory at a given path.
        /// </summary>
        /// <param name="path">Directory path. Can include file name, which will be removed.</param>
        public static void CreateDirectory(string path) {
            var directoryPath = Path.GetDirectoryName(path);
            if (Directory.Exists(directoryPath)) return;
            Directory.CreateDirectory(directoryPath);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Focus the editor in a given asset in the proejct view.
        /// </summary>
        /// <param name="asset">Asset to focus.</param>
        public static void FocusOnProjectAsset(Object asset) {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}