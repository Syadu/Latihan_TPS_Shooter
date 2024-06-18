using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    public int damage = 10;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, life);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang ditabrak adalah musuh
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            // Dapatkan komponen EnemyHealth dari objek yang ditabrak
            Enemy enemy = other.GetComponent<Enemy>();
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            // Jika komponen EnemyHealth ditemukan, kurangi nyawa musuh dengan damage peluru
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            else if (player != null)
            {
                player.TakeDamage(damage);
            }

            // Hancurkan peluru setelah mengenai musuh
            Destroy(gameObject);
        }
    }
}
