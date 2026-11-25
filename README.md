# Android XR Starter Kit for XREAL AURA

[日本語](./README.ja.md)

| scenes 0–2 | scenes 3–5 |
| --- | --- |
| [![Android XR Starter Sample demo video](https://img.youtube.com/vi/52wZrd1oV24/maxresdefault.jpg)](https://youtu.be/52wZrd1oV24) | [![Android XR Starter Sample ver.2 demo video](https://i.ytimg.com/vi/FxXNzq-5ClU/maxresdefault.jpg)](https://youtu.be/FxXNzq-5ClU) |
| ▶ [Watch the demo video on YouTube](https://youtu.be/52wZrd1oV24) | ▶ [Watch the demo video on YouTube](https://youtu.be/FxXNzq-5ClU) |

A Unity starter project for building Android XR apps for XREAL AURA. The six sample scenes show how to render a 3D object, manipulate it with hand tracking, add an interactive UI in 3D space, access the camera image, hide virtual content behind real objects with occlusion, and recognize and track an image marker. The project uses OpenXR, XR Interaction Toolkit, and the Universal Render Pipeline (URP).

## Sample scenes

Open these scenes from `Assets/Scenes` in the `OpenXR-Sample` Unity project.

| Scene | What to try |
| --- | --- |
| [`0-ShowObject`](./OpenXR-Sample/Assets/Scenes/0-ShowObject.unity) | Look for a virtual cube in your surroundings. Start here to check that 3D content appears in the glasses. |
| [`1-ObjectManipulation`](./OpenXR-Sample/Assets/Scenes/1-ObjectManipulation.unity) | Grab the cube with one hand to move and rotate it. Grab it with both hands and bring your hands closer together or farther apart to resize the cube while its position and rotation stay fixed. |
| [`2-UI`](./OpenXR-Sample/Assets/Scenes/2-UI.unity) | Press the button on the world-space canvas to show or hide the cube. Select the text field and type with the spatial keyboard to update the label above the cube. |
| [`3-CameraImage`](./OpenXR-Sample/Assets/Scenes/3-CameraImage.unity) | Grant the camera permission when asked, and the image from the device’s camera appears on a panel in your surroundings. The available cameras and the image size are shown on a label. |
| [`4-Occlusion`](./OpenXR-Sample/Assets/Scenes/4-Occlusion.unity) | Real objects such as walls hide the cube behind them. Move the cube into a wall as in `1-ObjectManipulation`, and the part inside the wall is hidden. Grant the permission Android XR requires for occlusion when asked. |
| [`5-ImageTracking`](./OpenXR-Sample/Assets/Scenes/5-ImageTracking.unity) | Show the marker image (`Assets/ImageTracking/Markers/logo.png`) on a screen, and the cube appears on it and follows its position and orientation. When the marker is lost, the cube stays where it was; you can set it to hide instead in the Inspector. Grant the permission Android XR requires for image tracking when asked. |

## Sample scripts

The scripts in `Assets/Scripts` were written for this starter kit. You can reuse them as optional helpers in your own app.

| Script | What it does |
| --- | --- |
| [`TwoHandsScaleController.cs`](./OpenXR-Sample/Assets/Scripts/TwoHandsScaleController.cs) | Keeps the cube’s rotation fixed while you resize it with both hands and can also lock its position. |
| [`ToggleCubeVisibility.cs`](./OpenXR-Sample/Assets/Scripts/ToggleCubeVisibility.cs) | Shows or hides the cube when the UI button is pressed. |
| [`ShowInputText.cs`](./OpenXR-Sample/Assets/Scripts/ShowInputText.cs) | Updates the label above the cube as the text field changes. |
| [`CameraAccess.cs`](./OpenXR-Sample/Assets/Scripts/CameraAccess.cs) | Requests the camera permission, lists the available cameras, shows the selected camera’s image on a renderer, and logs the results to an optional label. |
| [`OcclusionEnabler.cs`](./OpenXR-Sample/Assets/Scripts/OcclusionEnabler.cs) | Turns on the AR Occlusion Manager after the user grants the permission Android XR requires for occlusion. |
| [`ImageTrackingEnabler.cs`](./OpenXR-Sample/Assets/Scripts/ImageTrackingEnabler.cs) | Turns on the AR Tracked Image Manager after the user grants the permission Android XR requires for image tracking. |
| [`TrackedImageFollower.cs`](./OpenXR-Sample/Assets/Scripts/TrackedImageFollower.cs) | Makes a target object follow one tracked image, chosen by its name in the reference image library. Add one per marker. |

## Requirements

- Unity Hub and Unity 6.3 LTS (`6000.3.7f1`). [Unity Editor download](https://unity.com/releases/editor/whats-new/6000.3.7f1)
- **Android Build Support**, including **Android SDK & NDK Tools** and **OpenJDK**, installed through Unity Hub. [Android XR project setup](https://developer.android.com/develop/xr/unity/setup)
- XREAL AURA glasses, the Compute Puck, and a data-capable USB-C cable for on-device testing.

## Open the project

```sh
git clone https://github.com/TakashiYoshinaga/AndroidXR-StarterKit.git
```

Add `AndroidXR-StarterKit/OpenXR-Sample` from disk in Unity Hub and open it. Let Unity finish resolving packages and importing assets on the first launch.

A fresh clone opens on the regular **Android** platform, because the active build profile is saved in the `Library` folder, which isn’t part of the repository. While the project is on that platform, the Unity OpenXR Android XR package turns off some of the Android XR features. After the first launch, switch to the included **Android XR** profile once:

1. Go to **File → Build Profiles** and select **Android XR** under **Build Profiles**. [Build profile asset](./OpenXR-Sample/Assets/Settings/Build%20Profiles/Android%20XR.asset)
2. If the profile shows **Android XR is currently disabled**, click **Enable Platform**. Then click **Switch Profile**.
3. Go to **Edit → Project Settings → XR Plug-in Management → OpenXR**, open the **Android** tab, and check that **Android XR Support**, **Hand Interaction Profile**, and **Foveated Rendering** are turned on. Turn on any that are off.

You only need to do this again if you delete the `Library` folder.

## Build and run on XREAL AURA

1. In Unity’s **Project** window, open the scene you want to try from `Assets/Scenes`. For your first build, open `0-ShowObject`.
2. Go to **File → Build Profiles** and check that the included **Android XR** profile is active. If it isn’t, follow the steps in [Open the project](#open-the-project).
3. The build scene list starts empty. In the profile, use **Add Settings → Scene List**, then click **Add Open Scenes**. Keep only the scene you want to launch enabled in the list. [Unity scene list guide](https://docs.unity3d.com/6000.3/Documentation/Manual/build-profile-scene-list.html)
4. Enable **USB debugging** in the AURA developer options. Keep the glasses plugged into the Compute Puck and connect your computer to the puck’s charging port with a data-capable USB-C cable. Accept the USB debugging prompt on the device if it appears. [XREAL device setup guide](https://tutorials.xreal.com/docs/aura/developers/getting-started/)
5. Select the device under **Run Device** and click **Build and Run** to build, install, and launch the APK. To try another sample, add its scene to the list, enable only that scene, and build again. [Unity Android build guide](https://docs.unity3d.com/6000.3/Documentation/Manual/android-BuildProcess.html)

## License

The original project code is released under the [MIT License](./LICENSE).

Unity sample assets are included under `OpenXR-Sample/Assets/Samples`. See the respective package terms for [XR Interaction Toolkit](https://unity.com/legal/licenses/unity-companion-license) and [XR Hands](https://unity.com/legal/licenses/unity-package-distribution-license).

The occlusion shaders in `OpenXR-Sample/Assets/Shaders/Occlusion` are copied unmodified from [AR Foundation Samples](https://github.com/Unity-Technologies/arfoundation-samples) (branch `6.6`) by Unity Technologies, and are licensed under the [Unity Companion License](https://unity.com/legal/licenses/unity-companion-license). See [`LICENSE.md`](./OpenXR-Sample/Assets/Shaders/Occlusion/LICENSE.md) in that folder.
