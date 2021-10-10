using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProgressBarComponent : MonoBehaviour
{
    
    public LevelComponent Level;
    public RectTransform ProgressBar;
    public Transform tresholdRoot;
    
   

    public void SetProgressBarTresholds(LevelComponent _level)
    {
        Level = _level;
        float totalwidth = ProgressBar.rect.width;

        

        for (int i = 0; i < Level.tresholdList.Count - 1; i++)
        {

            GameObject medal = Instantiate(Level.tresholdList[i].tresholdPrefab,tresholdRoot);
            medal.GetComponent<RectTransform>().localPosition = (float) Level.tresholdList[i].scoreTreshold / Level.tresholdList[Level.tresholdList.Count - 1].scoreTreshold * totalwidth * Vector3.right;
            

        }

    }
}
