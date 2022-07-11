using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Estimated : MonoBehaviour
{
    [SerializeField] private Button lowerBtn, higherBtn;
    [SerializeField] private TMP_Text numTxt;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        numTxt.text = gm.EstimatedNum.ToString();

        lowerBtn.onClick.AddListener(() => ChangeDiceCount(-1));
        higherBtn.onClick.AddListener(() => ChangeDiceCount(1));
        EventManager.inst.EstimatedNumUpdate += UpdateEst;
    }

    private void ChangeDiceCount(int value)
    {
        gm.EstimatedChange(value).ToString();
    }
    private void UpdateEst(int value) => numTxt.text = value.ToString();

    private void OnDestroy()
    {
        EventManager.inst.EstimatedNumUpdate -= UpdateEst;
    }
}
