using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform PreviousRoom;
    [SerializeField] private Transform NextRoom;
    [SerializeField] private CameraController cam;

    private void Awake()
    {
        cam=Camera.main.GetComponent<CameraController>();   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(NextRoom);
                NextRoom.GetComponent<Room>().ActivateRoom(true);
                PreviousRoom.GetComponent<Room>().ActivateRoom(false);
            }

            else
            {
                cam.MoveToNewRoom(PreviousRoom);
                PreviousRoom.GetComponent<Room>().ActivateRoom(true);
                NextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}
