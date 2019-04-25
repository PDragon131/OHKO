using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour {

    private GameManager _gameManager;
    public GameObject menuPause;

    private void Start()
    {
        _gameManager = GameManager.instance;
    }


    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _gameManager.RageQuit();

            SceneManager.LoadScene("Scene_Menu");
        }

    }
}
