using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //var
    [SerializeField] private TextMeshProUGUI ammo_count;
    private float max_fuel;
    private float max_left_wing_health;
    private float max_right_wing_health;
    private float max_body_health;
    private float max_propeller_health;

    private bool main_cam_flag;
    private bool is_gameover;
    private int screen_flag;

    private float timer;

    //image
    [SerializeField] private Image left_wing;
    [SerializeField] private Image right_wing;
    [SerializeField] private Image body;
    [SerializeField] private Image propeller;

    [SerializeField] private Image weapon;
    [SerializeField] private Image ammo;

    //components
    [SerializeField] private Player player;
    [SerializeField] private Gun gun;

    Rigidbody player_rigid;

    //UI active object
    private GameObject PlayUI;
    private GameObject AimUI;
    private GameObject GameStartUI;
    private GameObject GameOverUI;

    //camera
    private GameObject main_camera;
    private GameObject sub_camera;
    private GameObject sub_back_camera;
    private GameObject title_camera;
    private GameObject play_ui_camera;
    private GameObject game_over_camera;

    private void Awake()
    {
        //components
        player_rigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<Player>();
        gun = GameObject.Find("GunLeft").GetComponent<Gun>();

        //var
        ammo_count = GameObject.Find("AmmoCount").GetComponent<TextMeshProUGUI>();

        //image
        left_wing = GameObject.Find("LeftWingHealth").GetComponent<Image>();
        right_wing = GameObject.Find("RightWingHealth").GetComponent<Image>();
        body = GameObject.Find("BodyHealth").GetComponent<Image>();
        propeller = GameObject.Find("PropellerHealth").GetComponent<Image>();

        weapon = GameObject.Find("Weapon").GetComponent<Image>();
        ammo = GameObject.Find("Ammo").GetComponent<Image>();

        //UI active object
        PlayUI = GameObject.Find("PlayUI");
        AimUI = GameObject.Find("AimUI");
        GameStartUI = GameObject.Find("GameStartUI");
        GameOverUI = GameObject.Find("GameOverUI"); ;

        //cammera
        main_camera = GameObject.Find("MainCamera");
        sub_camera = GameObject.Find("SubCamera");
        sub_back_camera = GameObject.Find("SubBackCamera");
        title_camera = GameObject.Find("TitleCamera");
        play_ui_camera = GameObject.Find("UICamera");
        game_over_camera = GameObject.Find("GameOverCamera");
    }

    // Start is called before the first frame update
    void Start()
    {
        //cursor disable
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //var
        main_cam_flag = true;
        is_gameover = false;
        screen_flag = 1;
        timer = 0f;
        
        max_fuel = player.fuel;
        max_propeller_health = player.propeller_health;
        max_left_wing_health = player.left_wing_health;
        max_right_wing_health = player.right_wing_health;
        max_body_health = player.body_health;

        //image init
        weapon.color = Color.white;
        ammo.color = Color.green;
        left_wing.color = Color.green;
        right_wing.color = Color.green;
        body.color = Color.green;
        propeller.color = Color.green;

        //UI init
        GameOverUI.SetActive(false);

        //camera init
        main_camera.SetActive(false);
        sub_camera.SetActive(false);
        sub_back_camera.SetActive(false);
        play_ui_camera.SetActive(false);
        game_over_camera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ScreenControll();

        if (screen_flag == 2)
        {
            CameraControll();
        }
        if (screen_flag == 3)
        {
            //restart
        }
    }

    private void LateUpdate()
    {
        PlayUI.SetActive(true);
        if (screen_flag == 2)
        {
            AmmoWatch();
            HealthWatch();
        }
    }
    private void CameraControll()//camera controll
    {
        //view controll
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            main_cam_flag = true;
            main_camera.SetActive(true);
            sub_camera.SetActive(false);
            sub_back_camera.SetActive(false);

            AimUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            main_cam_flag = false;
            main_camera.SetActive(false);
            sub_camera.SetActive(true);
            sub_back_camera.SetActive(false);

            AimUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            main_cam_flag = false;
            main_camera.SetActive(false);
            sub_camera.SetActive(false);
            sub_back_camera.SetActive(true);

            AimUI.SetActive(false);
        }

        //view option
        if (main_cam_flag == true)
        {
            Camera.main.fieldOfView = 65 + (player_rigid.velocity.magnitude / 10);
        }
    }
    private void AmmoWatch()//ammo watching
    {
        if (gun.ammo > 0)
        {
            if (gun.ammo < 50)
            {
                ammo.color = Color.yellow;
                ammo_count.color = Color.yellow;
            }
            ammo_count.text = gun.ammo.ToString();
        }
        else
        {
            ammo.color = Color.red;
            ammo_count.color = Color.red;
            ammo_count.text = "Empty";
        }
    }
    private void HealthWatch()//health watching
    {
        //left wing
        if (player.left_wing_health > max_left_wing_health * (2f / 3f))
        {
            left_wing.color = Color.green;
        }
        else if (player.left_wing_health > max_left_wing_health * (1f / 3f))
        {
            left_wing.color = Color.yellow;
        }
        else if (player.left_wing_health > 0)
        {
            left_wing.color = Color.red;
        }
        else
        {
            left_wing.color = Color.black;
        }

        //right wing
        if (player.right_wing_health > max_right_wing_health * (2f / 3f))
        {
            right_wing.color = Color.green;
        }
        else if (player.right_wing_health > max_right_wing_health * (1f / 3f))
        {
            right_wing.color = Color.yellow;
        }
        else if (player.right_wing_health > 0)
        {
            right_wing.color = Color.red;
        }
        else
        {
            right_wing.color = Color.black;
        }

        //propeller
        if (player.propeller_health > max_propeller_health * (2f / 3f))
        {
            propeller.color = Color.green;
        }
        else if (player.propeller_health > max_propeller_health * (1f / 3f))
        {
            propeller.color = Color.yellow;
        }
        else if (player.propeller_health > 0)
        {
            propeller.color = Color.red;
        }
        else
        {
            propeller.color = Color.black;
        }

        //body
        if (player.body_health > max_body_health * (2f / 3f))
        {
            body.color = Color.green;
        }
        else if (player.body_health > max_body_health * (1f / 3f))
        {
            body.color = Color.yellow;
        }
        else if (player.body_health > 0)
        {
            body.color = Color.red;
        }
        else
        {
            body.color = Color.black;
        }
    }
    private void AimUIControll()//AimUIControll
    {
        
    }
    private void ScreenControll()//start to play screen controll
    {
        if (screen_flag == 1 && Input.GetKeyUp(KeyCode.F))
        {
            screen_flag = 2;

            //UI
            GameStartUI.SetActive(false);

            //camera
            main_camera.SetActive(true);
            title_camera.SetActive(false);
            play_ui_camera.SetActive(true);
        }
    }
    public void GameOver()//game over
    {
        screen_flag = 3;//gameover screen

        //UI
        GameOverUI.SetActive(true);
        
        //camera
        main_camera.SetActive(false);
        sub_camera.SetActive(false);
        sub_back_camera.SetActive(false);
        play_ui_camera.SetActive(false);
        game_over_camera.SetActive(true);

        //best record
    }
}
