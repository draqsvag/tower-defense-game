using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool isTarget;
    public Image debug;
    public Transform healthBar;
    [SerializeField] private float maxHealth;
    public float curHealth;

    void Start()
    {
        StartCoroutine(PathManager.Instance.FollowPath(transform.parent, speed));
        curHealth = maxHealth;

        List<Sprite> images = new List<Sprite>();

        Sprite myData = Resources.Load("Square") as Sprite;
        images.Add(myData);
    }

    private void Update()
    {
        healthBar.parent.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void Damage(int amount)
    {
        curHealth -= amount;

        if (curHealth <= 0)
        {
            curHealth = 0;
            UpdateHealthBar();
            Destroy(transform.parent.gameObject);
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        Vector3 barScale = healthBar.localScale;
        barScale.x = curHealth / maxHealth;

        healthBar.transform.localScale = barScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            int damageAmount = projectile.damage;
            Damage(damageAmount);
        }
    }
}
