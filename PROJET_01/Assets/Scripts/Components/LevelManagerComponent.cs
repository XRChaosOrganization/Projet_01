using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerComponent : MonoBehaviour
{
    
    public GameObject prefabButton;
    public GameObject levelTestRoot;
    public GameObject currentLoadedLevel;

    public List<GameObject> leveltestList;

    public void Awake()
    {
        LoadLevelsInList();
        LoadButtons();
    }

    void LoadLevelsInList()
    {

        foreach (GameObject g in Resources.LoadAll("Levels_Test", typeof(GameObject)))
        {
            leveltestList.Add(g);

        }
    }
    void LoadButtons()
    {
            for (int i = 0; i < leveltestList.Count; i++)
            {
                GameObject tempButton = Instantiate(prefabButton, levelTestRoot.transform);
                tempButton.GetComponent<LevelButtonComponent>().InitButton(this, leveltestList[i],leveltestList[i].name);
                
            
            }
          
           
    }
    public void LoadLevel(GameObject _go)
    {
        GameObject level = Instantiate(_go);
        currentLoadedLevel = level;
        UIManagerComponent.Instance.InitProgressBar(level.GetComponent<LevelComponent>());

    }
}

