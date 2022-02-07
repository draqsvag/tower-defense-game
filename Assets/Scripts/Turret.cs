using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float attackRadius;
    Collider2D[] detectedEnemies;
    bool isEnemyDetected;


    void Start()
    {

    }

    void Update()
    {
        detectedEnemies = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        isEnemyDetected = (detectedEnemies.Length > 0) ? true : false;

        if (isEnemyDetected)
        {
            LookEnemy();
        }
    }

    private void LookEnemy()
    {
        Vector2 direction = (transform.position - detectedEnemies[0].transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
