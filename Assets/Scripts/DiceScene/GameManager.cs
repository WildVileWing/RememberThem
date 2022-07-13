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

    private int[] rolledEdges;

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
        rolledEdges = new int[NumDice * NumEdges];

        for (int i = 0; i < NumDice; i++)
        {
            var obj = Instantiate(DicePrefab, parent, false);
            obj.GetComponent<Dice>().Init(NumEdges);
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
        rolledEdges[value - 1] += 1;

        events.CurrentNumUpdateInv(diceSum);

        if (diceRolled < NumDice) return;

        Statistics.AddStatistic($"{NumEdges}Edges", rolledEdges);

        if (diceSum == EstimatedNum)
            events.GameCompletedInv(true, 1f / maxEstimated * 100);
        else
            events.GameCompletedInv(false, 1f / maxEstimated * 100);
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
        if (value < minNumEdges || value > maxNumEdges) return NumEdges;

        NumEdges = value;
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
