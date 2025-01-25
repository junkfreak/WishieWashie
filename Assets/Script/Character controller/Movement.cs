using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public PlayerInput pInput;
    public InputActionAsset pcntrl;

    private InputAction moveAct;
    private InputAction rotateAct;
    private InputAction sprintAct;
    private InputAction jumpAct;

    private Vector2 moveInput;
    private Vector2 rotateInput;

    public float speed, rotSpeed, sprintSpeed, normSpeed;
    public Camera cam;

    public CharacterController contrlr;
    public Vector3 playerVelo;
    public float gravity;
    public bool grounded, sprint;

    public float jumpHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
