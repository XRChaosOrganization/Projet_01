using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColorTest : MonoBehaviour
{

    public SplashBehavior splashReference;

    [Space]
    [Space]

    public SpriteRenderer[] colorChangeLayers;

    [Space]
    [Space]

    public Color[] splashColor;

    public void UpdateColor(int _bounces)
    {
        foreach (SpriteRenderer layer in colorChangeLayers)
        {
            layer.color = splashColor[_bounces];
        }
    }

    public void RestoreGrey()
    {
        foreach (SpriteRenderer layer in colorChangeLayers)
        {
            layer.color = Color.white;
        }
    }



#if UNITY_EDITOR
    [CustomEditor(typeof(ColorTest))]
    public class ColorTestEditor : Editor
    {
        

        public int sliderValue;
        int previousValue;


        public override void OnInspectorGUI()
        {

            ColorTest ct = (ColorTest)target;


            
            GUILayout.Label("");
            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUILayout.Label("=============================  Test Menu  =============================");
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUI.skin.label.fontStyle = FontStyle.Normal;
            GUILayout.Label("");
            GUI.backgroundColor = Color.green;

            


            if (GUILayout.Button("Restore Default Colors"))
                ct.splashColor = ct.splashReference.splashColor;
            GUI.backgroundColor = Color.white;




            if (GUILayout.Button("Restore Grey Colors"))
                ct.RestoreGrey();




            GUILayout.Label("");
            GUILayout.Label("Remaining Bounces : " + sliderValue);
            sliderValue = Mathf.RoundToInt(GUILayout.HorizontalSlider(sliderValue, 0f, 7f));

            if (sliderValue != previousValue)
            {
                ct.UpdateColor(sliderValue);
                previousValue = sliderValue;
            }




            GUILayout.Label("");
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("Update Changes"))
                ct.UpdateColor(sliderValue);




            GUI.backgroundColor = Color.white;
            GUILayout.Label("");
            GUILayout.Label("");
            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUILayout.Label("=============================  Splash Config  =============================");
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUI.skin.label.fontStyle = FontStyle.Normal;
            GUILayout.Label("");





            //GUI.backgroundColor = Color.green;

            //if (GUILayout.Button("Add new setup"))
            //{
            //    component.AddNewSetup();
            //}
            //GUI.backgroundColor = Color.red;
            //if (GUILayout.Button("Clean setup target(s) in herarchy"))
            //{
            //    component.CleanSetupTargets();
            //}

            //GUI.backgroundColor = Color.white;

            //Draw base inspector
            base.OnInspectorGUI();
        }
    }
#endif
}
