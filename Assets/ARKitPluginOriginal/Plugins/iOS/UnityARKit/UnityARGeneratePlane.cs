﻿using System;
using System.Text;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{
    public class UnityARGeneratePlane : MonoBehaviour
    {
        [Tooltip("検出した平面情報から生成するPrefab。設定しない場合空のGameObjectのみ作られる")]
        public GameObject planePrefab;

        [Tooltip("検出した全ての平面情報をデバッグ表示するか")]
        public bool displayPositions;

        private UnityARAnchorManager unityARAnchorManager;

        void Start()
        {
            unityARAnchorManager = new UnityARAnchorManager();
            UnityARUtility.InitializePlanePrefab(planePrefab);
        }

        void OnDestroy()
        {
            unityARAnchorManager.Destroy();
        }

        void OnGUI()
        {
            if (displayPositions)
            {
                var sb = new StringBuilder();
                foreach(var arpag in unityARAnchorManager.GetCurrentPlaneAnchors())
                {
                    var ap = arpag.planeAnchor;
                    sb.AppendFormat("Center: x:{0}, y:{1}, z:{2} - ", ap.center.x, ap.center.y, ap.center.z);
                    sb.AppendFormat("Extent: x:{0}, y:{1}, z:{2}\n", ap.extent.x, ap.extent.y, ap.extent.z);
                }
                GUI.Label(new Rect(10, 40, 800, 500), sb.ToString());
            }
        }
    }
}

