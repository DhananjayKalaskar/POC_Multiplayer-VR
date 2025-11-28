using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetPokeToFingerAttachPoint : MonoBehaviour
{
    public Transform PokeAttachedPoint;
    private XRPokeInteractor _xrPokeInteractor;
    // Start is called before the first frame update
    void Start()
    {
        _xrPokeInteractor = transform.parent.parent.GetComponentInChildren<XRPokeInteractor>();
        SetPokeAttachedPoint();
    }

    private void SetPokeAttachedPoint()
    {
        if(PokeAttachedPoint == null) { Debug.Log("Poke Attached Point is null"); return; }
        if(_xrPokeInteractor == null) { Debug.Log("XR Poke Interactor is null"); return;}
        _xrPokeInteractor.attachTransform = PokeAttachedPoint;
    }
}
