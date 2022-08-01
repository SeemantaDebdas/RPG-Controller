using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderAnimationHandler : MonoBehaviour
{
    [SerializeField] GameObject weaponCollider;

    void EnableCollider() => weaponCollider.SetActive(true);
    void DisableCollider() => weaponCollider.SetActive(false);
}
