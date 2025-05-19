using System.Runtime.InteropServices;
using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    [Header("🎮 Mobile Controls")]
    public GameObject joystick;
    public GameObject dashButton;
    public GameObject attackButton;
    public GameObject ButtonE;

#if UNITY_WEBGL && !UNITY_EDITOR
    //[DllImport("__Internal")]
    //private static extern void DetectDeviceType();
#endif

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        //DetectDeviceType();
#endif
    }

    public void OnDeviceTypeReceived(string deviceType)
    {
        Debug.Log("Device Type: " + deviceType);

        if (deviceType == "desktop")
        {
            if (joystick) Destroy(joystick);
            if (dashButton) Destroy(dashButton);
            if (attackButton) Destroy(attackButton);
            if (ButtonE) Destroy(ButtonE);
        }
    }
}
