using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPS : MonoBehaviour
{
    CharacterController playerController;


    public Vector3 direction;

    public GameObject standOnObj;

    public float speed = 1;
    public float jumpPower = 5;
    public float gravity = 7f;


    public float mousespeed = 5f;


    public float minmouseY = -45f;
    public float maxmouseY = 45f;


    float RotationY = 0f;
    float RotationX = 0f;


    public Transform agretctCamera;

    bool switchCursor = true;

    // Use this for initialization
    void Start()
    {
        playerController = this.GetComponent<CharacterController>();
        Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.lockCursor == true)
        {
            float _horizontal = Input.GetAxis("Horizontal");
            float _vertical = Input.GetAxis("Vertical");

            if (playerController.isGrounded)
            {
                if (standOnObj != null && standOnObj.transform.GetComponent<CellScript>().Alive == true)
                {

                }
                else
                {
                    direction = new Vector3(_horizontal, 0, _vertical);
                    if (Input.GetKeyDown(KeyCode.Space))
                        direction.y = jumpPower;
                }
            }
            direction.y -= gravity * Time.deltaTime;
            playerController.Move(playerController.transform.TransformDirection(direction * Time.deltaTime * speed));

            RotationX += agretctCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mousespeed;
            RotationY -= Input.GetAxis("Mouse Y") * mousespeed;
            RotationY = Mathf.Clamp(RotationY, minmouseY, maxmouseY);
            this.transform.eulerAngles = new Vector3(0, RotationX, 0);
            agretctCamera.transform.eulerAngles = new Vector3(RotationY, RotationX, 0);
        }


        if (Input.GetKeyUp(KeyCode.E) && standOnObj != null)
        {
            standOnObj.transform.GetComponent<CellScript>().Alive = !standOnObj.transform.GetComponent<CellScript>().Alive;

            if (standOnObj.transform.GetComponent<CellScript>().Alive == true)//launching the player in the air
            {
                direction.y = jumpPower;
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            switchCursor = !switchCursor;
            Screen.lockCursor = switchCursor;
        }
    }
}