using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Transform bulletSpawn;
    public Slider healthSlider;

    public float moveSpeed = 3f;
    private Transform target;
    private bool isHurt;
    private float hurtTimer;

    private Renderer enemyRenderer;
    private Color originalColor;
    public Color hurtColor = Color.red;

    private Score scoreManager;

    public GameObject bulletPrefab; // Prefab peluru
    public float attackRange = 10f; // Jarak serangan
    public float attackRate = 1f; // Kecepatan serangan (dalam detik)
    private float nextAttackTime;


    private void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;

        scoreManager = FindObjectOfType<Score>();

        nextAttackTime = Time.time;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    private void Update()
    {
        if (!isHurt)
        {
            AttackPlayer();
        }
        else
        {
            HurtReaction();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            isHurt = true;
            hurtTimer = 0.5f; // Waktu reaksi terkena damage (dalam detik)
            enemyRenderer.material.color = hurtColor; // Mengubah warna musuh saat terkena damage
        }
    }

    private void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= attackRange)
            {
                // Menembak peluru ke arah pemain
                ShootBullet();
                nextAttackTime = Time.time + 1f / attackRate; // Mengatur waktu serangan berikutnya
            }
        }
    }

    private void ShootBullet()
    {
        // Membuat objek peluru dari prefab
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Debug.Log("Enemy menembakkan peluru!");

        // Mengarahkan peluru ke arah pemain
        bullet.GetComponent<Rigidbody>().velocity = (target.position - bulletSpawn.position).normalized * 10f;
    }
    private void HurtReaction()
    {
        hurtTimer -= Time.deltaTime;

        if (hurtTimer <= 0)
        {
            isHurt = false;
            hurtTimer = 0;
            enemyRenderer.material.color = originalColor; // Mengembalikan warna musuh ke warna asli
        }
    }

    private void Die()
    {
        if (scoreManager != null)
        {
            if (currentHealth <= 0)
            {
                scoreManager.AddScore();
            }
        }

        Destroy(gameObject);
    }
}
