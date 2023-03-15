using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CamPositionMove();
    }

    private void CamPositionMove()
    {
        transform.position = new Vector3(player.transform.position.x, 3775f, player.transform.position.z);
        transform.rotation = Quaternion.Euler(90f, player.transform.rotation.eulerAngles.y, 0f);
    }
}
