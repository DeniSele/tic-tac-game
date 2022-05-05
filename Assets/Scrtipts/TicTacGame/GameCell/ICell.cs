public interface ICell
{
    int Index { get; }
    GameSignType CellSignType { get; }

    void Initialize(int index);
    void Reset();
    void SetSign(GameSignType gameSign);
}
