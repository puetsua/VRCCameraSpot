using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace PuetsuaWorkshop.VRCCameraSpot
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class VRCCameraSpot : UdonSharpBehaviour
    {
        [Header("Camera Feed")]
        [SerializeField] private Camera sourceCamera;
        [SerializeField] private RenderTexture sourceTexture;

        [Header("Overlay")]
        [SerializeField] private Material spotMaterial;
        [SerializeField] private Renderer spotRenderer;

        private Material _overlayMaterial;

        private void Start()
        {
            ApplyCameraFeed();
        }

        public void RefreshCameraFeed()
        {
            ApplyCameraFeed();
        }

        public void SetSourceTexture(RenderTexture texture)
        {
            if (!Utilities.IsValid(texture) || texture == sourceTexture)
            {
                return;
            }

            RenderTexture previous = sourceTexture;
            sourceTexture = texture;
            ApplyCameraFeed();

            // Free the old resolution's GPU memory; 8K is large.
            if (Utilities.IsValid(previous))
            {
                previous.Release();
            }
        }

        private void ApplyCameraFeed()
        {
            if (Utilities.IsValid(sourceCamera) && Utilities.IsValid(sourceTexture))
            {
                sourceCamera.targetTexture = sourceTexture;
            }

            Material material = GetOverlayMaterial();
            if (Utilities.IsValid(material) && Utilities.IsValid(sourceTexture))
            {
                material.SetTexture("_MainTex", sourceTexture);
            }

            if (Utilities.IsValid(spotRenderer))
            {
                spotRenderer.enabled = true;
            }
        }

        // Per-spot material copy, so spots don't overwrite each other's texture
        // or modify the shared material asset in the editor.
        private Material GetOverlayMaterial()
        {
            if (Utilities.IsValid(_overlayMaterial))
            {
                return _overlayMaterial;
            }

            if (!Utilities.IsValid(spotRenderer))
            {
                return null;
            }

            if (Utilities.IsValid(spotMaterial))
            {
                spotRenderer.sharedMaterial = spotMaterial;
            }

            _overlayMaterial = spotRenderer.material;
            return _overlayMaterial;
        }
    }
}
