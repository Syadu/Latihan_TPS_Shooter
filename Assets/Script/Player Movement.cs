using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 10.0f;
    private float jumpHeight = 1f;
    private float gravityValue = -9.81f;

    public int maxHealth = 100;
    private int currentHealth;
    private bool isHurt;
    private float hurtTimer;

    private Renderer playerRenderer;
    private Color originalColor;
    public Color hurtColor = Color.red;

    public Transform cameraTransform; // Referensi ke objek kamera
    public Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;
        controller = gameObject.AddComponent<CharacterController>();

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Mendapatkan input gerakan horizontal dan vertikal
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Menggunakan arah kamera untuk menentukan arah gerakan
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = (cameraForward * vertical) + (cameraTransform.right * horizontal);

        controller.Move(movement * Time.deltaTime * playerSpeed);

        // Menghadapkan pemain ke arah gerakan
        if (movement != Vector3.zero)
        {
            gameObject.transform.forward = movement;
        }

        // Changes the height position of the player..
        if (Input.GetKeyDown("space") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (!isHurt)
        {
            
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
            playerRenderer.material.color = hurtColor; // Mengubah warna musuh saat terkena damage
        }
    }

    private void HurtReaction()
    {
        hurtTimer -= Time.deltaTime;

        if (hurtTimer <= 0)
        {
            isHurt = false;
            hurtTimer = 0;
            playerRenderer.material.color = originalColor; // Mengembalikan warna musuh ke warna asli
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
