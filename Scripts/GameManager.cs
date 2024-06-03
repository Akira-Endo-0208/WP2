using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject goalText;
    bool isGameClear = false;
    bool isGameOver = false;

    void Update()
    {
        if (isGameClear == true && Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

        [SerializeField]
    BoxCollider boxCollider;
    public void GameClear()
    {

        if (isGameClear || isGameOver)
        {
            return;
        }
        goalText.SetActive(true);

        isGameClear = true;

 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") 
        {
            GameClear();
        }
    }
}
