using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGMManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        BgmManager.Instance.Play("いびつな散歩道");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
