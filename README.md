# VRC Camera Spot

Minimal camera spot system for VRChat worlds.

It is a simplified camera spot package based on the MIT-licensed `rhaamo/CameraSystem` project, especially its `EventCameraSystem`/`CameraJackShader` approach, but with one camera feed and no camera selector UI:

1. A Unity `Camera` renders into `RenderTextures/VRCCameraSpot.renderTexture`.
2. `Materials/VRCCameraSpotOverlay.mat` displays that render texture.
3. A small mesh in the world uses that material.
4. `Shaders/VRCCameraSpotOverlay.shader` draws that mesh as a fullscreen overlay for cameras that see the spot.
5. `Scripts/VRCCameraSpot.cs` wires the camera texture to the material and toggles `_ForceSpot` for desktop users.

## Prefab Setup

Drag `Prefabs/VRCCameraSpot.prefab` into the scene.

The prefab contains:

- `VRCCameraSpot`: root object with the UdonSharp behavior and an interaction collider.
- `SpotSphere_PlayerCameraHere`: the overlay sphere that a player camera should look at or sit inside.
- `VisibleGuideSphere`: a transparent visible marker showing where the spot is.
- `SourceCamera_MoveMe`: the camera that renders the target view into the render texture.

Move `SourceCamera_MoveMe` to the world position you want to broadcast. Move `SpotSphere_PlayerCameraHere` and `VisibleGuideSphere` together to the place where players should use their VRChat camera.

For desktop usage, look at the spot and press `F10`, or interact with the root object if interaction is enabled. This sets `_ForceSpot` on the material, bypassing distance/FOV checks.

## Manual Scene Setup

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
- Add a second guide mesh with `Materials/SpotSphereTransparent.mat` if players need a visible marker.
- Assign the same material to the script's `Spot Material`.
- Assign the spot mesh renderer to the script's `Spot Renderer`.

For VR usage, place the player's VRChat handheld camera inside or close to the spot mesh.

For desktop usage, look at the spot and press `F10`, or interact with the object if its collider/interact settings are enabled. This sets `_ForceSpot` on the material, bypassing distance/FOV checks.

## Material Notes

Duplicate `VRCCameraSpotOverlay.mat` if you make multiple independent camera spots. `_ForceSpot` and `_MainTex` live on the material, so sharing one material means all spots share the same state and feed.

The default render texture is 1280x720. Increase it if the feed needs to be sharper, or lower it if the world needs to be cheaper.

## Credits And License

This project is MIT licensed.

It is based on `CameraSystem` by Ottpossum / rhaamo:

- https://github.com/rhaamo/CameraSystem
- MIT License, Copyright (c) 2024 Ottpossum

The fullscreen camera overlay shader is derived from `CameraJackShader.shader`, which credits kurotori and was adapted by ottpossum. See `THIRD_PARTY_NOTICES.md` for the preserved upstream MIT notice.
