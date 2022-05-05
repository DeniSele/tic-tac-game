using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour, ICell
{
    #region Fields

    [SerializeField] private Image signImage;
    [SerializeField] private Button button;

    private int index;
    private GameSignType signType;

    #endregion



    #region Properties

    public int Index => index;
    public GameSignType CellSignType => signType;

    #endregion



    #region Public methods

    public void Initialize(int index)
    {
        this.index = index;
        Reset();

        button.onClick.AddListener(
            () => 
                GameManager.Instance.ServicesHub.EventService.TicTacEvents.OnCellClicked?.Invoke(this)
            );
    }


    public void Reset()
    {
        SetSign(GameSignType.None);
    }


    public void SetSign(GameSignType gameSignType)
    {
        signType = gameSignType;

        UpdateSignVisual();
    }

    #endregion



    #region Private methods

    private void UpdateSignVisual()
    {
        if(signType == GameSignType.None)
        {
            signImage.color = new Color(1, 1, 1, 0);
            return;
        }

        signImage.color = new Color(1, 1, 1, 1);
        signImage.sprite = GameManager.Instance.DataHub.GameGraphicsConfig.GetSignSprite(signType);
    }

    #endregion
}
