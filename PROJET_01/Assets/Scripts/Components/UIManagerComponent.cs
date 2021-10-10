using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerComponent : MonoBehaviour
{
    public static UIManagerComponent Instance;
    public GameObject mainMenu;
    public GameObject settingsMenuButtons;
    public GameObject creditsMenuButtons;
    public GameObject worldSelectorMenuButtons;
    public ProgressBarComponent progressBar;
    public Text timerDisplay;

    private void Awake()
    {
        Instance = this;
    }
    public void CloseGameMenuPanel()
    {
        mainMenu.SetActive(false);
    }
    public void InitProgressBar(LevelComponent _level)
    {
        _level.InitLevelComponent(progressBar.GetComponent<Slider>(),timerDisplay);
        progressBar.SetProgressBarTresholds(_level);
    }
}
