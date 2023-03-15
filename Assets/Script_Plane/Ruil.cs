using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruil : MonoBehaviour
{
    private float yaw;
    [SerializeField] private float yaw_scale;
    // Start is called before the first frame update
    void Start()
    {
        yaw = 0f;
        yaw_scale = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        yaw = Input.GetAxis("Vertical");
        transform.localEulerAngles = new Vector3(-yaw * yaw_scale, 0f, 0f);
    }
}
