using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// デバッグ用にUnity上の座標を表示する
/// </summary>
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
            var labelGuiStyle = new GUIStyle();
            labelGuiStyle.fontSize = 16;
            GUI.Label(new Rect(10, 10, 400, (targets.Length * 18 + 10)), sb.ToString(), labelGuiStyle);
        }

        GUILayout.FlexibleSpace();

        var buttonGuiStyle = GUI.skin.button;
        buttonGuiStyle.fontSize = 22;
        if (GUILayout.Button("Return to Menu", buttonGuiStyle))
        {
            SceneManager.LoadScene("00_Menu");
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
