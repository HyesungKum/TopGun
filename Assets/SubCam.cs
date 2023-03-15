using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCam : MonoBehaviour
{
    Transform player;

    [SerializeField] private float rotate_sensitive;
    private float rotation_x;
    private float rotation_y;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        rotate_sensitive = 500f;
        rotation_x = 0f;
        rotation_y = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        rotation_x += Input.GetAxis("Mouse X") * rotate_sensitive * Time.deltaTime;
        rotation_y += Input.GetAxis("Mouse Y") * rotate_sensitive * Time.deltaTime;
        transform.eulerAngles = new Vector3(player.rotation.eulerAngles.x - rotation_y, player.rotation.eulerAngles.y + rotation_x, player.rotation.eulerAngles.z);

    }
}
