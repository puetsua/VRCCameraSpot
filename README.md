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
3. Players move their own camera into the sphere. In VR, this means the VRChat handheld camera. On desktop, this means the player's view camera.

## Notes

- Duplicate `Materials/VRCCameraSpotOverlay.mat` and `RenderTextures/VRCCameraSpot.renderTexture` for each independent camera spot.
- The default render texture is 1280x720.
- For 8K output, assign `RenderTextures/VRCCameraSpot_8K.renderTexture` to both `SourceCamera_MoveMe` and the `VRCCameraSpot` script's `Source Texture` field.
- The sphere is only a camera spot marker. It is not clickable.
- This project is supported with Codex GPT-5.5.
- MIT licensed. Based on `CameraSystem` by Ottpossum / rhaamo; see `THIRD_PARTY_NOTICES.md`.
