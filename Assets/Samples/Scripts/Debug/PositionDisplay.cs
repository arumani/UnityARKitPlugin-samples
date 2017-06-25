using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionDisplay : MonoBehaviour
{
    [System.Serializable]
    public class Target
    {
        public string label;
        public Transform transform;
    }

    [SerializeField]
    Target[] targets;

    GUIStyle guiStyle;

    void OnEnable()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 16;
    }

    IEnumerator Start()
    {
        enabled = false;
        // 誤タップ防止
        yield return new WaitForSeconds(0.5f);
        enabled = true;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();

        if (targets != null)
        {
            var sb = new StringBuilder();
            foreach (var t in targets)
            {
                sb.AppendFormat(t.label + " " + t.transform.position + "\n");
            }
            GUI.Label(new Rect(10, 10, 400, (targets.Length * 18 + 10)), sb.ToString(), guiStyle);
        }

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Return to Menu"))
        {
            SceneManager.LoadScene("00_Menu");
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
