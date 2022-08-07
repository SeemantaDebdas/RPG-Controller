using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using System;

public class Targeter : MonoBehaviour
{
    [SerializeField] CinemachineTargetGroup cineTargetGroup = null;

    List<Target> targets = new();
    public Target CurrentTarget { get; private set; }

    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Target target))
        {
            targets.Add(target);
            target.OnTargetDestroyed += RemoveTarget;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Target target))
        {
            targets.Remove(target);
            RemoveTarget(target);
        }
    }

    public bool SetTarget()
    {
        if (targets.Count == 0) return false;

        Target closestTargetToScreen = ClosestTargetToScreenCenter();

        if (closestTargetToScreen == null) return false;

        CurrentTarget = closestTargetToScreen;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1, 2);
        return true;
    }

    private Target ClosestTargetToScreenCenter()
    {
        Target closestTargetToScreen = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            Vector2 viewportPoint = mainCam.WorldToViewportPoint(target.transform.position);
            if (!target.GetComponentInChildren<Renderer>().isVisible)
                continue;

            Vector2 distanceToCenter = viewportPoint - new Vector2(0.5f, 0.5f);
            if (distanceToCenter.magnitude < closestTargetDistance)
            {
                closestTargetToScreen = target;
                closestTargetDistance = distanceToCenter.magnitude;
            }
        }

        return closestTargetToScreen;
    }

    public void Cancel()
    {
        if (CurrentTarget == null) return;

        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }
    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnTargetDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
