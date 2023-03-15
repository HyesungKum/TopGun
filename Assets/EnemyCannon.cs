using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour
{
    //component
    [SerializeField] private GameObject bullet_prefab;
    private int count;

    //timer
    private float timer;
    private float timer2;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        //timer
        timer = 0f;
        timer2 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (count < 20 && timer > 0.1)
        {
            count++;
            timer = 0;
            GameObject bullet = Instantiate(bullet_prefab, transform.position, transform.rotation);
        }

        if (count >= 20)
        {
            timer2 += Time.deltaTime;

            if (timer2 > 3)
            {
                timer2 = 0;
                count = 0;
            }
        }
    }
}
