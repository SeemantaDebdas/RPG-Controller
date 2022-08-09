using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3> OnLedgeDetection;

    private void OnTriggerEnter(Collider other)
    { 
        OnLedgeDetection?.Invoke(other.transform.forward);
    }
}
