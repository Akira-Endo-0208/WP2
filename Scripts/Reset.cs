using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//‚±‚Ìs‚ğ’Ç‹L

public class Reset : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r")) //‚±‚Ìif•¶‚ğ’Ç‹L
        {
            SceneManager.LoadScene(1);
        }
    }
}
