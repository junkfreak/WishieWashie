using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.Mathematics;

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

    public float speed, rotSpeed, sprintSpeed, normSpeed, sensi, sensiX;
    public float inputYrot, upRange, downRange;
    public Camera cam;

    public CharacterController contrlr;
    public Vector3 playerVelo;
    public float gravity;
    public bool grounded, sprint;

    public float jumpHeight;
    private void Awake()
    {
        moveAct = pcntrl.FindActionMap("Player").FindAction("Move");
        rotateAct = pcntrl.FindActionMap("Player").FindAction("Look");
        sprintAct = pcntrl.FindActionMap("Player").FindAction("Sprint");



        moveAct.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAct.canceled += context => moveInput = Vector2.zero;

        rotateAct.performed += context => rotateInput = context.ReadValue<Vector2>();
        rotateAct.canceled += context => rotateInput = Vector2.zero;

        sprintAct.performed += context => sprint = true;
        sprintAct.canceled += context => sprint = false;




    }

    // Update is called once per frame
    void Update()
    {
        Bewegen();
        Draaien();
    }

    public void Bewegen()
    {
        Vector3 camF = cam.transform.forward;
        Vector3 camR = cam.transform.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        Vector3 forwardRelative = moveInput.y * camF;
        Vector3 rightRelative = moveInput.x * camR;



        Vector3 camRelativMove = Vector3.ClampMagnitude(forwardRelative + rightRelative, 1);
        Vector3 moveDir = camRelativMove * speed;


        contrlr.Move(moveDir * Time.deltaTime * speed);

        if (grounded == false)
        {
            playerVelo += Physics.gravity / 20;

            contrlr.Move(playerVelo * Time.deltaTime);
        }

    }

    public void Draaien()
    {
        if (rotateInput.sqrMagnitude > 0.1f)
        {
            float inputXrot = rotateInput.x * sensiX;
            transform.Rotate(0, inputXrot, 0);

             inputYrot -= rotateInput.y * sensi;
            inputYrot = Mathf.Clamp(inputYrot, downRange, upRange);
            cam.transform.localRotation = quaternion.Euler(inputYrot, 0, 0);
        }
    }
}
