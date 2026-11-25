# XREAL AURA 向け Android XR スターターキット

[English](./README.md)

| Scene 0〜2 | Scene 3〜5 |
| --- | --- |
| [![Android XR スターターサンプルのデモ動画（YouTube）](https://img.youtube.com/vi/52wZrd1oV24/maxresdefault.jpg)](https://youtu.be/52wZrd1oV24) | [![Android XR スターターサンプル ver.2 のデモ動画（YouTube）](https://i.ytimg.com/vi/FxXNzq-5ClU/maxresdefault.jpg)](https://youtu.be/FxXNzq-5ClU) |
| ▶ [デモ動画を YouTube で見る](https://youtu.be/52wZrd1oV24) | ▶ [デモ動画を YouTube で見る](https://youtu.be/FxXNzq-5ClU) |

XREAL AURA 向けの Android XR アプリ開発を始めるための Unity プロジェクトです。6 つのサンプルシーンで、3D オブジェクトの表示、ハンドトラッキングによる操作、空間 UI の実装、カメラ画像へのアクセス、実物による仮想オブジェクトの遮蔽（オクルージョン）、画像マーカーの認識と追跡を試せます。OpenXR、XR Interaction Toolkit、Universal Render Pipeline（URP）を使用しています。

## サンプルシーン

`OpenXR-Sample` を Unity で開き、`Assets/Scenes` から各シーンを選びます。

| シーン | 試せること |
| --- | --- |
| [`0-ShowObject`](./OpenXR-Sample/Assets/Scenes/0-ShowObject.unity) | 周囲の空間に仮想のキューブを表示します。まずこのシーンで、グラスに 3D オブジェクトが表示されることを確認できます。 |
| [`1-ObjectManipulation`](./OpenXR-Sample/Assets/Scenes/1-ObjectManipulation.unity) | 片手でキューブをつかんで移動・回転できます。両手でつかんで手の間隔を変えると、位置と向きを保ったまま拡大・縮小できます。 |
| [`2-UI`](./OpenXR-Sample/Assets/Scenes/2-UI.unity) | ワールド空間の Canvas にあるボタンでキューブの表示・非表示を切り替えます。入力欄を選んで空間キーボードで文字を入力すると、キューブの上のラベルに反映されます。 |
| [`3-CameraImage`](./OpenXR-Sample/Assets/Scenes/3-CameraImage.unity) | カメラの権限を許可すると、デバイスのカメラ映像が周囲の空間のパネルに表示されます。利用できるカメラの一覧と画像サイズも、ラベルに表示します。 |
| [`4-Occlusion`](./OpenXR-Sample/Assets/Scenes/4-Occlusion.unity) | 壁のような実物は、その後ろにあるキューブを隠します。`1-ObjectManipulation` と同じ操作でキューブを壁にめり込ませると、壁の中に入った部分が隠れます。Android XR がオクルージョンに必要とする権限は、求められたときに許可してください。 |
| [`5-ImageTracking`](./OpenXR-Sample/Assets/Scenes/5-ImageTracking.unity) | 画面などに表示したマーカー画像（`Assets/ImageTracking/Markers/logo.png`）を認識すると、その上にキューブが現れ、マーカーの位置と向きに追従します。マーカーを見失ったときは、キューブがその場にとどまります（Inspector で、隠す設定にもできます）。Android XR が画像トラッキングに必要とする権限は、求められたときに許可してください。 |

## サンプルスクリプト

`Assets/Scripts` には、このスターターキット用に作成した補助スクリプトが入っています。自分のアプリでは、必要に応じて再利用できます。

| スクリプト | 役割 |
| --- | --- |
| [`TwoHandsScaleController.cs`](./OpenXR-Sample/Assets/Scripts/TwoHandsScaleController.cs) | 両手で拡大・縮小している間はキューブの向きを固定し、設定に応じて位置も固定します。 |
| [`ToggleCubeVisibility.cs`](./OpenXR-Sample/Assets/Scripts/ToggleCubeVisibility.cs) | UI のボタンを押すと、キューブの表示・非表示を切り替えます。 |
| [`ShowInputText.cs`](./OpenXR-Sample/Assets/Scripts/ShowInputText.cs) | 入力欄の文字が変わるたびに、キューブの上のラベルを更新します。 |
| [`CameraAccess.cs`](./OpenXR-Sample/Assets/Scripts/CameraAccess.cs) | カメラの権限を要求し、利用できるカメラを一覧にして、選択したカメラの画像を Renderer に表示します。結果は任意のラベルにも出力します。 |
| [`OcclusionEnabler.cs`](./OpenXR-Sample/Assets/Scripts/OcclusionEnabler.cs) | Android XR がオクルージョンに必要とする権限をユーザーが許可したあとで、AR Occlusion Manager を有効にします。 |
| [`ImageTrackingEnabler.cs`](./OpenXR-Sample/Assets/Scripts/ImageTrackingEnabler.cs) | Android XR が画像トラッキングに必要とする権限をユーザーが許可したあとで、AR Tracked Image Manager を有効にします。 |
| [`TrackedImageFollower.cs`](./OpenXR-Sample/Assets/Scripts/TrackedImageFollower.cs) | 参照画像ライブラリでの名前で選んだ 1 枚の追跡画像に、対象のオブジェクトを追従させます。マーカーごとに 1 つ追加します。 |

## 必要な環境

- Unity Hub と Unity 6.3 LTS（`6000.3.7f1`）。[Unity エディターのダウンロード](https://unity.com/releases/editor/whats-new/6000.3.7f1)
- Unity Hub で **Android Build Support** と、その配下の **Android SDK & NDK Tools**、**OpenJDK** をインストールしてください。[Android XR 向け Unity プロジェクトの設定](https://developer.android.com/develop/xr/unity/setup?hl=ja)
- 実機で試すには、XREAL AURA グラス、Compute Puck、データ転送対応の USB-C ケーブルが必要です。

## プロジェクトを開く

```sh
git clone https://github.com/TakashiYoshinaga/AndroidXR-StarterKit.git
```

Unity Hub で `AndroidXR-StarterKit/OpenXR-Sample` フォルダーをディスクから追加して開きます。初回はパッケージの取得とアセットのインポートが完了するまで待ちます。

clone した直後のプロジェクトは、通常の **Android** プラットフォームで開きます。使用中のビルドプロファイルは、リポジトリに含まれない `Library` フォルダーに保存されるためです。このプラットフォームのままだと、Unity OpenXR Android XR パッケージが Android XR の機能の一部を無効にします。初回の起動後に一度だけ、同梱の **Android XR** プロファイルに切り替えます。

1. **File → Build Profiles** を開き、**Build Profiles** の **Android XR** を選択します。
2. プロファイルに **Android XR is currently disabled** と表示されたら、**Enable Platform** をクリックします。続けて **Switch Profile** をクリックします。

   <img src="./docs/images/enable-android-xr-platform.png" alt="Android XR プロファイルを選択した Build Profiles ウィンドウ。Android XR is currently disabled と Enable Platform ボタンが表示されている" width="600">

3. **Edit → Project Settings → XR Plug-in Management → OpenXR** を開き、**Android** タブで **Android XR Support**、**Hand Interaction Profile**、**Foveated Rendering** が有効になっているか確認します。無効のものがあれば有効にします。

`Library` フォルダーを削除しないかぎり、この手順を繰り返す必要はありません。

## XREAL AURA でビルドして実行する

1. Unity の **Project** ウィンドウで、`Assets/Scenes` から試したいシーンを開きます。最初は `0-ShowObject` を開きます。
2. **File → Build Profiles** を開き、同梱の **Android XR** プロファイルが使用中になっているか確認します。なっていなければ、[プロジェクトを開く](#プロジェクトを開く)の手順を行います。
3. 初期状態ではビルド対象のシーン一覧は空です。プロファイルで **Add Settings → Scene List** を選び、**Add Open Scenes** をクリックします。一覧では起動したいシーンだけを有効にします。[Unity のシーン一覧ガイド](https://docs.unity3d.com/6000.3/Documentation/Manual/build-profile-scene-list.html)
4. AURA の開発者向けオプションで **USB デバッグ**を有効にします。グラスを Compute Puck につないだ状態で、パソコンと Puck の充電ポートをデータ転送対応の USB-C ケーブルで接続します。実機に USB デバッグの確認が表示されたら許可します。[XREAL の実機セットアップガイド](https://tutorials.xreal.com/docs/aura/developers/getting-started/)
5. **Run Device** で実機を選び、**Build and Run** をクリックして APK のビルド、インストール、起動を行います。別のサンプルを試すときは、そのシーンを一覧に追加し、そのシーンだけを有効にして再ビルドします。[Unity の Android ビルドガイド](https://docs.unity3d.com/6000.3/Documentation/Manual/android-BuildProcess.html)

## ライセンス

本プロジェクトのオリジナルコードは [MIT License](./LICENSE) で公開しています。

`OpenXR-Sample/Assets/Samples` に含まれる Unity のサンプルアセットについては、[XR Interaction Toolkit](https://unity.com/legal/licenses/unity-companion-license) と [XR Hands](https://unity.com/legal/licenses/unity-package-distribution-license) の各パッケージのライセンス条件を参照してください。

`OpenXR-Sample/Assets/Shaders/Occlusion` のオクルージョン用シェーダーは、Unity Technologies の [AR Foundation Samples](https://github.com/Unity-Technologies/arfoundation-samples)（`6.6` ブランチ）から改変せずにコピーしたもので、[Unity Companion License](https://unity.com/legal/licenses/unity-companion-license) のもとで提供されています。詳細は同フォルダーの [`LICENSE.md`](./OpenXR-Sample/Assets/Shaders/Occlusion/LICENSE.md) を参照してください。
