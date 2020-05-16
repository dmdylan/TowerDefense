using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField] private LevelDifficulty levelDifficulty = null;
    public LevelDifficulty LevelDifficulty => levelDifficulty;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }
}
