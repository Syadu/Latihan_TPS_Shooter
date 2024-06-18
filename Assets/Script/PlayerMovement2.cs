using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6.0f;
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
    }
}
