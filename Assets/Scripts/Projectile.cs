using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public Transform target;
    public int damage;

    private void Update()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * projectileSpeed);

        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * projectileSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
