using UnityEngine;
using Unity.Services.Vivox;
using Unity.Services.Core;

public class VivoxInitializer : MonoBehaviour
{
    async void Start()
    {
        // Initialize Unity Services
        await UnityServices.InitializeAsync();

        // Initialize Vivox
        await VivoxService.Instance.InitializeAsync();

        Debug.Log("Vivox initialized");
    }

    void OnDestroy()
    {
        // Vivox service cleanup is handled automatically
        // Individual players should logout in their NetworkVoiceChat script
        // No explicit uninitialize needed
    }
}
