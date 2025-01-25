using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CleanScript : MonoBehaviour
{

    public Camera _camera;
    [SerializeField] private Texture _dirtMaskBase;
    [SerializeField] private Texture2D _brush;

    public Material _material;
    public GameObject reus;
    private InputAction ShootAct;

    public bool shoot;
    public PlayerInput pInput;
    public InputActionAsset pcntrl;
    public float rayDist;
    public void Awake()
    {
        ShootAct = pcntrl.FindActionMap("Player").FindAction("Attack");


        ShootAct.performed += context => shoot = true;
        ShootAct.canceled += context => shoot = false;
    }
    private void Start()
    {
        _material = reus.GetComponent<Material>();
        _dirtMaskBase = _material.GetTexture("_greenmask");
        //_dirtMaskBase = _material.
        //CreateTexture();
    }

    private void Update()
    {
        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward, Color.red, rayDist);
        if (shoot)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, rayDist))
            {
                Vector2 textureCoord = hit.textureCoord;

                int pixlX = (int)(textureCoord.x * _dirtMaskBase.width);
                int pixlY = (int)(textureCoord.y * _dirtMaskBase.height);

                Vector2Int paintPixlPos = new Vector2Int(pixlX, pixlY);

                Debug.Log("UV" + textureCoord + " ; pixels: " + paintPixlPos);
            }

               
        }
            
    }

    
}
