using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformTriggerHandler : MonoBehaviour
{
    const string kPlayer = "Player";


    [SerializeField]
    Transform geometry;
    [SerializeField]
    TriggerObject playerTrigger;


    private void Start()
    {
        playerTrigger.tagCondition = kPlayer;
        playerTrigger.OnTriggerEnterDelegate = OnTriggerPlayerEnterCallback;
        playerTrigger.OnTriggerExitDelegate = OnTriggerPlayerExitCallback;

    }

    void OnTriggerPlayerEnterCallback(Transform other)
    {
        Debug.Log("OnTriggerPlayerEnterCallback");
        other.SetParent(transform);
    }

    public void OnTriggerPlayerExitCallback(Transform other)
    {
        Debug.Log("OnTriggerPlayerExitCallback");
        other.SetParent(null);
    }
}
