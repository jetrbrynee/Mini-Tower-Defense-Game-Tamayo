using UnityEngine;

public static class RotationController
{
    public static void RotateTowardsTarget(Transform turretTransform, Transform targetTransform, float turnRate)
    {
        // Add your rotation logic here to rotate the turret towards the target
        Vector3 targetDirection = targetTransform.position - turretTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, targetRotation, turnRate * Time.deltaTime);

        Debug.Log("Turret is rotating towards the target.");
    }
}
