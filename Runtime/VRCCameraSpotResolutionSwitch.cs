using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace PuetsuaWorkshop.VRCCameraSpot
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class VRCCameraSpotResolutionSwitch : UdonSharpBehaviour
    {
        [SerializeField] private VRCCameraSpot cameraSpot;

        [Header("Resolutions (same order)")]
        [SerializeField] private RenderTexture[] resolutionTextures;
        [SerializeField] private Texture[] resolutionIcons;
        [SerializeField] private RawImage resolutionIconImage;
        [SerializeField] private int startIndex;

        private int _index;

        private void Start()
        {
            _index = startIndex;
            ApplyResolution();
        }

        public override void Interact()
        {
            if (resolutionTextures == null || resolutionTextures.Length == 0)
            {
                return;
            }

            _index = (_index + 1) % resolutionTextures.Length;
            ApplyResolution();
        }

        private void ApplyResolution()
        {
            if (resolutionTextures == null || _index < 0 || _index >= resolutionTextures.Length)
            {
                return;
            }

            if (Utilities.IsValid(cameraSpot))
            {
                cameraSpot.SetSourceTexture(resolutionTextures[_index]);
            }

            if (Utilities.IsValid(resolutionIconImage) && resolutionIcons != null && _index < resolutionIcons.Length)
            {
                resolutionIconImage.texture = resolutionIcons[_index];
            }
        }
    }
}
