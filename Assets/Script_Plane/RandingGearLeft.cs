using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandingGearLeft : MonoBehaviour
{
    private float timer;
    private bool Randing_Flag;

    [SerializeField] private float move_speed;
    [SerializeField] private float min_rotation;
    [SerializeField] private float max_rotation;

    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        this.Randing_Flag = true;

        min_rotation = 0f;
        max_rotation = 88f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (rotation.z < max_rotation && timer > 0.01 && Randing_Flag == true)
        {
            timer = 0;
            rotation.z += 1f;
        }
        
        if (rotation.z > min_rotation && timer > 0.01 && Randing_Flag == false)
        {
            timer = 0;
            rotation.z -= 1f;
        }

        transform.localEulerAngles = new Vector3(0f, 0f, rotation.z);
    }

    void SWRanding()//recive message
    {
        if (this.Randing_Flag == true)
        {
            this.Randing_Flag = false;
        }
        else
        {
            this.Randing_Flag = true;
        }
    }
}

