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

        [Header("Desktop Control")]
        [SerializeField] private bool allowDesktopToggle = true;
        [SerializeField] private KeyCode desktopToggleKey = KeyCode.F10;
        [SerializeField] private bool forceDesktopOnStart;

        private bool forceDesktop;

        private void Start()
        {
            forceDesktop = forceDesktopOnStart;
            ApplyCameraFeed();
            ApplyForceState();
        }

        private void Update()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (!Utilities.IsValid(localPlayer)) { return; }
            if (localPlayer.IsUserInVR()) { return; }
            if (!allowDesktopToggle) { return; }

            if (Input.GetKeyDown(desktopToggleKey))
            {
                ToggleDesktopForce();
            }
        }

        public override void Interact()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (!Utilities.IsValid(localPlayer)) { return; }
            if (localPlayer.IsUserInVR()) { return; }

            ToggleDesktopForce();
        }

        public void ToggleDesktopForce()
        {
            forceDesktop = !forceDesktop;
            ApplyForceState();
        }

        public void ForceDesktopOn()
        {
            forceDesktop = true;
            ApplyForceState();
        }

        public void ForceDesktopOff()
        {
            forceDesktop = false;
            ApplyForceState();
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
        }

        private void ApplyForceState()
        {
            if (Utilities.IsValid(spotMaterial))
            {
                spotMaterial.SetFloat("_ForceSpot", forceDesktop ? 1.0f : 0.0f);
            }

            if (Utilities.IsValid(spotRenderer))
            {
                spotRenderer.enabled = true;
            }
        }
    }
}
