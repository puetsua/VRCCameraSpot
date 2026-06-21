# VRC Camera Spot

Minimal camera spot system for VRChat worlds.

It uses the same basic trick as `Ottpossum/Camera System/Prefabs/EventCameraSystem.prefab`, but with one camera feed and no camera selector UI:

1. A Unity `Camera` renders into `RenderTextures/VRCCameraSpot.renderTexture`.
2. `Materials/VRCCameraSpotOverlay.mat` displays that render texture.
3. A small mesh in the world uses that material.
4. `Shaders/VRCCameraSpotOverlay.shader` draws that mesh as a fullscreen overlay for cameras that see the spot.
5. `Scripts/VRCCameraSpot.cs` wires the camera texture to the material and toggles `_ForceSpot` for desktop users.

## Scene Setup

Create this hierarchy in your scene:

```text
VRCCameraSpot
  SourceCamera
  SpotMesh
```

Recommended setup:

- Add `VRCCameraSpot.cs` to the root object.
- Add a normal Unity `Camera` as `SourceCamera`.
- Assign `RenderTextures/VRCCameraSpot.renderTexture` to the script's `Source Texture`.
- Assign `SourceCamera` to the script's `Source Camera`.
- Add a small quad, plane, or sphere as `SpotMesh`.
- Assign `Materials/VRCCameraSpotOverlay.mat` to the spot mesh renderer.
- Assign the same material to the script's `Spot Material`.
- Assign the spot mesh renderer to the script's `Spot Renderer`.

For VR usage, place the player's VRChat handheld camera inside or close to the spot mesh.

For desktop usage, look at the spot and press `F10`, or interact with the object if its collider/interact settings are enabled. This sets `_ForceSpot` on the material, bypassing distance/FOV checks.

## Material Notes

Duplicate `VRCCameraSpotOverlay.mat` if you make multiple independent camera spots. `_ForceSpot` and `_MainTex` live on the material, so sharing one material means all spots share the same state and feed.

The default render texture is 1280x720. Increase it if the feed needs to be sharper, or lower it if the world needs to be cheaper.
