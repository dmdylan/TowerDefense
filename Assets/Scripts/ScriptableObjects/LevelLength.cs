using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum Length { Short, Medium, Long }
[CreateAssetMenu(fileName = "New Level Length", menuName = "Level Info/Length Info")]
public class LevelLength : ScriptableObject
{
    public Length Length; 
}
