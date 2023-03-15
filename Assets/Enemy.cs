using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //state
    [SerializeField] private float health;
    [SerializeField] private float damage_scale;
    [SerializeField] private float turn_speed;
    private bool detective_flag;

    //component
    Rigidbody enemy_rigidbody;
    private Transform target;

    //var
    Quaternion rand_rotation;
    Quaternion cur_rotation;

    //timer
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //state
        detective_flag = false;
        health = 100f;
        damage_scale = 10f;
        turn_speed = 10f;

        //component\
        enemy_rigidbody = gameObject.GetComponent<Rigidbody>();
        target = FindObjectOfType<Player>().transform;

        //var
        rand_rotation = Quaternion.Euler(0f, 0f, 0f);
        cur_rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);

        //timer
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            gameObject.SetActive(false);
        }

        if (detective_flag == true)
        {
            transform.LookAt(target);
        }
        else
        {
            timer += Time.deltaTime;

            if (timer > 10)
            {
                timer = 0;
                rand_rotation = Quaternion.Euler(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));
                cur_rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            }
            transform.rotation = Quaternion.Lerp(cur_rotation, rand_rotation, 1);
        }

        enemy_rigidbody.AddRelativeForce(new Vector3(0f,0f,10000f), ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            health -= damage_scale;
        }
    }
    private void Detective()
    {
        detective_flag = true;
    }
    private void Missing()
    {
        detective_flag = false;
    }
}
