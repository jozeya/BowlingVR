using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.IO;
using UnityEngine.UI;
using System;

public class Client : MonoBehaviour
{
    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    public float force;
    public Transform camera;
    public Transform ball;
    private Bowling scriptBall;

    private void Start()
    {
        ConnectToServer();
        scriptBall = ball.GetComponent<Bowling>();
    }
    public void ConnectToServer()
    {
        if (socketReady)
            return;

        string host = "192.168.43.185";
        int port = 6321;

        /*string h;
        int p;

        h = GameObject.Find("HostInput").GetComponent<InputField>().text;
        if (h != "")
            host = h;

        int.TryParse(GameObject.Find("PortInput").GetComponent<InputField>().text, out p);

        if (p != 0)
            port = p;*/

        //create the socket

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
        if (scriptBall.getStatusBall())
        {
            ball.GetComponent<Rigidbody>().AddForce(camera.forward * force);
            //scriptBall.setBallShoot();
            scriptBall.accBowling();
        }
        //ball.GetComponent<Rigidbody>().AddForce(camera.forward * force);
        //ball.AddForce(camera.forward * force);
    }
}
