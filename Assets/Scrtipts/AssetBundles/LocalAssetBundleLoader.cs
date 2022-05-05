using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalAssetBundleLoader : IAssetBundleLoader
{
    public IEnumerator LoadBundle(string assetName = "graphicsbundle", Action<AssetBundle> callback = null)
    {
        string path = Path.Combine(Application.streamingAssetsPath, assetName);
        if (!File.Exists(path))
        {
            Debug.Log($"AssetBundle from path: {Path.Combine(Application.streamingAssetsPath, assetName)} does not exist!");
            yield break;
        }

        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(path);
        yield return bundleLoadRequest;

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log($"Failed to load AssetBundle from path: {Path.Combine(Application.streamingAssetsPath, assetName)}");
            yield break;
        }

        Debug.Log("Success load from file!");

        callback?.Invoke(myLoadedAssetBundle);
    }
}
