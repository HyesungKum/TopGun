//start 2022 08 10
//need camera zoom
//need bullet
//edit center of mass

//2022_08_12
//need higher down randing cam

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //components
    private Rigidbody player_rigid;

    [SerializeField] private GameObject burn_body_prefab;

    private GameObject propeller;

    private GameObject lw_smoke_fx;
    private GameObject rw_smoke_fx;
    private GameObject en_smoke_fx;
    
    private GameObject lw_fire_fx;
    private GameObject rw_fire_fx;
    private GameObject en_fire_fx;

    //position
    [SerializeField] private float position_x;
    [SerializeField] private float position_y;
    [SerializeField] private float position_z;

    //init state
    [SerializeField] const float max_fuel = 1000f;
    [SerializeField] const float max_left_wing_health = 100f;
    [SerializeField] const float max_right_wing_health = 100f;
    [SerializeField] const float max_body_health = 150f;
    [SerializeField] const float max_propeller_health = 80f;

    //state
    [SerializeField] public float fuel;
    [SerializeField] public float left_wing_health;
    [SerializeField] public float right_wing_health;
    [SerializeField] public float body_health;
    [SerializeField] public float propeller_health;
    [SerializeField] private float damage_scale;

    //timer
    private float timer;

    //throttle
    private bool flight_flag;
    private bool engine_flag;
    [SerializeField] public float throttle_speed;
    [SerializeField] private float player_max_speed;

    //aleron
    private float tilt_speed;
    private float yaw_speed;

    private void Awake()
    {
        timer = 0f;

        //physics
        player_rigid = GetComponent<Rigidbody>();

        //mesh
        propeller = GameObject.Find("PropelerCylinder");

        //FX
        lw_smoke_fx = GameObject.Find("LeftWingSmoke");
        rw_smoke_fx = GameObject.Find("RightWIngSmoke");
        en_smoke_fx = GameObject.Find("EngineSmoke");
        lw_fire_fx = GameObject.Find("LeftWingFire");
        rw_fire_fx = GameObject.Find("RightWingFire");
        en_fire_fx = GameObject.Find("EngineFire");

        //state
        fuel = max_fuel;
        left_wing_health = max_left_wing_health;
        right_wing_health = max_right_wing_health;
        body_health = max_body_health;
        propeller_health = max_propeller_health;
        damage_scale = 0.8f;

        //throttle
        flight_flag = false;
        player_max_speed = 100f;
        engine_flag = true;
        throttle_speed = 0f;

        //aleron
        tilt_speed = 300f;
        yaw_speed = 300f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //mesh
        propeller.SetActive(true);

        //fx
        lw_smoke_fx.SetActive(false);
        rw_smoke_fx.SetActive(false);
        en_smoke_fx.SetActive(false);
        lw_fire_fx.SetActive(false);
        rw_fire_fx.SetActive(false);
        en_fire_fx.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        ThrottleControll();//throttle controll
        AleronControll();//aleron controll
        RudderControl();//rudder controll
        PlanePartsControll();//plane parts controll
        DamageContorll();//damage state controll
        DieWatch();//watching plyaer dead
    }

    //Update last
    private void LateUpdate()
    {
        
    }

    private void ThrottleControll()//throttle controll
    {
        //key input
        timer += Time.deltaTime;
        if (engine_flag == true && timer > 1 && Input.GetKey(KeyCode.P))
        {
            timer = 0;
            if(player_rigid.velocity.magnitude < player_max_speed && fuel > 0)
            {
                throttle_speed += 0.1f;
            }
        }
        if (timer > 1 && Input.GetKey(KeyCode.O))
        {
            timer = 0;
            if (throttle_speed > 0.1)
            {
                throttle_speed -= 0.1f;
            }
        }

        //fuel empty
        if (fuel < 0)
        {
            throttle_speed = 0;
        }

        //engine state
        if (throttle_speed > 0)
        {
            //fuel condition
            if (fuel > 0)
            {
                fuel -= throttle_speed * (float)0.01;
            }

            engine_flag = true;
            if (throttle_speed > 0.3)
            {
                flight_flag = true;
            }
        }
        else
        {
            flight_flag = false;
        }

        //engine active check
        if (engine_flag == false)
        {
            throttle_speed = 0;
        }

        //move forward
        player_rigid.AddRelativeForce(new Vector3(0f, 0f, throttle_speed), ForceMode.VelocityChange);
    }
    private void AleronControll()//aleron controll
    {
        timer += Time.deltaTime;
        if (flight_flag == true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                timer = 0;
                transform.Rotate(new Vector3(0f, 0f, -0.1f * tilt_speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.A))
            {
                timer = 0;
                transform.Rotate(new Vector3(0f, 0f, 0.1f * tilt_speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.W))
            {
                timer = 0;
                transform.Rotate(new Vector3(0.1f * yaw_speed * Time.deltaTime, 0f, 0f));
            }
            if (Input.GetKey(KeyCode.S))
            {
                timer = 0;
                transform.Rotate(new Vector3(-0.1f * yaw_speed * Time.deltaTime, 0f, 0f));
            }
        }
    }
    private void RudderControl()//rudder horizontal tail wing controll
    {
        timer += Time.deltaTime;
        if (flight_flag == true)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                timer = 0;
                transform.Rotate(new Vector3(0f, -0.1f * tilt_speed * Time.deltaTime, 0f));
            }
            if (Input.GetKey(KeyCode.E))
            {
                timer = 0;
                transform.Rotate(new Vector3(0f, 0.1f * tilt_speed * Time.deltaTime, 0f));
            }
        }
    }
    private void PlanePartsControll()//send controll message to all plane parts
    {
        timer += Time.deltaTime;

        //randing gear controll
        if (timer > 3 && Input.GetKey(KeyCode.R))
        {
            timer = 0;
            BroadcastMessage("SWRanding", SendMessageOptions.DontRequireReceiver);
        }
    }
    private void DamageContorll()//damage state controll
    {
        if (right_wing_health < 60)//righit wing fx
        {
            rw_smoke_fx.SetActive(true);
            if (right_wing_health < 30)
            {
                rw_fire_fx.SetActive(true);
            }
        }

        if (left_wing_health < 60)//left wing fx
        {
            lw_smoke_fx.SetActive(true);
            if (left_wing_health < 30)
            {
                lw_fire_fx.SetActive(true);
            }
        }

        if (body_health < 80)//engine fx
        {
            en_smoke_fx.SetActive(true);
            if (body_health < 40)
            {
                en_fire_fx.SetActive(true);
            }
        }

        if (propeller_health < 0)//propeller disable
        {
            engine_flag = false;
            propeller.SetActive(false);
        }
    }
    private void DieWatch()//watching player dead
    {
        if (body_health < 0)
        {
            GameObject burn_body = Instantiate(burn_body_prefab, this.transform.position, this.transform.rotation);
            gameObject.SetActive(false);

            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.GameOver();
        }
    }
    #region get damaged message
    private void LeftWingDamaged()//left wing damaged
    {
        if (left_wing_health > 0)
        {
            left_wing_health -= damage_scale;
        }
        else
        {
            body_health -= damage_scale;
        }
    }
    private void RightWingDamaged()//right wing damaged
    {
        if (right_wing_health > 0)
        {
            right_wing_health -= damage_scale;
        }
        else
        {
            body_health -= damage_scale;
        }
    }
    private void BodyDamaged()//body wing damaged
    {
        body_health -= damage_scale * 3;
    }
    private void PropellerDamaged()//propeller wing damaged
    {
        if (propeller_health > 0)
        {
            propeller_health -= damage_scale * 2;
        }
        else
        {
            body_health -= damage_scale;
        }
    }
    #endregion
}
