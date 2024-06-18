using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
    private void Start()
    {
        // Menyembunyikan pointer saat game dimulai
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Menyembunyikan pointer saat game berjalan
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
