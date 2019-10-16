using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class ClientTest : MonoBehaviour
{
    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    public Canvas canvas;

    private void Start()
    {
        //ConnectToServer();
    }
    public void ConnectToServer()
    {
        if (socketReady)
            return;

        string host = "192.168.43.185";
        int port = 6321;


        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;

        }
        catch (System.Exception e)
        {
            Debug.LogError("Socket error : " + e.Message);
        }
    }

    private void Update()
    {
        canvas.enabled = true;
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    Debug.Log(data);
                    OnIncomingData(data);
                }
            }
        }
    }

    private void OnIncomingData(string data)
    {
        canvas.enabled = false;
        //ball.GetComponent<Rigidbody>().AddForce(camera.forward * force);
        //ball.AddForce(camera.forward * force);
    }
}
