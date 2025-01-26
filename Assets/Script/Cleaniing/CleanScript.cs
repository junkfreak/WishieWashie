

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


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
    public float rayDist, handDist, rayRadius, sphereDist;

    public GameObject handL, handR, pisser;

    public LayerMask mask;



    public AudioSource soapSource, pissSource;
    public bool soapLand;


    public float dirtAmountTotal;
    public float dirtAmount;

    public float dirtpercent;

    public Slider slide;
    public GameObject slideobj;
    Vector3 slidemeter;

    public Buttons butt;
    public void Awake()
    {
        slidemeter = slideobj.transform.localScale;
        ShootAct = pcntrl.FindActionMap("Player").FindAction("Attack");


        ShootAct.performed += context => shoot = true;
        ShootAct.canceled += context => shoot = false;




        _material = reus.GetComponent<SkinnedMeshRenderer>().material;
        dirtMaskTextureBase = _material.GetTexture("_greenmask") as Texture2D;

        _dirtMaskBase = new Texture2D(dirtMaskTextureBase.width , dirtMaskTextureBase.height);
        _dirtMaskBase.SetPixels(dirtMaskTextureBase.GetPixels());
        //_dirtMaskBase.graphicsFormat = TextureFormat.RGBA32;
        _dirtMaskBase.Apply();


        //_dirtMaskBase.SetPixels(dirtMaskTextureBase.GetPixels());
        //_dirtMaskBase.Apply();
        _material.SetTexture("_greenmask", _dirtMaskBase);

        DirtScore();
    }
    private void Start()
    {
        
        //_dirtMaskBase = _material.GetTexture("_greenmask") as Texture2D;
        //_dirtMaskBase = _material.
        //CreateTexture();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(handL.transform.forward * sphereDist, rayRadius);
        Gizmos.DrawSphere(handR.transform.forward * sphereDist, rayRadius);
    }

    private void Update()
    {
        dirtpercent = Mathf.RoundToInt(GetDirtAmount() * 100f);
        slide.value = dirtpercent;
        slidemeter.z = dirtpercent / 100;
        slideobj.transform.localScale = slidemeter;

        if (dirtpercent <= 40)
        {
            butt.goalReached = true;
        }
        //DirtScore();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (soapLand)
        {
            soapSource.enabled = true;
        }
        else
        {
            soapSource.enabled = false;
        }


        if(shoot == false)
        {
            pissSource.enabled = false ;
            pisser.SetActive(false);
        }
        //Debug.DrawRay(handL.transform.position, - handL.transform.forward, Color.red, handDist);
        if (Physics.SphereCast(handL.transform.position, rayRadius, handL.transform.forward, out RaycastHit hithand, sphereDist, ~mask))
            //if (Physics.Raycast(handL.transform.position, - handL.transform.forward, out RaycastHit hithand, handDist, ~mask))
        {
            //audio part
          if(soapLand == false)
            {
                soapLand = true;
            }

            Vector2 textureCoord = hithand.textureCoord;
            //Debug.Log(hithand);

            int pixlX = (int)(textureCoord.x * _dirtMaskBase.width);
            int pixlY = (int)(textureCoord.y * _dirtMaskBase.height);

            Vector2Int paintPixlPos = new Vector2Int(pixlX, pixlY);

            Debug.Log("UV" + textureCoord + " ; pixels: " + paintPixlPos);


            int pixlOffsetX = pixlX - (_brush.width / 6);
            int pixlOffsetY = pixlY - (_brush.height / 6);

            for (int x = 0; x < _brush.width; x++)
            {
                for (int y = 0; y < _brush.height; y++)
                {
                    //Debug.Log("painting");
                    Color pixDirt = _brush.GetPixel(x, y);
                    Color pixMask = _dirtMaskBase.GetPixel(pixlOffsetX + x, pixlOffsetY + y);

                    float removedAmount = pixMask.g - (pixMask.g * pixDirt.g);
                    dirtAmount -= removedAmount;

                    _dirtMaskBase.SetPixel(pixlOffsetX + x, pixlOffsetY + y, new Color(0, pixMask.g * pixDirt.g, 0));
                }

            }

            _dirtMaskBase.Apply();
        }
        else
        {
            soapLand = false;
        }
        


        //Debug.DrawRay(handR.transform.position, -handR.transform.forward, Color.red, handDist);
        if (Physics.SphereCast(handR.transform.position,rayRadius,handR.transform.forward, out RaycastHit hithand2, sphereDist, ~mask))
          //if (Physics.Raycast(handR.transform.position, -handR.transform.forward, out RaycastHit hithand2, handDist, ~mask))
        {
            Vector2 textureCoord = hithand2.textureCoord;
            Debug.Log(textureCoord);

            int pixlX = (int)(textureCoord.x * _dirtMaskBase.width);
            int pixlY = (int)(textureCoord.y * _dirtMaskBase.height);

            Vector2Int paintPixlPos = new Vector2Int(pixlX, pixlY);

            Debug.Log("UV" + textureCoord + " ; pixels: " + paintPixlPos);


            int pixlOffsetX = pixlX - (_brush.width / 6);
            int pixlOffsetY = pixlY - (_brush.height / 6);

            for (int x = 0; x < _brush.width; x++)
            {
                for (int y = 0; y < _brush.height; y++)
                {
                    //Debug.Log("painting");
                    Color pixDirt = _brush.GetPixel(x, y);
                    Color pixMask = _dirtMaskBase.GetPixel(pixlOffsetX + x, pixlOffsetY + y);

                    float removedAmount = pixMask.g - (pixMask.g * pixDirt.g);
                    dirtAmount -= removedAmount;

                    _dirtMaskBase.SetPixel(pixlOffsetX + x, pixlOffsetY + y, new Color(0, pixMask.g * pixDirt.g, 0));
                }

            }

            _dirtMaskBase.Apply();
        }
        else
        {
            soapLand = false;
        }


        //Debug.DrawLine(_camera.transform.position, _camera.transform.forward, Color.red, rayDist);
        
        if (shoot)
        {
            pissSource.enabled = true;
            pisser.SetActive(true);
            //Debug.DrawRay(_camera.transform.position, _camera.transform.forward, Color.yellow, rayDist);
            Debug.DrawLine(_camera.transform.position, _camera.transform.forward, Color.yellow, rayDist);
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

                        float removedAmount = pixMask.g - (pixMask.g * pixDirt.g);
                        dirtAmount -= removedAmount;

                        _dirtMaskBase.SetPixel(pixlOffsetX + x, pixlOffsetY + y, new Color(0,pixMask.g * pixDirt.g,0));
                    }

                }

                _dirtMaskBase.Apply();
            }

               
        }
            
    }

    public void DirtScore()
    {
        for(int x = 0; x < dirtMaskTextureBase.width; x++)
        {
            for (int y = 0; y < dirtMaskTextureBase.height; y++)
            {
                dirtAmountTotal += dirtMaskTextureBase.GetPixel(x, y).g;
            }

        }

        dirtAmount = dirtAmountTotal;
       
        dirtpercent = Mathf.RoundToInt(GetDirtAmount() * 100f);
        Debug.Log("dirt percentage" + dirtpercent );
    }

    private float GetDirtAmount()
    {
        return this.dirtAmount / dirtAmountTotal;
    }
}
