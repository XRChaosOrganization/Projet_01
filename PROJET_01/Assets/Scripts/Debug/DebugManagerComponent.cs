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
    public PaddleBehavior paddle;
    public TMP_Dropdown paddleFunctionModeDropDown;
    public Slider maxAngleSlider;
    public Slider velocitySlider;
    public TMP_Text angleFeedback; 
    public TMP_Text velocityFeedback; 

    private void Start()
    {
        //Turn off canvas if its displayed 
        if (debugMenu.activeSelf)
            debugMenu.SetActive(false);

        //Init paddle changement option 
        InitPaddleModificationsDebug();
    }

    public void ToggleDebugMenu ()
    {
        if (debugMenu.activeSelf)
            debugMenu.SetActive(false);
        else
            debugMenu.SetActive(true);
    }

    //PADDLE MODIFICATIONS
    private void InitPaddleModificationsDebug ()
    {
        maxAngleSlider.value = paddle.maxAngle;
        velocitySlider.value = paddle.velocity;

        velocityFeedback.text = paddle.velocity.ToString();
        angleFeedback.text = paddle.maxAngle.ToString();
    }

    public void UpdatePaddle ()
    {
        switch (paddleFunctionModeDropDown.value)
        {
            case 0 :
                paddle.spreadMode = PaddleBehavior.SpreadMode.LINEAR;
                break;
            case 1:
                paddle.spreadMode = PaddleBehavior.SpreadMode.CUBIC_IN;
                break;
            case 2:
                paddle.spreadMode = PaddleBehavior.SpreadMode.CUBIC_OUT;
                break;
            case 3:
                paddle.spreadMode = PaddleBehavior.SpreadMode.SINE_IN;
                break;
            case 4:
                paddle.spreadMode = PaddleBehavior.SpreadMode.SINE_OUT;
                break;
            case 5:
                paddle.spreadMode = PaddleBehavior.SpreadMode.CIRCLE_IN;
                break;
            case 6:
                paddle.spreadMode = PaddleBehavior.SpreadMode.CIRCLE_OUT;
                break;
            default:
                break;
        }

        paddle.maxAngle = (int) maxAngleSlider.value;
        paddle.velocity = (int) velocitySlider.value;

        velocityFeedback.text = paddle.velocity.ToString();
        angleFeedback.text = paddle.maxAngle.ToString();
    }
}
