using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{

    public int world;

    public int scoreToComplete;
    public int scoreToPlatinum;
    public float timer;

    public enum levelDifficulty {T1, T2, T3, T4, T5, T6};

    //public List<Spawner> spawners;
    //public CollectibleTemplateSO collectibleTemplate;

}
