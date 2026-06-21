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

        private void Start()
        {
            ApplyCameraFeed();
        }

        public void RefreshCameraFeed()
        {
            ApplyCameraFeed();
        }

        private void ApplyCameraFeed()
        {
            if (Utilities.IsValid(sourceCamera) && Utilities.IsValid(sourceTexture))
            {
                sourceCamera.targetTexture = sourceTexture;
            }

            if (Utilities.IsValid(spotMaterial) && Utilities.IsValid(sourceTexture))
            {
                spotMaterial.SetTexture("_MainTex", sourceTexture);
            }

            if (Utilities.IsValid(spotRenderer))
            {
                spotRenderer.enabled = true;
            }
        }
    }
}
