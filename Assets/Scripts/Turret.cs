using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 5.0f;
    public float fireRate = 1.0f;
    public float turnRate = 5.0f;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private GameObject currentTarget;
    private float fireCooldown;

    private Transform turretRotationPoint;

    private void Start()
    {
        // Assuming you have a child object named "RotatePoint" for turret rotation.
        turretRotationPoint = transform.Find("RotatePoint");
    }

    private void Update()
    {
        UpdateEnemiesInRange();
        ChooseTarget();
        AimAtTarget();
        Fire();
    }

    private void UpdateEnemiesInRange()
    {
        enemiesInRange.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(turretRotationPoint.position, enemy.transform.position);

            if (distance <= range)
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void ChooseTarget()
    {
        currentTarget = SelectTarget(enemiesInRange, turretRotationPoint);
    }

    private void AimAtTarget()
    {
        if (currentTarget == null) return;

        Vector3 targetDirection = currentTarget.transform.position - turretRotationPoint.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        turretRotationPoint.rotation = Quaternion.Slerp(turretRotationPoint.rotation, targetRotation, turnRate * Time.deltaTime);
    }

    private void Fire()
    {
        if (currentTarget == null) return;

        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0)
        {
            // Perform your firing logic here.
            // For example, instantiate and fire a projectile at the currentTarget.

            // Reset the cooldown.
            fireCooldown = 1 / fireRate;
        }
    }

    private GameObject SelectTarget(List<GameObject> enemiesInRange, Transform turretTransform)
    {
        GameObject currentTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(turretTransform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentTarget = enemy;
            }
        }

        return currentTarget;
    }
}
