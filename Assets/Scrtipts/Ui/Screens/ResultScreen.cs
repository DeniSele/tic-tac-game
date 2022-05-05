using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : ScreenBase
{
    #region Fields

    [Header("Result screen settings")]
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Transform buttonsRoot;

    [Space]
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button toMenuButton;

    #endregion



    #region Public methods

    public override void Initialize(UiService uiService)
    {
        base.Initialize(uiService);

        uiService.EventService.TicTacEvents.OnGameEnded += SetResultText;

        restartGameButton.onClick.AddListener(() => uiService.ShowScreen(ScreenType.GameScreen));
        toMenuButton.onClick.AddListener(() => uiService.ShowScreen(ScreenType.MenuScreen));
    }


    public override void Show(Action onShow = null)
    {
        base.Show(onShow);

        float cachedY = resultText.rectTransform.localPosition.y;
        resultText.rectTransform.localPosition += new Vector3(0, 200, 0);
        resultText.rectTransform.DOLocalMoveY(cachedY, 1.5f).SetEase(Ease.OutBounce);

        float cachedY1 = buttonsRoot.localPosition.y;
        buttonsRoot.localPosition += new Vector3(0, -200, 0);
        buttonsRoot.DOLocalMoveY(cachedY1, 1.5f).SetEase(Ease.OutBounce);
    }

    #endregion



    #region Private methods

    private void SetResultText(GameResultType gameResult)
    {
        switch (gameResult)
        {
            case GameResultType.Draw:
                resultText.text = "Draw...";
                break;
            case GameResultType.Player1Win:
                resultText.text = "Player 1 won!";
                break;
            case GameResultType.Player2Win:
                resultText.text = "Player 2 won!";
                break;
            default:
                resultText.text = $"Unknown result {gameResult}.";
                break;
        }
    }

    #endregion
}
