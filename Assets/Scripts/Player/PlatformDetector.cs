using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    [Header("\uD83C\uDFAE Mobile Controls")]
    public GameObject joystick;
    public GameObject dashButton;
    public GameObject attackButton;

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval(@"
            (function() {
                var isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
                var deviceType = isMobile ? 'mobile' : 'desktop';
                SendMessage('PlatformDetector', 'OnDeviceTypeReceived', deviceType);
            })();
        ");
#endif
    }

    public void OnDeviceTypeReceived(string deviceType)
    {

        if (deviceType == "desktop")
        {
            if (joystick) Destroy(joystick);
            if (dashButton) Destroy(dashButton);
            if (attackButton) Destroy(attackButton);
        }
    }
}
