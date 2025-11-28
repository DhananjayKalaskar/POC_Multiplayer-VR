using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkImageSync : NetworkBehaviour
{
    [Header("Image Settings")]
    public Image targetImage;  // Assign the Image component in Inspector
    public Sprite[] sprites = new Sprite[5];  // Assign 5 sprites in Inspector

    // Network variable to sync current sprite index (-1 = no sprite/null)
    private NetworkVariable<int> currentSpriteIndex = new NetworkVariable<int>(
        -1,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Subscribe to changes
        currentSpriteIndex.OnValueChanged += OnSpriteIndexChanged;

        // Set initial sprite
        UpdateImageSprite(currentSpriteIndex.Value);
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        currentSpriteIndex.OnValueChanged -= OnSpriteIndexChanged;
    }

    // Called when sprite index changes on network
    private void OnSpriteIndexChanged(int oldIndex, int newIndex)
    {
        UpdateImageSprite(newIndex);
    }

    // Update the Image component with the sprite
    private void UpdateImageSprite(int index)
    {
        if (targetImage == null) return;

        if (index >= 0 && index < sprites.Length && sprites[index] != null)
        {
            targetImage.sprite = sprites[index];
            targetImage.enabled = true;  // Make sure image is visible
        }
        else
        {
            targetImage.sprite = null;
            targetImage.enabled = false;  // Hide image when null
        }
    }

    // Call this from "Next" button's OnClick event
    public void NextSprite()
    {
        if (IsServer)
        {
            NextSpriteServerRpc();
        }
        else
        {
            NextSpriteServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void NextSpriteServerRpc()
    {
        // Cycle through sprites (0, 1, 2, 3, 4, then back to 0)
        int nextIndex = (currentSpriteIndex.Value + 1) % sprites.Length;
        currentSpriteIndex.Value = nextIndex;
    }

    // Call this from "Reset" button's OnClick event
    public void ResetSprite()
    {
        if (IsServer)
        {
            ResetSpriteServerRpc();
        }
        else
        {
            ResetSpriteServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ResetSpriteServerRpc()
    {
        // Set to -1 to indicate no sprite (null)
        currentSpriteIndex.Value = -1;
    }
}
