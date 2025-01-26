using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject reus, startButt, resetButt;

    public bool start, reset;

    public string sceneName;

    public BoxCollider box;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (start)
        {
            Debug.Log(other.gameObject);
            reus.SetActive(true);
            resetButt.SetActive(true);
            // startButt.SetActive(false);
            box.enabled = false;
            //reset = true;
            start = false;
        }

        if (reset)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
