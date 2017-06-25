using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{
    /// <summary>
    /// 画面がタップされたらその位置でARKitのHitTest（AR空間との当たり判定）を行い、当たった位置にm_HitTransformを移動する（回転も合わせる）
    /// </summary>
	public class UnityARHitTestExample : MonoBehaviour
	{
        [Tooltip("当たった場所に移動するオブジェクト")]
		public Transform m_HitTransform;

        bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
        {
            // HitTest実行
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    Debug.Log ("Got hit!");
                    m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
                    Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
                    return true;
                }
            }
            return false;
        }
		
		// Update is called once per frame
		void Update () {
			if (Input.touchCount > 0 && m_HitTransform != null)
			{
				var touch = Input.GetTouch(0);
                // タップされた
				if (touch.phase == TouchPhase.Began)
				{
					var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

                    // HitTestの対象を指定する。
                    // https://developer.apple.com/documentation/arkit/arhittestresult.resulttype
                    ARHitTestResultType[] resultTypes = {
                        // 検出済み平面
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 

                        // 検出済み平面を延長した面
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlane,

                        // 垂直面（検出済み平面と無関係に推測したもの）
                        ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 

                        // 特徴点（ポイントクラウドで使われているものと思われる）
                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    }; 
					
                    foreach (ARHitTestResultType resultType in resultTypes)
                    {
                        if (HitTestWithResultType (point, resultType))
                        {
                            return;
                        }
                    }
				}
			}
		}

	
	}
}

