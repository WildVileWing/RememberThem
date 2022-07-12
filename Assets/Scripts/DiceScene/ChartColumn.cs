using TMPro;
using UnityEngine;

public class ChartColumn : MonoBehaviour
{
    [SerializeField] private TMP_Text numTxt, statisticTxt;

    public void Init(int num, int statisticCount, float statisticChance)
    {
        numTxt.text = num.ToString();
        statisticTxt.text = $"{statisticCount}:{statisticChance:F2}";
    }
}
