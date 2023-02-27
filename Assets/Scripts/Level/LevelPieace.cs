using UnityEngine;

public class LevelPieace : MonoBehaviour
{
    [SerializeField] private LevelPieceType _type;

    public LevelPieceType Type { get; private set; }
    public int Level { get; private set; }
    public int Index { get; private set; }
    public int UnitCount { get; private set; }

    public virtual int Initialize(int level, int index, int unitCount)
    {
        Level = level;
        Index = index;
        UnitCount = unitCount;
        return unitCount;
    }
}

public enum LevelPieceType
{
    Empty = 0,
    Trap = 1,
    Crowd = 2,
    Gates = 3
}
