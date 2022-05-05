using System;

public interface IPlayController
{
    Action<ICell> OnMoveCompleted { get; set; }
    GameSignType SignType { get; }
    void Initialize(GameSignType signType);
    void AllowMove();
    void DisallowMove();
}
