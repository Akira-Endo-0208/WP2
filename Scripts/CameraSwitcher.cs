using UnityEngine;
using Cinemachine;


public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCameraBase vcam1;
    [SerializeField]
    private CinemachineVirtualCameraBase vcam2;

    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
    }
}