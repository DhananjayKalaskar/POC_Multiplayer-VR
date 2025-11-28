using UnityEngine;
using Unity.Netcode;
using Unity.Services.Vivox;
using System.Threading.Tasks;
public class NetworkVoiceChat : NetworkBehaviour
{
    private string channelName = "GameChannel"; // Shared channel name

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            // Only the owner initializes voice chat
            InitializeVoiceChat();
        }
    }

    private async void InitializeVoiceChat()
    {
        try
        {
            // Login to Vivox
            await LoginToVivox();

            // Join the voice channel (2D non-positional)
            await JoinChannel();

            // Set transmission mode to All (so you can hear everyone)
            await VivoxService.Instance.SetChannelTransmissionModeAsync(TransmissionMode.All, channelName);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Voice chat initialization failed: {e.Message}");
        }
    }

    private async Task LoginToVivox()
    {
        // Get unique player ID from network
        string playerId = NetworkManager.Singleton.LocalClientId.ToString();

        // Login to Vivox service
        LoginOptions loginOptions = new LoginOptions
        {
            DisplayName = $"Player_{playerId}",
            ParticipantUpdateFrequency = ParticipantPropertyUpdateFrequency.FivePerSecond
        };

        await VivoxService.Instance.LoginAsync(loginOptions);
        Debug.Log("Logged in to Vivox");
    }

    private async Task JoinChannel()
    {
        // Create channel options - make it the active channel
        ChannelOptions channelOptions = new ChannelOptions
        {
            MakeActiveChannelUponJoining = true
        };

        // Join the 2D group channel (non-positional - everyone hears everyone)
        await VivoxService.Instance.JoinGroupChannelAsync(
            channelName,
            ChatCapability.AudioOnly, // Audio only, no text
            channelOptions
        );

        Debug.Log($"Joined voice channel: {channelName}");
    }

    public override void OnNetworkDespawn()
    {
        if (IsOwner)
        {
            LeaveChannel();
        }
        base.OnNetworkDespawn();
    }

    private async void LeaveChannel()
    {
        try
        {
            await VivoxService.Instance.LeaveChannelAsync(channelName);
            await VivoxService.Instance.LogoutAsync();
            Debug.Log("Left voice channel and logged out");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error leaving channel: {e.Message}");
        }
    }
}
