using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    float bottomY = 0.0f;
    float topY = 0.2f;
    float speed = 0.5f;

    bool active;

    public DoorScript door;

    void Update()
    {
        if (active && transform.position.y > bottomY)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;

            
                door.isOpen = true;

            
        }
        else if(active == false && transform.position.y < topY)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            door.isOpen = false;



        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active =! active;
            
        }

    }
}