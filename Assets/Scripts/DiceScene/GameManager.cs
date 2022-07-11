using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject DicePrefab;
    [SerializeField] private Transform parent;

    [SerializeField] private int minNumDice, maxNumDice;
    [SerializeField] private int minNumEdges, maxNumEdges;
    private int minEstimated, maxEstimated;

    public int NumDice { get; private set; } = 1;
    public int NumEdges { get; private set; } = 6;
    public int EstimatedNum { get; private set; }

    private int diceSum = 0;
    private int diceRolled = 0;

    private EventManager events;

    private void Awake()
    {
        events = GetComponent<EventManager>();
        events.DiceRollEnd += DiceRollAdd;
        events.StartGame += StartGame;
        events.EndGame += EndGame;

        RecalculateEst();

        EstimatedNum = minEstimated;
    }

    private void StartGame()
    {
        diceSum = 0;
        diceRolled = 0;

        for (int i = 0; i < NumDice; i++)
        {
            Instantiate(DicePrefab, parent, false);
        }
    }
    private void EndGame()
    {
        for (int i = parent.childCount; i > 0; i--)
        {
            Destroy(parent.GetChild(i - 1).gameObject);
        }
    }
    private void DiceRollAdd(int value)
    {
        diceSum += value;
        diceRolled++;

        events.CurrentNumUpdateInv(diceSum);

        if (diceRolled < NumDice) return;

        if (diceSum == EstimatedNum)
            events.GameCompletedInv(true, 1f / maxEstimated);
        else
            events.GameCompletedInv(false, 1f / maxEstimated);
    }

    public int NumDiceChange(int value)
    {
        var temp = NumDice + value;

        if (temp < minNumDice || temp > maxNumDice) return NumDice;

        NumDice = temp;
        RecalculateEst();

        return NumDice;
    }
    public int NumEdgesChange(int value)
    {
        var temp = NumEdges + value;

        if (temp < minNumEdges || temp > maxNumEdges) return NumEdges;

        NumEdges = temp;
        RecalculateEst();

        return NumEdges;
    }
    public int EstimatedChange(int value)
    {
        var temp = EstimatedNum + value;

        if (temp < minEstimated || temp > maxEstimated) return EstimatedNum;

        EstimatedNum = temp;

        events.EstimatedNumUpdateInv(EstimatedNum);
        return EstimatedNum;
    }
    private void RecalculateEst()
    {
        minEstimated = NumDice;
        maxEstimated = NumDice * NumEdges;

        if (EstimatedNum > maxEstimated)
        {
            EstimatedNum = maxEstimated;
            events.EstimatedNumUpdateInv(EstimatedNum);
        }
        else if (EstimatedNum < minEstimated)
        {
            EstimatedNum = minEstimated;
            events.EstimatedNumUpdateInv(EstimatedNum);
        }
    }

    private void OnDestroy()
    {
        events.DiceRollEnd -= DiceRollAdd;
        events.StartGame -= StartGame;
        events.EndGame -= EndGame;
    }
}
