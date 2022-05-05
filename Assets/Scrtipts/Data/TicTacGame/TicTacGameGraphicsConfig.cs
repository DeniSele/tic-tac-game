using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "GameGraphicsConfig", menuName = "Data/GameGraphics", order = 1)]
public class TicTacGameGraphicsConfig : ScriptableObject
{
    #region Nested types

    [Serializable]
    public class SignSpriteData
    {
        public SignSpriteData(GameSignType signType, Sprite sprite)
        {
            this.signType = signType;
            this.sprite = sprite;
        }

        public GameSignType signType;
        public Sprite sprite;
    }

    #endregion



    #region Fields

    [SerializeField] private List<SignSpriteData> signSprites;
    [SerializeField] private Sprite background;

    [Header("Bundle settings")]
    [SerializeField] private string bundleName = "graphicsbundle";


    private List<SignSpriteData> loadedSprites;
    private Sprite loadedBackground = null;

    #endregion



    #region Properties

    public string BundleName => bundleName;

    #endregion



    #region Public methods

    public Sprite GetSignSprite(GameSignType signType)
    {
        if (loadedSprites == null || loadedSprites.Count == 0)
            return GetSignSpriteDefault(signType);

        return loadedSprites.Find(data => data.signType == signType).sprite;
    }


    public Sprite GetSignSpriteDefault(GameSignType signType)
    {
        return signSprites.Find(data => data.signType == signType).sprite;
    }


    public Sprite GetBackgroundSpriteDefault() => background;


    public Sprite GetBackgroundSprite() => loadedBackground == null ? background : loadedBackground;


    public IEnumerator LoadDataFromBundle(AssetBundle assetBundle)
    {
        loadedSprites = new List<SignSpriteData>();

        var assetLoadRequestBack = assetBundle.LoadAssetAsync<Sprite>("background");
        yield return assetLoadRequestBack;

        Sprite spriteBack = assetLoadRequestBack.asset as Sprite;
        loadedBackground = spriteBack;


        var assetLoadRequestCross = assetBundle.LoadAssetAsync<Sprite>("cross");
        yield return assetLoadRequestCross;

        Sprite spriteCross = assetLoadRequestCross.asset as Sprite;
        loadedSprites.Add(new SignSpriteData(GameSignType.Cross, spriteCross));


        var assetLoadRequestZero = assetBundle.LoadAssetAsync<Sprite>("zero");
        yield return assetLoadRequestZero;

        Sprite spriteZero = assetLoadRequestZero.asset as Sprite;
        loadedSprites.Add(new SignSpriteData(GameSignType.Zero, spriteZero));


        assetBundle.Unload(false);

        Debug.Log("Bundle installed successfully!");
    }

    #endregion
}
