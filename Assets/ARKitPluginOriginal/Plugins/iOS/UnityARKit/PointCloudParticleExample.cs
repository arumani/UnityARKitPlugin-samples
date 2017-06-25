using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

/// <summary>
/// ポイントクラウドの位置にパーティクルを出す
/// </summary>
public class PointCloudParticleExample : MonoBehaviour {
    public ParticleSystem pointCloudParticlePrefab;
    public int maxPointsToShow;
    public float particleSize = 1.0f;
    private Vector3[] m_PointCloudData;
    private bool frameUpdated = false;
    private ParticleSystem currentPS;
    private ParticleSystem.Particle [] particles;

	// Use this for initialization
	void Start () {
        // ARFrame（ARKitの処理単位となるフレーム。Unityのフレームとは別なので頻度やタイミングも異なる）
        UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
        currentPS = Instantiate (pointCloudParticlePrefab);
        frameUpdated = false;
	}
	
    public void ARFrameUpdated(UnityARCamera camera)
    {
        // ポイントクラウドを取得
        m_PointCloudData = camera.pointCloudData;
        // ARKitのフレームとUnityのフレームは異なるので、ARKit側が更新された時にのみUnityのUpdate()で処理するようフラグを立てる
        frameUpdated = true;
    }

	// Update is called once per frame
	void Update () {
        if (frameUpdated) {
            if (m_PointCloudData != null && m_PointCloudData.Length > 0) {
                int numParticles = Mathf.Min (m_PointCloudData.Length, maxPointsToShow);
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numParticles];
                int index = 0;
                // ポイントクラウドの位置にパーティクルを出す
                foreach (Vector3 currentPoint in m_PointCloudData) {     
                    particles [index].position = currentPoint;
                    particles [index].startColor = new Color (1.0f, 1.0f, 1.0f);
                    particles [index].startSize = particleSize;
                    index++;
                }
                currentPS.SetParticles (particles, numParticles);
            } else {
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1];
                particles [0].startSize = 0.0f;
                currentPS.SetParticles (particles, 1);
            }
            frameUpdated = false;
        }
	}
}
