using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDisplay : MonoBehaviour
{
    [SerializeField]
    Transform target;

    void OnGUI()
    {
        if (target != null)
        {
            GUI.Label(new Rect(10, 10, 400, 30), "Camera position" + target.position);
        }
    }
}
