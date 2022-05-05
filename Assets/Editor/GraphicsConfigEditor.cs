using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TicTacGameGraphicsConfig))]
public class GraphicsConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TicTacGameGraphicsConfig myScript = (TicTacGameGraphicsConfig)target;
        if (GUILayout.Button("Build Asset Bundle"))
        {
            AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

            buildMap[0].assetBundleName = myScript.BundleName;

            string[] graphicsAssets = new string[3];

            graphicsAssets[0] = AssetDatabase.GetAssetPath(myScript.GetBackgroundSpriteDefault());
            graphicsAssets[1] = AssetDatabase.GetAssetPath(myScript.GetSignSpriteDefault(GameSignType.Cross));
            graphicsAssets[2] = AssetDatabase.GetAssetPath(myScript.GetSignSpriteDefault(GameSignType.Zero));

            buildMap[0].assetNames = graphicsAssets;

            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", buildMap, BuildAssetBundleOptions.None, BuildTarget.Android);

            GUIUtility.ExitGUI();
        }
    }
}
