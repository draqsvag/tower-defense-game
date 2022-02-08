using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform barrel;
    [SerializeField] private float cooldown;
    float cooldownElapsed;
    [SerializeField] private Image cooldownBar;

    public List<Collider2D> detectedEnemies = new List<Collider2D>();

    Enemy currentTarget;

    private bool inCooldown;
    private bool isEnemyDetected;

    void Start()
    {

    }

    void Update()
    {
        isEnemyDetected = (detectedEnemies.Count > 0) ? true : false;

        if (isEnemyDetected && detectedEnemies[0] != null)
        {
            Physics2D.Raycast(transform.position, detectedEnemies[0].transform.position - transform.position);

            LookEnemy();

            if (inCooldown == false)
                StartCoroutine(FireAndWait());
        }
    }

    private void LookEnemy()
    {
        Vector2 direction = (detectedEnemies[0].transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50f);
    }

    private IEnumerator FireAndWait()
    {
        inCooldown = true;

        Fire();

        //Wait
        while (cooldownElapsed < cooldown)
        {
            cooldownElapsed += Time.deltaTime;
            cooldownBar.fillAmount = cooldownElapsed / cooldown;
            yield return null;
        }

        inCooldown = false;
    }

    private void Fire()
    {
        if (isEnemyDetected)
        {
            Projectile lastProjectile = Instantiate(projectile, barrel.position, transform.rotation).GetComponent<Projectile>();
            lastProjectile.target = detectedEnemies[0].transform;

            cooldownElapsed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            detectedEnemies.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            detectedEnemies.Remove(collision);
        }
    }
}
