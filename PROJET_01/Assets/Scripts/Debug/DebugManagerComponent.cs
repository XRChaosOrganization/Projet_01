using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 

public class DebugManagerComponent : MonoBehaviour
{
    public GameObject debugMenu;

    [Space]
    [Header("Paddle Changement")]

    public TMP_Dropdown paddleFunctionModeDropDown;
    public Slider maxAngleSlider;
    public Slider velocitySlider;
    public TMP_Text angleFeedback; 
    public TMP_Text velocityFeedback;
    PaddleBehavior paddle;

    [Space]
    [Header("Level Selection")]

    public TMP_Dropdown levelSelectDropDown;
    public List<GameObject> levelList;

    GameObject currentLevel;



    private void Start()
    {
        //Turn off canvas if its displayed 
        if (debugMenu.activeSelf)
            debugMenu.SetActive(false);

        //Init paddle changement option 
        InitPaddleModificationsDebug();

        currentLevel = Instantiate(levelList[0]);
    }

    public void ToggleDebugMenu ()
    {
        if (debugMenu.activeSelf)
        {
            debugMenu.SetActive(false);
            Time.timeScale = 1;
        }

        else
        {
            Time.timeScale = 0f;
            
            debugMenu.SetActive(true);
        }
            
    }

    //PADDLE MODIFICATIONS
    private void InitPaddleModificationsDebug ()
    {

        paddle = FindObjectOfType<PaddleBehavior>();

        //maxAngleSlider.value = paddle.maxAngle;
        //velocitySlider.value = paddle.velocity;

        //velocityFeedback.text = paddle.velocity.ToString();
        //angleFeedback.text = paddle.maxAngle.ToString();
    }

    public void UpdatePaddle ()
    {
        paddle = FindObjectOfType<PaddleBehavior>(); 

        switch (paddleFunctionModeDropDown.value)
        {
            case 0 :
                paddle.spreadMode = PaddleBehavior.SpreadMode.LINEAR;
                paddle.maxAngle = 50;
                paddle.velocity = 95;
                break;
            case 1:
                paddle.spreadMode = PaddleBehavior.SpreadMode.SINE_IN;
                paddle.maxAngle = 50;
                paddle.velocity = 95;
                break;
            case 2:
                paddle.spreadMode = PaddleBehavior.SpreadMode.CUBIC_OUT;
                paddle.maxAngle = 30;
                paddle.velocity = 95;
                break;
            default:
                break;
        }

        

        //paddle.maxAngle = (int) maxAngleSlider.value;
        //paddle.velocity = (int) velocitySlider.value;

        //velocityFeedback.text = paddle.velocity.ToString();
        //angleFeedback.text = paddle.maxAngle.ToString();
    }


    public void LevelChange()
    {

        Destroy(currentLevel);
        currentLevel = Instantiate(levelList[levelSelectDropDown.value]);

    }
}
