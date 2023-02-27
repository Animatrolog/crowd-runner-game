using UnityEngine;
using TMPro;

public class Arithmetiser : MonoBehaviour
{
    [SerializeField] private TMP_Text _expressionText;

    public string Expression { get; private set; }
    public int Number { get; private set; }

    public void Initialize(string expression, int number)
    {
        Expression = expression;
        Number = number;
        _expressionText.text = Expression + Number.ToString();
    }

    public int Calculate(int number)
    {
        switch (Expression)
        {
            case "+":
                return number + Number;
            case "x":
                return number * Number;
            default:
                Debug.LogError("Expression is not valid");
                break;
        }
        return number;
    }
}
