using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class DebugManagerComponent : MonoBehaviour
{
    public GameObject debugMenu; 

    [Space]
    [Header("Paddle Changement")]
    public List<GameObject> paddles; 
    public TMP_Dropdown paddleDropDown;

    private void Start()
    {
        //Turn off canvas if its displayed 
        if (debugMenu.activeSelf)
            debugMenu.SetActive(false);

        //Init paddle changement option 
        InitPaddleChangementDropDown();
    }

    public void ToggleDebugMenu ()
    {
        if (debugMenu.activeSelf)
            debugMenu.SetActive(false);
        else
            debugMenu.SetActive(true);
    }

    //PADDLE CHANGEMENT
    private void InitPaddleChangementDropDown ()
    {
        //Populate the drop down using our list of paddles 
        for (int i = 0; i < paddles.Count; i++)
        {
            paddleDropDown.options.Add(new TMPro.TMP_Dropdown.OptionData(paddles[i].gameObject.name));
        }
    }

    public void UpdatePaddle ()
    {
        for (int i = 0; i < paddles.Count; i++)
        {
            if (paddleDropDown.value == i)
                paddles[i].SetActive(true);
            else
                paddles[i].SetActive(false);

        }
    }
}
