# VRC Camera Spot

Simple VRChat world camera spot prefab.

## Installation

1. Clone or download this repository.
2. Put the `VRCCameraSpot` folder under your Unity project's `Assets` folder.
3. Open the project in Unity and wait for scripts/materials to import.
4. Drag one prefab into your scene:
   - `Prefabs/VRCCameraSpot.prefab` for QHD.
   - `Prefabs/VRCCameraSpot_4K.prefab` for 4K.
   - `Prefabs/VRCCameraSpot_8K.prefab` for 8K.

## Setup

1. Move `SourceCamera_MoveMe` to the view you want to show.
2. Move `SpotOverlayQuad_Internal` and `VisibleGuideSphere` to the place where players should use their VRChat camera.
3. Players move their own camera into the sphere. In VR, this means the VRChat handheld camera. On desktop, this means the player's view camera.

## Notes

- Duplicate `Materials/VRCCameraSpotOverlay.mat` and `RenderTextures/VRCCameraSpot.renderTexture` for each independent camera spot.
- The default render texture is QHD 2560x1440 with 4x MSAA to reduce jagged edges.
- For 4K output, assign `RenderTextures/VRCCameraSpot_4K.renderTexture` to both `SourceCamera_MoveMe` and the `VRCCameraSpot` script's `Source Texture` field.
- For 8K output, use `RenderTextures/VRCCameraSpot_8K.renderTexture` the same way.
- The 4K and 8K presets use 2x MSAA.
- The overlay fills the player's view and crops to fit, so it does not show black bars.
- The sphere is only a camera spot marker. It is not clickable.
- Each prefab includes a camera icon canvas inside the spot marker.
- This project is supported with Codex GPT-5.5.
- MIT licensed. Based on `CameraSystem` by Ottpossum / rhaamo; see `THIRD_PARTY_NOTICES.md`.
