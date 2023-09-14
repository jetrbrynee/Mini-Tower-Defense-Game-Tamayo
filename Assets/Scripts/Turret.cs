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
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= range)
            {
                enemiesInRange.Add(enemy);
            }
        }

        Debug.Log("Number of enemies in range: " + enemiesInRange.Count);
    }

    private void ChooseTarget()
    {
        currentTarget = SelectTarget(enemiesInRange, transform);

        if (currentTarget != null)
        {
            Debug.Log("Current target: " + currentTarget.name);
        }
        else
        {
            Debug.Log("No enemies in range.");
        }
    }

    private void AimAtTarget()
    {
        if (currentTarget == null) return;

        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnRate * Time.deltaTime);

        Debug.Log("Aiming at target: " + currentTarget.name);
    }

    private void Fire()
    {
        if (currentTarget == null) return;

        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0)
        {
            Debug.Log("Firing at target: " + currentTarget.name);

            // Reset fire cooldown
            fireCooldown = 1 / fireRate;
        }
    }

    private GameObject SelectTarget(List<GameObject> enemiesInRange, Transform turretTransform)
    {
        // Add your target selection logic here
        GameObject currentTarget = null;

        // Implement your custom target selection logic here
        // For example, prioritize the closest enemy as the current target

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
