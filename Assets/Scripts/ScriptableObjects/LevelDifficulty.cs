using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy, Normal, Hard, Extreme}
[CreateAssetMenu(fileName ="New Difficulty Info", menuName = "Level Info/Difficulty Info")]
public class LevelDifficulty : ScriptableObject
{
    public Difficulty Difficulty;
}
