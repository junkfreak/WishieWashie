using UnityEngine;

public class Lift : MonoBehaviour
{
    public GameObject lift;

    public float maxDist, speed;

    public bool uppies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (uppies && lift.transform.localPosition.y < maxDist)
        {
            lift.transform.Translate(0,speed,0);
        }
        if(uppies == false && lift.transform.localPosition.y >= 1)
        {
            lift.transform.Translate(0, -speed, 0);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == 8)
        {
            uppies = true;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            uppies = false;
        }
    }
}
