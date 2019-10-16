using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startPosition;
    private Quaternion startRotation;
    public float force;
    public Transform camera;
    private bool isStart;

    private int maxShoot = 2;
    private int currentShoot = 0;
    private bool flagRound = false;
    private bool completeRound = false;


    private AudioSource[] audio;

    private AudioClip clip1;
    private AudioClip clip2;
    void Start()
    {
        startPosition = transform.position;
        startRotation = camera.rotation;
        isStart = true;

        audio = GetComponents<AudioSource>();
        clip1 = audio[0].clip;
        clip2 = audio[1].clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5f)
        {
            transform.position = startPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            isStart = true;

            if (flagRound)
            {
                completeRound = true;
                currentShoot = 0;
            }


        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //GetComponent<Rigidbody>().AddForce(new Vector3(-camera.rotation.x*1000, camera.rotation.y*1000, force));
            GetComponent<Rigidbody>().AddForce(camera.forward * force);
            Clip1();
            isStart = false;
            currentShoot++;
            /*if (currentShoot > maxShoot)
            {
                currentShoot = 1;
            }*/

            if (currentShoot == maxShoot)
            {
                flagRound = true;
            }
                
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            //transform.rotation = camera.rotation;
        }
    }
    
    public void accBowling()
    {
        isStart = false;
        currentShoot++;
        /*if (currentShoot > maxShoot)
        {
            currentShoot = 1;
        }*/

        if (currentShoot == maxShoot)
        {
            flagRound = true;
        }
    }
    public bool getStatusBall()
    {
        return isStart;
    }

    public void setBallShoot()
    {
        isStart = false;
    }

    public int getCurrentShoot()
    {
        return currentShoot;
    }

    public bool getCompleteRound()
    {
        return completeRound;
    }

    public void Reset()
    {
        flagRound = false;
        completeRound = false;
    }

    public void Clip1()
    {
        GetComponent<AudioSource>().PlayOneShot(clip1);
    }

    public void Clip2()
    {
        GetComponent<AudioSource>().PlayOneShot(clip2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pin")
            Clip2();
    }

}
