using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    static GameEvents instance;

    public static GameEvents Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameEvents>();
            }
            return instance;
        }
    }

    private void Awake() => instance = this;

    public event Action OnBuiltAStructure;
    public void BuiltAStructure() => OnBuiltAStructure?.Invoke();

    public event Action OnWaveStarted;
    public void WaveStarted() => OnWaveStarted?.Invoke();

    public event Action OnWaveEnded;
    public void WaveEnded() => OnWaveEnded?.Invoke();
}
