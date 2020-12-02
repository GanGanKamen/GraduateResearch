using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialPortStart : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Iwase");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
