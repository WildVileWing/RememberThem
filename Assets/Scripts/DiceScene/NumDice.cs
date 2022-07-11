using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumDice : MonoBehaviour
{
    [SerializeField] private Button lowerBtn, higherBtn;
    [SerializeField] private TMP_Text numTxt;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        numTxt.text = gm.NumDice.ToString();

        lowerBtn.onClick.AddListener(() => ChangeDiceCount(-1));
        higherBtn.onClick.AddListener(() => ChangeDiceCount(1));
    }

    private void ChangeDiceCount(int value)
    {
        numTxt.text = gm.NumDiceChange(value).ToString();
    }
}
