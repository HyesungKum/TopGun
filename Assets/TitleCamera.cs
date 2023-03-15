using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    [SerializeField] private float turn_speed;

    // Start is called before the first frame update
    void Start()
    {
        turn_speed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, turn_speed, 0f);
    }
}
