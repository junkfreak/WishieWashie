
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CleanScript : MonoBehaviour
{

    public Camera _camera;
    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;
    [SerializeField] private Texture2D dirtMaskTextureBase;

    public Material _material;
    
    public GameObject reus;
    private InputAction ShootAct;

    public bool shoot;
    public PlayerInput pInput;
    public InputActionAsset pcntrl;
    public float rayDist;

    public LayerMask mask;
    public void Awake()
    {
        ShootAct = pcntrl.FindActionMap("Player").FindAction("Attack");


        ShootAct.performed += context => shoot = true;
        ShootAct.canceled += context => shoot = false;




        _material = reus.GetComponent<SkinnedMeshRenderer>().material;
        dirtMaskTextureBase = _material.GetTexture("_greenmask") as Texture2D;

        _dirtMaskBase = new Texture2D(dirtMaskTextureBase.width, dirtMaskTextureBase.height);
        //_dirtMaskBase.SetPixels(dirtMaskTextureBase.GetPixels());
        _dirtMaskBase.Apply();
        _material.SetTexture("_greenmask", _dirtMaskBase);
    }
    private void Start()
    {
        
        //_dirtMaskBase = _material.GetTexture("_greenmask") as Texture2D;
        //_dirtMaskBase = _material.
        //CreateTexture();
    }

    private void Update()
    {
        //Debug.DrawLine(_camera.transform.position, _camera.transform.forward, Color.red, rayDist);
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward, Color.yellow, rayDist);
        if (shoot)
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, rayDist, ~mask))
            {
                Vector2 textureCoord = hit.textureCoord;
                Debug.Log(textureCoord);

                int pixlX = (int)(textureCoord.x * _dirtMaskBase.width);
                int pixlY = (int)(textureCoord.y * _dirtMaskBase.height);

                Vector2Int paintPixlPos = new Vector2Int(pixlX, pixlY);

                Debug.Log("UV" + textureCoord + " ; pixels: " + paintPixlPos);


                int pixlOffsetX = pixlX - (_brush.width / 6);
                int pixlOffsetY = pixlY - (_brush.height / 6);

                for (int x = 0; x < _brush.width; x++ )
                {
                    for (int y = 0; y < _brush.height; y++)
                    {
                        //Debug.Log("painting");
                        Color pixDirt = _brush.GetPixel(x, y);
                        Color pixMask = _dirtMaskBase.GetPixel(pixlOffsetX + x, pixlOffsetY + y);


                        
                        _dirtMaskBase.SetPixel(pixlOffsetX + x, pixlOffsetY + y, new Color(0,pixMask.g * pixDirt.g,0));
                    }

                }

                _dirtMaskBase.Apply();
            }

               
        }
            
    }

    
}
