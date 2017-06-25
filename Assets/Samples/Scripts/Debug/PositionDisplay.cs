using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

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

    void Start()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 16;
    }

    void OnGUI()
    {
        if (targets != null)
        {
            var sb = new StringBuilder();
            foreach(var t in targets)
            {
                sb.AppendFormat(t.label + " " + t.transform.position + "\n");
            }
            GUI.Label(new Rect(10, 10, 400, (targets.Length * 18 + 10)), sb.ToString(), guiStyle);
        }
    }
}
