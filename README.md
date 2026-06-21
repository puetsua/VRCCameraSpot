# VRC Camera Spot

Simple VRChat world camera spot prefab.

## Installation

1. Clone or download this repository.
2. Put the `VRCCameraSpot` folder under your Unity project's `Assets` folder.
3. Open the project in Unity and wait for scripts/materials to import.
4. Drag `Prefabs/VRCCameraSpot.prefab` into your scene.

## Setup

1. Move `SourceCamera_MoveMe` to the view you want to show.
2. Move `SpotSphere_PlayerCameraHere` and `VisibleGuideSphere` to the place where players should use their VRChat camera.
3. For VR users, place the VRChat camera inside or near the sphere.
4. For desktop users, look at the spot and press `F10`, or interact with the root object.

## Notes

- Duplicate `Materials/VRCCameraSpotOverlay.mat` and `RenderTextures/VRCCameraSpot.renderTexture` for each independent camera spot.
- The default render texture is 1280x720.
- MIT licensed. Based on `CameraSystem` by Ottpossum / rhaamo; see `THIRD_PARTY_NOTICES.md`.
