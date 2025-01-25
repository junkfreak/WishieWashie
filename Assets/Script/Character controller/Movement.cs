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

    public float speed, rotSpeed, sensi, sensiX, radius, jumpForce;
    public float inputYrot, upRange, downRange;
    public Camera cam;

    public CharacterController contrlr;
    public Vector3 playerVelo;
    public float gravity;
    public bool grounded, sprint, jump;

    public float jumpHeight;

    public LayerMask lMask;
    private void Awake()
    {
        moveAct = pcntrl.FindActionMap("Player").FindAction("Move");
        rotateAct = pcntrl.FindActionMap("Player").FindAction("Look");
        sprintAct = pcntrl.FindActionMap("Player").FindAction("Sprint");
        jumpAct = pcntrl.FindActionMap("Player").FindAction("Jump");


        moveAct.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAct.canceled += context => moveInput = Vector2.zero;

        rotateAct.performed += context => rotateInput = context.ReadValue<Vector2>();
        rotateAct.canceled += context => rotateInput = Vector2.zero;

        sprintAct.performed += context => sprint = true;
        sprintAct.canceled += context => sprint = false;

        jumpAct.performed += context => jump = true;
        jumpAct.canceled += context => jump = false;




    }

    // Update is called once per frame
    void Update()
    {
        //playerVelo.y += gravity * Time.deltaTime;

        Bewegen();
        Draaien();
        GrondChecker();
        Springen();

       
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
            playerVelo += Physics.gravity /20;

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

    public void GrondChecker()
    {
        grounded = Physics.CheckSphere(transform.position, radius, lMask);
        if (grounded && playerVelo.y < 0)
        {
            playerVelo.y = -2;
            contrlr.Move(playerVelo * Time.deltaTime);
        }
    }

    public void Springen()
    {
        if ( jump && grounded)
        {
            Debug.Log("spring");
            playerVelo.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            contrlr.Move(playerVelo * Time.deltaTime);
        }
    }
}
