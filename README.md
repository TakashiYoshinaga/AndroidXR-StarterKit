# Android XR Starter Kit for XREAL AURA

[日本語](./README.ja.md)

A Unity starter project for building Android XR apps for XREAL AURA. The three sample scenes show how to render a 3D object, manipulate it with hand tracking, and add an interactive UI in 3D space. The project uses OpenXR, XR Interaction Toolkit, and the Universal Render Pipeline (URP).

## Sample scenes

Open these scenes from `Assets/Scenes` in the `OpenXR-Sample` Unity project.

| Scene | What to try |
| --- | --- |
| [`0-ShowObject`](./OpenXR-Sample/Assets/Scenes/0-ShowObject.unity) | Look for a virtual cube in your surroundings. Start here to check that 3D content appears in the glasses. |
| [`1-ObjectManipulation`](./OpenXR-Sample/Assets/Scenes/1-ObjectManipulation.unity) | Grab the cube with one hand to move and rotate it. Grab it with both hands and bring your hands closer together or farther apart to resize the cube while its position and rotation stay fixed. |
| [`2-UI`](./OpenXR-Sample/Assets/Scenes/2-UI.unity) | Press the button on the world-space canvas to show or hide the cube. Select the text field and type with the spatial keyboard to update the label above the cube. |

## Sample scripts

The scripts in `Assets/Scripts` were written for this starter kit. You can reuse them as optional helpers in your own app.

| Script | What it does |
| --- | --- |
| [`TwoHandsScaleController.cs`](./OpenXR-Sample/Assets/Scripts/TwoHandsScaleController.cs) | Keeps the cube’s rotation fixed while you resize it with both hands and can also lock its position. |
| [`ToggleCubeVisibility.cs`](./OpenXR-Sample/Assets/Scripts/ToggleCubeVisibility.cs) | Shows or hides the cube when the UI button is pressed. |
| [`ShowInputText.cs`](./OpenXR-Sample/Assets/Scripts/ShowInputText.cs) | Updates the label above the cube as the text field changes. |

## Requirements

- Unity Hub and Unity 6.3 LTS (`6000.3.7f1`). [Unity Editor download](https://unity.com/releases/editor/whats-new/6000.3.7f1)
- **Android Build Support**, including **Android SDK & NDK Tools** and **OpenJDK**, installed through Unity Hub. [Android XR project setup](https://developer.android.com/develop/xr/unity/setup)
- XREAL AURA glasses, the Compute Puck, and a data-capable USB-C cable for on-device testing.

## Open the project

```sh
git clone https://github.com/TakashiYoshinaga/AndroidXR-StarterKit.git
```

Add `AndroidXR-StarterKit/OpenXR-Sample` from disk in Unity Hub and open it. Let Unity finish resolving packages and importing assets on the first launch.

## Build and run on XREAL AURA

1. In Unity’s **Project** window, open the scene you want to try from `Assets/Scenes`. For your first build, open `0-ShowObject`.
2. Go to **File → Build Profiles**, select the included **Android XR** profile, and click **Switch Profile** if needed. [Build profile asset](./OpenXR-Sample/Assets/Settings/Build%20Profiles/Android%20XR.asset)
3. The build scene list starts empty. In the profile, use **Add Settings → Scene List**, then click **Add Open Scenes**. Keep only the scene you want to launch enabled in the list. [Unity scene list guide](https://docs.unity3d.com/6000.3/Documentation/Manual/build-profile-scene-list.html)
4. Enable **USB debugging** in the AURA developer options. Keep the glasses plugged into the Compute Puck and connect your computer to the puck’s charging port with a data-capable USB-C cable. Accept the USB debugging prompt on the device if it appears. [XREAL device setup guide](https://tutorials.xreal.com/docs/aura/developers/getting-started/)
5. Select the device under **Run Device** and click **Build and Run** to build, install, and launch the APK. To try another sample, add its scene to the list, enable only that scene, and build again. [Unity Android build guide](https://docs.unity3d.com/6000.3/Documentation/Manual/android-BuildProcess.html)

## License

The original project code is released under the [MIT License](./LICENSE).

Unity sample assets are included under `OpenXR-Sample/Assets/Samples`. See the respective package terms for [XR Interaction Toolkit](https://unity.com/legal/licenses/unity-companion-license) and [XR Hands](https://unity.com/legal/licenses/unity-package-distribution-license).
