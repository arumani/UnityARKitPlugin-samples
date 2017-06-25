using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    string[] sceneNames;

    IEnumerator Start()
    {
        enabled = false;
    	// 誤タップ防止
        yield return new WaitForSeconds(0.5f);
        enabled = true;
    }

    void OnGUI()
    {
        var guiStyle = GUI.skin.button;
        guiStyle.fontSize = 36;

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();

        foreach (var sceneName in sceneNames)
        {
            if (GUILayout.Button(sceneName, guiStyle))
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
