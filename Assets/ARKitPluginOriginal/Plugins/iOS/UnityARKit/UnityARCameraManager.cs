using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class UnityARCameraManager : MonoBehaviour
{

    public Camera m_camera;
    private UnityARSessionNativeInterface m_session;
    private Material savedClearMaterial;

    // https://developer.apple.com/documentation/arkit/arworldtrackingsessionconfiguration/2867263-planedetection
    [Tooltip("平面検出")]
    [SerializeField]
    UnityARPlaneDetection planeDetection = UnityARPlaneDetection.None;

    // https://developer.apple.com/documentation/arkit/arsessionconfiguration.worldalignment
    [Tooltip("3D空間と端末の動作の関連付け")]
    [SerializeField]
    UnityARAlignment alignment = UnityARAlignment.UnityARAlignmentGravity;

    // https://developer.apple.com/documentation/arkit/arframe/2887449-rawfeaturepoints
    [Tooltip("ポイントクラウド")]
    [SerializeField]
    bool pointCloud = false;

    // https://developer.apple.com/documentation/arkit/arframe/2878306-lightestimate
    [Tooltip("周囲の明るさ推定")]
    [SerializeField]
    bool lightEstimation = false;

	// https://developer.apple.com/documentation/arkit/arsession.runoptions/2875727-resettracking
	[Tooltip("前回のARKitセッションのトラッキング結果をクリアして始める")]
    [SerializeField]
	bool resetTracking = true;

	// https://developer.apple.com/documentation/arkit/arsession.runoptions/2878307-removeexistinganchors
	[Tooltip("前回検出した平面情報をクリアして始める")]
	[SerializeField]
	bool removeExistingAnchors = true;

    void Start()
    {
#if !UNITY_EDITOR
		Application.targetFrameRate = 60;
		m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();
        ARKitWorldTackingSessionConfiguration config = new ARKitWorldTackingSessionConfiguration();
        config.planeDetection = planeDetection;
        config.alignment = alignment;
        config.getPointCloudData = pointCloud;
        config.enableLightEstimation = lightEstimation;

		var runOption = resetTracking ? UnityARSessionRunOption.ARSessionRunOptionResetTracking : 0;
		runOption |= removeExistingAnchors ? UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors : 0;
        m_session.RunWithConfigAndOptions(config, runOption);

		if (m_camera == null) {
			m_camera = Camera.main;
		}
#endif
    }

    public void SetCamera(Camera newCamera)
    {
        if (m_camera != null)
        {
            UnityARVideo oldARVideo = m_camera.gameObject.GetComponent<UnityARVideo>();
            if (oldARVideo != null)
            {
                savedClearMaterial = oldARVideo.m_ClearMaterial;
                Destroy(oldARVideo);
            }
        }
        SetupNewCamera(newCamera);
    }

    private void SetupNewCamera(Camera newCamera)
    {
        m_camera = newCamera;

        if (m_camera != null)
        {
            UnityARVideo unityARVideo = m_camera.gameObject.GetComponent<UnityARVideo>();
            if (unityARVideo != null)
            {
                savedClearMaterial = unityARVideo.m_ClearMaterial;
                Destroy(unityARVideo);
            }
            unityARVideo = m_camera.gameObject.AddComponent<UnityARVideo>();
            unityARVideo.m_ClearMaterial = savedClearMaterial;
        }
    }

    // Update is called once per frame

#if !UNITY_EDITOR
	void Update () {
		
        if (m_camera != null)
        {
            // JUST WORKS!
            Matrix4x4 matrix = m_session.GetCameraPose();
			m_camera.transform.localPosition = UnityARMatrixOps.GetPosition(matrix);
			m_camera.transform.localRotation = UnityARMatrixOps.GetRotation (matrix);
            m_camera.projectionMatrix = m_session.GetCameraProjection ();
        }

	}
#endif

}
