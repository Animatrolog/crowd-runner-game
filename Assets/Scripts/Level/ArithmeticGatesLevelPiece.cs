using UnityEngine;

public class ArithmeticGatesLevelPiece : LevelPieace
{
    [SerializeField] private Arithmetiser _leftGate;
    [SerializeField] private Arithmetiser _rightGate;

    public Arithmetiser LeftGate => _leftGate;
    public Arithmetiser RightGate => _rightGate;

    public override int Initialize(int level, int index, int unitCount = 1)
    {
        base.Initialize(level, index, unitCount);
        InitializeGates();
        return GetLargestPotentialCount(unitCount);
    }

    private void InitializeGates()
    {
        RandomizeGate(_leftGate);
        RandomizeGate(_rightGate);
        while ((_leftGate.Number == _rightGate.Number) && (_leftGate.Expression == _rightGate.Expression))
            RandomizeGate(_rightGate);
    }

    private int GetLargestPotentialCount(int currentCount)
    {
        int largestCount = LeftGate.Calculate(currentCount);
        int rightGateCount = RightGate.Calculate(currentCount);
        if (rightGateCount > largestCount)
            largestCount = rightGateCount;
        return largestCount;
    }

    private void RandomizeGate(Arithmetiser gate)
    {
        string expression = GetRandomExpression();
        int number;
        int randomNumber = Random.Range(1, 4);

        if (expression == "x")
        {
            int numberBase = (int)((20 - base.Index + randomNumber) * 0.2f);
            number = Mathf.Clamp(numberBase, 2, 5);
        }
        else
            number = (base.Index + 1) * randomNumber;
        gate.Initialize(expression, number);
    }

    private string GetRandomExpression()
    {
        string expreession = "+";

        if (Random.Range(0, 2) > 0)
            expreession = "x";

        return expreession;
    }

}
