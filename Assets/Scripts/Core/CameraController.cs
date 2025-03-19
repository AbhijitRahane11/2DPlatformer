using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosx;
    private Vector3 velocity = Vector3.zero;


    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        //transform.position = Vector3.SmoothDamp(transform.position,new Vector3(currentPosx,transform.position.y,transform.position.z),ref velocity,speed);

        transform.position= new Vector3(player.position.x,transform.position.y,transform.position.z);  
    }
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosx=_newRoom.position.x; 
    }
}
