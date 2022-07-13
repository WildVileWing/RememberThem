using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumEdges : MonoBehaviour
{
    [SerializeField] private int[] edges;
    private int index = 1;

    [SerializeField] private Button lowerBtn, higherBtn;
    [SerializeField] private TMP_Text numTxt;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        numTxt.text = gm.NumEdges.ToString();

        lowerBtn.onClick.AddListener(() => ChangeDiceCount(-1));
        higherBtn.onClick.AddListener(() => ChangeDiceCount(1));

        ChangeDiceCount(0);
    }

    private void ChangeDiceCount(int value)
    {
        index = index + value >= edges.Length ? edges.Length - 1 : index + value < 0 ? 0 : index + value;

        numTxt.text = gm.NumEdgesChange(edges[index]).ToString();
    }
}
