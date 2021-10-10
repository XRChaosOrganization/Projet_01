using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelComponent : MonoBehaviour
{
    public Slider Progressbar;
    public int world;
    public int currentScore;
    public Text timerDisplay;
    public float timer;

    [System.Serializable]
   public struct LevelTresholds
    {
        public string tresholdname;
        public int scoreTreshold;
        public bool isTresholdPassed;
        public Image tresholdSprite;
        public GameObject tresholdPrefab;
    }

    public List<LevelTresholds> tresholdList;

    public void Update()
    {
        
        timer -= Time.deltaTime;
        timerDisplay.text = string.Format("{0:00.00}", timer);
        
        
    }

    public void OnEnable()
    {
        BasketComponent.Score += UpdatePoints;
    }

    private void OnDisable()
    {
        BasketComponent.Score -= UpdatePoints;
    }
    public void InitLevelComponent(Slider _progressbar,Text _timer)
    {
        timerDisplay = _timer;
        Progressbar = _progressbar;
        Progressbar.minValue = 0;
        Progressbar.maxValue = tresholdList[tresholdList.Count - 1].scoreTreshold;
    }
    void UpdatePoints(int _points)
    {
        currentScore += _points;
        Progressbar.value = currentScore;
        
              
        for (int i = 0; i < tresholdList.Count; i++)
        {
            LevelTresholds t = tresholdList[i];
           
            if (currentScore >= t.scoreTreshold && t.isTresholdPassed== false)
            {
                Debug.Log(t.tresholdname + " is passed !");
                t.isTresholdPassed = true;
                tresholdList[i] = t;

            }
        }

    }
    public enum levelDifficulty {T1, T2, T3, T4, T5, T6};

    //public List<Spawner> spawners;
    //public CollectibleTemplateSO collectibleTemplate;

}
