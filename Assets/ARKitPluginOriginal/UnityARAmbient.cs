using System.Runtime.InteropServices;
using UnityEngine.XR.iOS;

namespace UnityEngine.XR.iOS
{
    /// <summary>
    /// ARKitが推測した周囲の明るさをLightのIntensityに反映させる
    /// </summary>
    public class UnityARAmbient : MonoBehaviour
    {
        [Tooltip("現実空間の明るさを反映させるライト")]
        [SerializeField]
        Light l;
		private UnityARSessionNativeInterface m_Session;

        public void Start()
        {
#if !UNITY_EDITOR
			m_Session = UnityARSessionNativeInterface.GetARSessionNativeInterface ();
#endif
        }
#if !UNITY_EDITOR
        public void Update()
        {
            // ARKitの明度をUnityの明度に変換する
            // ARKitの明度範囲 0-2000
            // Unityの明度範囲 0-8 (for over-bright lights)
            float newai = m_Session.GetARAmbientIntensity();
            l.intensity = newai / 1000.0f;
        }
#endif
    }
}
