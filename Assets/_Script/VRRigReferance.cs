using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigReferance : MonoBehaviour
{
    public static VRRigReferance Singleton;
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private void Awake()
    {
        Singleton = this;

    }
}
