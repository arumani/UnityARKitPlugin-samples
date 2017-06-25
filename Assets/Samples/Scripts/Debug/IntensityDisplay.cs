using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class IntensityDisplay : MonoBehaviour
{
    GUIStyle guiStyle;
    UnityARSessionNativeInterface m_Session;

    void Start()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 20;
#if !UNITY_EDITOR
		m_Session = UnityARSessionNativeInterface.GetARSessionNativeInterface ();
#endif
    }


    void OnGUI()
    {
#if !UNITY_EDITOR
        float newai = m_Session.GetARAmbientIntensity();
        GUI.Label(new Rect(10, 40, 400, 80), "Intensity " + (newai / 1000.0f), guiStyle);
#endif
    }
}
