using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveRange : MonoBehaviour
{
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.SendMessage("Detective", SendMessageOptions.DontRequireReceiver);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.SendMessage("Missing", SendMessageOptions.DontRequireReceiver);
        }
    }
}
