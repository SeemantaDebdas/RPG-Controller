using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float smoothDragTime = 0.3f;
    float velocityY = 0;

    Vector3 impactForce = Vector3.zero;
    public Vector3 Movement => impactForce + Vector3.up * velocityY;


    Vector3 currentVelocityRef;

    private void Update()
    {
        if (velocityY < 0f && controller.isGrounded)
        {
            velocityY = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            velocityY += Physics.gravity.y * Time.deltaTime;
        }

        impactForce = Vector3.SmoothDamp(impactForce, Vector3.zero, ref currentVelocityRef, smoothDragTime * Time.deltaTime);
    }

    public void AddForce(Vector3 force)
    {
        impactForce += force;
    }
}
