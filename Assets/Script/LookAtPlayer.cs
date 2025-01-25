using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public GameObject target;

    public GameObject eyeL, eyeR, head;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        eyeL.transform.LookAt(target.transform.position);
        eyeR.transform.LookAt(target.transform.position);

    }
}
