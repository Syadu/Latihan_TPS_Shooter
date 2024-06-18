using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Target yang akan diikuti kamera
    public float distance = 5.0f; // Jarak kamera dari target
    public float height = 2.0f; // Tinggi kamera dari target
    public float smoothing = 5.0f; // Kelembutan gerakan kamera
    public float mouseXSensitivity = 3.0f; // Sensitivitas mouse sumbu X
    public float mouseYSensitivity = 3.0f; // Sensitivitas mouse sumbu Y

    private float mouseX, mouseY; // Variabel untuk menyimpan posisi mouse
    private Vector3 offset; // Offset posisi kamera dari target

    void Start()
    {
        // Hitung offset awal
        offset = new Vector3(0.0f, height, -distance);
    }

    void LateUpdate()
    {
        // Mendapatkan input mouse
        mouseX += Input.GetAxis("Mouse X") * mouseXSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * mouseYSensitivity;
        mouseY = Mathf.Clamp(mouseY, -90.0f, 90.0f); // Membatasi rotasi vertikal

        // Hitung posisi kamera berdasarkan mouse
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);
        Vector3 newPosition = target.position - (rotation * offset);

        // Atur posisi kamera dengan pergerakan yang halus
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothing * Time.deltaTime);

        // Menghadapkan kamera ke target
        transform.LookAt(target);
    }
}
