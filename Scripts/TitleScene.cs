using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//�V�[���}�l�W�����g��L���ɂ���

public class Title : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) 
        {
            SceneManager.LoadScene("PlayScene");
        }

    }
}