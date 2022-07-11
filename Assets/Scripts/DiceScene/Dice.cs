using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;

    private WaitForSeconds waitTime, rollingTime;
    private int index;

    private Coroutine cor;

    private void Awake()
    {
        waitTime = new WaitForSeconds(Random.Range(0.1f, 0.2f));
        rollingTime = new WaitForSeconds(Random.Range(2f, 5f));

        StartCoroutine(RollingManager());
    }

    private IEnumerator RollingManager()
    {
        cor = StartCoroutine(Rolling());

        yield return rollingTime;
        StopCoroutine(cor);
        EventManager.inst.DiceRollEndInv(index + 1);
    }

    private IEnumerator Rolling()
    {
        while (true)
        {
            yield return waitTime;

            var temp = Random.Range(0, sprites.Length);

            if (temp == index) index = Random.Range(0, sprites.Length);
            else index = temp;

            image.sprite = sprites[index];
        }
    }
}
