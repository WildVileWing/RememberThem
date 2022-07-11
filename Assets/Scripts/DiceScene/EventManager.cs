using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager inst;

    public Action<int> EstimatedNumUpdate;
    public Action<int> DiceRollEnd;
    public Action StartGame;
    public Action EndGame;
    public Action<bool, float> GameCompleted;
    public Action<int> CurrentNumUpdate;

    private void Awake()
    {
        if (inst == null) inst = this;
        else Destroy(this);
    }

    public void EstimatedNumUpdateInv(int value) => EstimatedNumUpdate?.Invoke(value);
    public void DiceRollEndInv(int value) => DiceRollEnd?.Invoke(value);
    public void StartGameInv() => StartGame?.Invoke();
    public void EndGameInv() => EndGame?.Invoke();
    public void GameCompletedInv(bool value, float chance) =>
        GameCompleted?.Invoke(value, chance);
    public void CurrentNumUpdateInv(int value) => CurrentNumUpdate?.Invoke(value);
}
