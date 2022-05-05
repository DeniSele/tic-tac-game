using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController
{
    #region Fields

    public Action OnTimerEnded;

    private Tween tween;
    private float timer;

    #endregion



    #region Public methods

    public TimeController()
    {
        GameManager.Instance.ServicesHub.EventService.TicTacEvents.OnGameEnded += (_) => TryStopTween();
        timer = GameManager.Instance.DataHub.TicTacGameConfig.MoveTime;
    }


    public void StartTimer()
    {
        TryStopTween();

        tween = DOVirtual.Float(timer, 0, timer, OnTimerUpdated)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                OnTimerEnded?.Invoke();
            });
    }


    public void StopTimer()
    {
        TryStopTween();
    }

    #endregion



    #region Private methods

    private void TryStopTween()
    {
        if (tween != null)
            tween.Kill();
    }


    private void OnTimerUpdated(float time)
    {
        GameManager.Instance.ServicesHub.EventService.TicTacEvents.OnTimeUpdated?.Invoke(time);
    }

    #endregion
}
