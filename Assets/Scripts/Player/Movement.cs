using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Player player;
    [HideInInspector] public CharacterController controller;
    public float Speed;
    public float sprintSpeed;
    public Transform orientation;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = transform.TransformDirection(input);
        move = Vector3.ClampMagnitude(move, 1f);
        move.y = -10;

        if (player.inventory.fishingRod.State != FishingRod.state.idle)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * Time.deltaTime * sprintSpeed);
        }
        else
        {

        controller.Move(move*Time.deltaTime*Speed);
        }
    }
}
