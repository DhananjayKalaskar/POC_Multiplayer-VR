using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public Renderer[] meshToDisable;
    // Start is called before the first frame update

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            foreach (var item in meshToDisable)
            {
                item.enabled = false;
            }
        }

    }
  
    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {

            root.position = VRRigReferance.Singleton.root.position;
            root.rotation = VRRigReferance.Singleton.root.rotation;

            head.position = VRRigReferance.Singleton.head.position;
            head.rotation = VRRigReferance.Singleton.head.rotation;

            leftHand.position = VRRigReferance.Singleton.leftHand.position;
            leftHand.rotation = VRRigReferance.Singleton.leftHand.rotation;

            rightHand.position = VRRigReferance.Singleton.rightHand.position;
            rightHand.rotation = VRRigReferance.Singleton.rightHand.rotation;
        }
    }
}
