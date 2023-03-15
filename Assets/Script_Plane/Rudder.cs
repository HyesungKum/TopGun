using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rudder : MonoBehaviour
{
    private float tilt;
    [SerializeField] private float tilt_scale;
    private int tilt_flag;

    // Start is called before the first frame update
    void Start()
    {
        tilt_flag = 0;//0: idle 1: Right 2: Left

        tilt = 0f;
        tilt_scale = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && tilt < 1)
        {
            tilt += 0.1f;
        }
        else if (Input.GetKeyUp(KeyCode.Q) && tilt > 0)
        {
            tilt_flag = 2;
        }

        if (Input.GetKey(KeyCode.E) && tilt > -1)
        {
            tilt -= 0.1f;
        }
        else if (Input.GetKeyUp(KeyCode.E) && tilt < 0)
        {
            tilt_flag = 1;
        }


        if (tilt_flag == 1 && tilt < 0f)
        {
            tilt += 0.1f;
        }
        else if (tilt < 0)
        {
            tilt_flag = 0;
        }

        if (tilt_flag == 2 && tilt > 0f)
        {
            tilt -= 0.1f;
        }
        else if (tilt > 0)
        {
            tilt_flag = 0;
        }

        transform.localEulerAngles = new Vector3(0f, tilt * tilt_scale, 0f);
    }
}
