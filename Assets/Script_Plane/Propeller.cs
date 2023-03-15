using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    [SerializeField] private float propfeller_speed;
    Player player;
    Rigidbody rigid_player;
    private void Awake()
    {
        rigid_player = GameObject.Find("Player").GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        propfeller_speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, player.throttle_speed*propfeller_speed));
    }
}
