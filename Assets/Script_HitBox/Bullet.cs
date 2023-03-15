using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        speed = 300f;
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = transform.forward * speed;

        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")//hit enemy
        {
            collision.collider.SendMessage("Dameged", SendMessageOptions.DontRequireReceiver);
        }
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
        if (collision.collider.tag == "Terrian")//hit terrian
        {
            Destroy(gameObject, 0.3f);
        }
    }
}
