using System;
using System.Collections;
using UnityEngine;

public interface IAssetBundleLoader
{
    IEnumerator LoadBundle(string assetName = "graphicsbundle", Action<AssetBundle> callback = null);
}
