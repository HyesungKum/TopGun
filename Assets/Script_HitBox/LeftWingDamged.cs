using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWingDamged : MonoBehaviour
{
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LeftWingCollision()
    {
        Player.SendMessage("LeftWingDamaged",SendMessageOptions.DontRequireReceiver);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player.SendMessage("LeftWingDamaged", SendMessageOptions.DontRequireReceiver);
    }
}
