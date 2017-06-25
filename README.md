# このレポジトリについて

[Unity ARKit Plugin](https://bitbucket.org/Unity-Technologies/unity-arkit-plugin)を元に、機能ごとのミニマム構成に絞ったシーンを作り、スクリプトにコメントを追加したものです。

Pluginのサンプルで機能は全てカバーされてはいるのですが、複数機能が混じっていて若干分かり難く感じたため、機能単体でそれぞれ何をしているか理解できるようにする意図で作っています。

- Unity ARKit Pluginに含まれていたファイルは Assets/ARKitPluginOriginal に移動しました（サンプルシーンがルートに置いてあるなど、今回作ったものと混同しそうだったため）。
- 元のPluginのスクリプトは以下の部分のみ手を加えました
  - サンプルシーンで利用したスクリプトのみ、日本語コメントやリファレンスへのリンクを追加
  - コンポーネントの設定をInspectorから行えるように変更
- 元にしたPluginのバージョンは、本プロジェクト作成時点での最新コミット 1d588da （2017/6/15）です。

# ARKitについて

## ARKit全般

- [AppleのARKitポータル](https://developer.apple.com/arkit/)
- [Unity ARKit Pluginレポジトリ](https://bitbucket.org/Unity-Technologies/unity-arkit-plugin)
- [アップルの「ARKit」を徹底解説、技術よりも戦略がすごい（西田宗千佳）](http://japanese.engadget.com/2017/06/21/arkit/)
- [ARKitができる事、できない事](http://qiita.com/arumani/items/6efad65a17e2eafde5a1)

## 開発環境の作り方

- [UnityでiOSのARKitを動かす手順](http://qiita.com/arumani/items/aac7372a171e441e1f16)
- [Unityで「ARKit」を使ってみよう](https://tech.sge.cyberagent.co.jp/gameengine/unity/arkit/)

# 各シーンの説明

Samples/Scenes 以下に含まれる各シーンの説明です
各シーンで特にARKitの機能を使っているクラスやオブジェクトについて、都度説明をいれています。

## 01_PositionalTrackingVR

通常の3Dゲーム（カメラ映像が無く全てCGで構成された3D空間）のMain CameraをARKitのポジショントラッキングで移動/回転できるようにするサンプルです。

### UnityARCameraManager

- ARKitを初期化する（初期化内容はInspectorで設定できるよう変更しました）
- 端末カメラの3D空間上での位置と回転を取得し、Unity上のCameraに反映させる

### UnityARCameraNearFar（Main Cameraに追加）

- このコンポーネントを追加しない場合、カメラのNearクリップ/Farクリップがプラグインの内部処理によってに0.01 / 30.0 に設定されてしまいます。このコンポーネントを追加すると、追加したカメラのNear/Farの値が使われるようになり、Unity上で意図したクリッピングになります。

## 02_PositionalTrackingAR

ARの基本形として、カメラ映像の中にARで3Dオブジェクトが重なり、ポジトラで移動できるだけのものです。01シーンとの違いは、Main Cameraの背景がSkybox → Depth Onlyになった点と、同オブジェクトにUnityARVideoを追加した点です。

### UnityARVideo

- このコンポーネントをCameraオブジェクトにつけると、そのCameraの背景としてARKitによるカメラ画像が表示されます
- Clear MaterialにYUVMaterialを設定します

## 03_PlaneDetection

水平面（床や机など）の検出を行います。検出した平面が枠で示されます。

### GeneratePlanes

- UnityARAnchorManagerを生成し、検出した平面を可視化するPrefabを設定する

### UnityARAnchorManager

- ARKitの平面検出/更新/削除イベントを受信する
- 検出/更新の場合は上記で設定したPrefabを作り、削除であればそれを削除する

## 04_PointCloud

ポイントクラウド（3次元の点座標情報）を取得し、点の位置にパーティクルを表示するサンプルです。

### PointCloudParticle

- ARKitのフレーム更新を受け取る
- フレーム情報からポイントクラウドを取得
- 各ポイントの座標に点（Particle）を表示する

## 05_HitTest

画面をタップすると、3D空間上のその位置にCubeを移動します。

### UnityARHitTestExample

- 画面のタップ位置をHitTestを行う座標に変換する
- タップ位置から当たり判定を実行する

## 06_Intensity

周囲の明るさを推定し、Directional Lightに反映するサンプルです。

### UnityARAmbient
- ARKitから受け取った明るさ情報をUnity用に変換し、ライトに反映する

