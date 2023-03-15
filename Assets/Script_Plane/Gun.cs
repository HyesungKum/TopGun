using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform muzzle_flash;

    Vector3 fixed_transform;

    [SerializeField] private GameObject bullet_prefab;
    [SerializeField] private GameObject read_bullet_prefab;
    [SerializeField] private float spawn_rate;
    [SerializeField] private int read_bullet_timing;
    [SerializeField] public int ammo;

    private int read_bullet_count;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        read_bullet_count = 0;
        read_bullet_timing = 5;

        ammo = 350;

        muzzle_flash = transform.GetChild(0);

        muzzle_flash.gameObject.SetActive(false);
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.1 && ammo > 0 && Input.GetKey(KeyCode.Space))//shoot random per 1sec
        {
            ammo--;
            read_bullet_count++;
            muzzle_flash.gameObject.SetActive(true);
            if (ammo <= 0)
            {
                muzzle_flash.gameObject.SetActive(false);
            }

            transform.Rotate(new Vector3(0f, 0f, 45f));
            timer = 0;
            fixed_transform = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            if (read_bullet_count == read_bullet_timing)
            {
                read_bullet_count = 0;
                GameObject read_bullet = Instantiate(read_bullet_prefab, fixed_transform, transform.rotation);
            }
            else 
            {
                GameObject bullet = Instantiate(bullet_prefab, fixed_transform, transform.rotation);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            muzzle_flash.gameObject.SetActive(false);
        }
    }
}
