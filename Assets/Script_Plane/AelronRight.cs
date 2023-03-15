using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AelronRight : MonoBehaviour
{
    private float tilt;
    private float yaw;

    [SerializeField] private float tilt_scale;
    [SerializeField] private float yaw_scale;

    // Start is called before the first frame update
    void Start()
    {
        yaw = 0f;
        tilt = 0f;
        tilt_scale = 15f;
        yaw_scale = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        tilt = Input.GetAxis("Horizontal");
        yaw = Input.GetAxis("Vertical");

        transform.localEulerAngles = new Vector3(((- yaw * yaw_scale) + (tilt * tilt_scale)), 0f, 0f);
    }
}
