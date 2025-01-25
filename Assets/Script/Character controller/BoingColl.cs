using UnityEngine;

public class BoingColl : MonoBehaviour
{

    public GameObject handL, handR;
    public float speed;
    public GameObject ballL, ballR;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void FixedUpdate()
    {
        ballL.transform.position = handL.transform.position;
        ballR.transform.position = handR.transform.position;
    }
}
