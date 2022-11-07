using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlaceFeetOnGround : MonoBehaviour
{
    [SerializeField] Vector3 feetPositionOffset;
    [SerializeField] LayerMask steppableMask;

    [SerializeField] float verticalRaycastOffset = default;
    
    private Animator _animator = null;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Vector3 leftFootPosition = _animator.GetIKPosition(AvatarIKGoal.LeftFoot); 
        Vector3 rightFootPosition = _animator.GetIKPosition(AvatarIKGoal.RightFoot);

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

        _animator.SetIKPosition(AvatarIKGoal.LeftFoot, FootRaycastPosition(leftFootPosition));
        _animator.SetIKPosition(AvatarIKGoal.RightFoot, FootRaycastPosition(rightFootPosition));
        _animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.Euler(transform.forward));
        _animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.Euler(transform.forward));
    }

    private Vector3 FootRaycastPosition(Vector3 oldFootPosition)
    {
        oldFootPosition.y += verticalRaycastOffset;
        
        RaycastHit raycastHit;
        Physics.Raycast(oldFootPosition, Vector3.down, out raycastHit, Mathf.Infinity, steppableMask);

        Vector3 newFootPositon = raycastHit.point + feetPositionOffset;
        return newFootPositon;
    }
}
