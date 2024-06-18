using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public ParticleSystem bulletParticleSystem;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            bulletParticleSystem.Play();

            animator.SetTrigger("Shoot");
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger("NotShoot");
        }
    }
}
