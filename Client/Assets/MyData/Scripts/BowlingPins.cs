using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class BowlingPins : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Vector3> startPos = new List<Vector3>();
    private List<Transform> pins = new List<Transform>();
    public Text chanceOne;
    public Text chanceTwo;
    public Text score;
    public GvrReticlePointer pointer;

    private int numPinsDown = 0;
    private int pinsTurnOne = 0;
    private int gameChance = 0;

    public GameObject ballOption;
    private Bowling ballScript;

    private bool isRunning = true;
    void Start()
    {
        ballScript = ballOption.GetComponent<Bowling>();

        foreach (Transform child in transform)
        {
            startPos.Add(child.position);
            pins.Add(child);
        }

        pointer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Transform child in pins)
        {
            if (child.gameObject.activeInHierarchy && ((child.position.y < -5f) ||
            (child.eulerAngles.x > 80.0f && child.eulerAngles.x < 290.0f) ||
            (child.eulerAngles.z > 80.0f && child.eulerAngles.z < 290.0f)))
            //-26 98 x 70 z 60  x 290, 270 z 295
            //if (child.gameObject.activeInHierarchy && ( child.rotation.x > 25 || child.rotation.x < -25 ) )
            {
                child.gameObject.SetActive(false);
                numPinsDown++;
                //ballScript.Clip2();

            }
        }

        /*if (numPinsDown == 10)
        {
            pointer.enabled = true;
        }*/

        if (ballScript.getCurrentShoot() == 1)
        {
            pinsTurnOne = numPinsDown;
            chanceOne.text = numPinsDown.ToString();
            //score.text = numPinsDown.ToString();
        }

        if (ballScript.getCurrentShoot() == 2)
        {
            if (numPinsDown != pinsTurnOne)
            {
                chanceTwo.text = (numPinsDown - pinsTurnOne).ToString();
                //pinsTurnOne = 0;
            }
            else
                chanceTwo.text = "0";

            score.text = numPinsDown.ToString();
            isRunning = false;
        }

        if (numPinsDown == pins.Count  &&  ballScript.getCurrentShoot() == 1)
        {
            Strike();
        } 

        if (ballScript.getCompleteRound())
        {
            Debug.Log("entro");
            Reset();
            ballScript.Reset();
        }
    }

    private void Reset()
    {
        for (int i = 0; i < pins.Count; i++)
        {
            pins[i].gameObject.SetActive(true);
            pins[i].position = startPos[i];
            pins[i].rotation = Quaternion.identity;
            Rigidbody r = pins[i].GetComponent<Rigidbody>();
            r.velocity = Vector3.zero;
            r.angularVelocity = Vector3.zero;
        }

        numPinsDown = 0;
        pointer.enabled = false;
    }
    private void Strike()
    {
        for (int i = 0; i < pins.Count; i++)
        {
            pins[i].gameObject.SetActive(true);
            pins[i].position = startPos[i];
            pins[i].rotation = Quaternion.identity;
            Rigidbody r = pins[i].GetComponent<Rigidbody>();
            r.velocity = Vector3.zero;
            r.angularVelocity = Vector3.zero;
        }
    }
}
