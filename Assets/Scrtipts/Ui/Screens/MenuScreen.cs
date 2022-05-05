using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : ScreenBase
{
    #region Fields

    [Header("Menu screen settings")]
    [SerializeField] private Transform buttonsRoot;
    [SerializeField] private Button playButtonPrefab;
    [SerializeField] private TMP_Text title;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button loadAssetButton;

    #endregion



    #region Public methods

    public override void Initialize(UiService uiService)
    {
        base.Initialize(uiService);

        var gameOptions = GameManager.Instance.DataHub.TicTacGameConfig.Options;
        foreach (var option in gameOptions)
        {
            Button button = Instantiate(playButtonPrefab, buttonsRoot);
            button.GetComponentInChildren<TMP_Text>().text = option.OptionName;
            button.onClick.AddListener(() => 
            {
                GameManager.Instance.ServicesHub.TicTacGameService.SetGameOption(option);
                GameManager.Instance.ServicesHub.UiService.ShowScreen(ScreenType.GameScreen);
            });
        }

        loadAssetButton.onClick.AddListener(() => 
        {
            IAssetBundleLoader assetBundleLoader = new LocalAssetBundleLoader();
            StartCoroutine(assetBundleLoader.LoadBundle(inputField.text, 
                (AssetBundle assetBundle) => 
                {
                    StartCoroutine(GameManager.Instance.DataHub.GameGraphicsConfig.LoadDataFromBundle(assetBundle));
                }));
        });
    }


    public override void Show(Action onShow = null)
    {
        base.Show(onShow);

        float cachedY = title.rectTransform.localPosition.y;
        title.rectTransform.localPosition += new Vector3(0, 200, 0);
        title.rectTransform.DOLocalMoveY(cachedY, 1.5f).SetEase(Ease.OutBounce);
    }


    public override void Hide(bool immediately = false, Action onHide = null)
    {
        if (!immediately)
        {
            Vector3 cached = title.rectTransform.localPosition;
            title.rectTransform
                .DOLocalMoveY(cached.y + 500, 4)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => title.rectTransform.localPosition = cached);
        }

        base.Hide(immediately, onHide);
    }

    #endregion
}
