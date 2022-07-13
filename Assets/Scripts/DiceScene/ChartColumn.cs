using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChartColumn : MonoBehaviour
{
    [SerializeField] private TMP_Text numTxt, statisticTxt, amountTxt;
    [SerializeField] private Image image;

    public void Init(int num, int statisticCount, float statisticPercent)
    {
        numTxt.text = num.ToString();
        image.fillAmount = statisticPercent / 100;

        statisticTxt.text = $"{statisticPercent:F2}%";
        amountTxt.text = $"{statisticCount}";

        float posY = Mathf.Clamp
            (statisticTxt.transform.localPosition.y * image.fillAmount, 75, 1000);

        statisticTxt.transform.localPosition = new Vector3
            (statisticTxt.transform.localPosition.x, posY);

        amountTxt.transform.localPosition = new Vector3
            (amountTxt.transform.localPosition.x, posY - 100);
    }
}
