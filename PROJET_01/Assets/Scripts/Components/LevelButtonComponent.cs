using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonComponent : MonoBehaviour
{
    public LevelManagerComponent levelManager;
    public GameObject levelPrefab;
    public string levelName;
    
    

    public void InitButton(LevelManagerComponent _levelManager, GameObject _levelPrefab, string _levelname)
    {
      
        levelManager = _levelManager;
        levelPrefab = _levelPrefab;
        levelName = _levelname;

        GetComponentInChildren<TextMeshProUGUI>().text = levelName;
       

    }
    public void LevelToLoad()
    {
        levelManager.LoadLevel(levelPrefab);
        UIManagerComponent.Instance.CloseGameMenuPanel();
    }
}
