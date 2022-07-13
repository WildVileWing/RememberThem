using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;

    private WaitForSeconds waitTime, rollingTime;
    private int index;
    private int edges;

    private Coroutine cor;

    public void Init(int edges)
    {
        this.edges = edges;

        waitTime = new WaitForSeconds(Random.Range(0.1f, 0.2f));
        rollingTime = new WaitForSeconds(Random.Range(2f, 5f));

        StartCoroutine(RollingManager());
    }

    private IEnumerator RollingManager()
    {
        cor = StartCoroutine(Rolling());

        yield return rollingTime;
        StopCoroutine(cor);
        EventManager.inst.DiceRollEndInv(index);
    }

    private IEnumerator Rolling()
    {
        while (true)
        {
            yield return waitTime;

            var temp = Random.Range(1f, edges);

            if (temp == index) index = (int)Random.Range(1f, edges);
            else index = (int)temp;

            text.text = index.ToString();
            //image.sprite = sprites[index];
        }
    }
}
