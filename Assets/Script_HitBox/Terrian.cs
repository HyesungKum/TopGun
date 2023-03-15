using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrian : MonoBehaviour
{
    [SerializeField] private GameObject bullet_impact_prefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)//bullet collision FX
    {
        if (collision.collider.gameObject.CompareTag("Bullet"))//bullet collision detection
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(bullet_impact_prefab, pos, rot);
        }
    }

    private void OnCollisionStay(Collision collision)//player collision check
    {
        if (collision.collider.gameObject.CompareTag("Lwing"))
        {
            collision.collider.SendMessage("LeftWingCollision", SendMessageOptions.DontRequireReceiver);
        }
        if (collision.collider.gameObject.CompareTag("Rwing"))
        {
            collision.collider.SendMessage("RightWingCollision", SendMessageOptions.DontRequireReceiver);
        }
        if (collision.collider.gameObject.CompareTag("Body"))
        {
            collision.collider.SendMessage("BodyCollision", SendMessageOptions.DontRequireReceiver);
        }
        if (collision.collider.gameObject.CompareTag("Propeller"))
        {
            collision.collider.SendMessage("PropellerCollision", SendMessageOptions.DontRequireReceiver);
        }
    }
}
