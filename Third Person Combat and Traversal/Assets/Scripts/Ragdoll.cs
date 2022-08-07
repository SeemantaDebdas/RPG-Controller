using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CharacterController controller;

    Collider[] colliders;
    Rigidbody[] rigidbodies;

    private void Start()
    {
        colliders = GetComponentsInChildren<Collider>(true);
        rigidbodies= GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Ragdoll"))
                collider.enabled = isRagdoll;
        }

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody.CompareTag("Ragdoll")) 
            { 
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }

        anim.enabled = !isRagdoll;
        controller.enabled = !isRagdoll;
    }
}
