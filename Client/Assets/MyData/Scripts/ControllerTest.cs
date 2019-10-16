using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerTest : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject exitInstruction;
    public GameObject client;
    private ClientTest scriptClientTest;
    public Canvas test;
    
    void Start()
    {
        scriptClientTest = client.GetComponent<ClientTest>();
        exitInstruction.SetActive(false);
        test.enabled = false;
    }

    public void StartConnection()
    {
        scriptClientTest.ConnectToServer();
        test.enabled = true;
        exitInstruction.SetActive(true);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
