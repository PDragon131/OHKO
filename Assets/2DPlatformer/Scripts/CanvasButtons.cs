using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasButtons : MonoBehaviour {


    public GameManager characters;
    public GameObject cameraMenu;
    private Animator animator;
    public GameObject arrowSy;
    public GameObject arrowSy2;
    public GameObject arrowMehgeek;
    public GameObject arrowMehgeek2;
    public GameObject arrowFalls;
    public GameObject arrowDragon;

    private bool pressedSy;
    private bool pressedMegek;
    private bool pressedSy2;
    private bool pressedMegek2;
    private SaveData _saveData;

    private int map = 1;


    private void Start()
    {
        animator = cameraMenu.GetComponent<Animator>();
        characters = GameManager.instance;

    }


    public void Play()
    {
        animator.SetBool("Play",true);
    }   

    public void OHKO()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + map);
		characters.matches++;
		characters.CallAddMatches();
    }

    public void Falls()
    {
        map = 1;
        arrowFalls.SetActive(true);
        arrowDragon.SetActive(false);
}

    public void DragonCave()
    {
        map = 2;
        arrowFalls.SetActive(false);
        arrowDragon.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene_Menu");
    }

    public void Sy()
    {
        if (!pressedSy)
        { 
            pressedSy = true;
            characters.characters.Add(Resources.Load<GameObject>("Sy"));
            arrowSy.SetActive(true);
        }
    }

    public void Megek()
    {
        if (!pressedMegek)
        {
            pressedMegek = true;
            characters.characters.Add(Resources.Load<GameObject>("Megek"));
            arrowMehgeek.SetActive(true);
        }
    }

    public void Sy2()
    {
        if (!pressedSy2)
        {
            pressedSy2 = true;
            characters.characters.Add(Resources.Load<GameObject>("Sy2"));
            arrowSy2.SetActive(true);
        }
    }

    public void Megek2()
    {
        if (!pressedMegek2)
        {
            pressedMegek2 = true;
            characters.characters.Add(Resources.Load<GameObject>("Megek2"));
            arrowMehgeek2.SetActive(true);
        }
    }

    public void ResetRagequits()
    {
        characters.ResetRagequit();
    }


    public void Controls()
    {
        animator.SetBool("Controls", true);
    }

    public void Shop()
    {
        animator.SetBool("Shop", true);

    }

    public void Options()
    {
        animator.SetBool("Options", true);

    }

    public void ReturnPlay()
    {
        animator.SetBool("Play", false);
        arrowMehgeek.SetActive(false);
        arrowMehgeek2.SetActive(false);
        arrowSy.SetActive(false);
        arrowSy2.SetActive(false);
        pressedSy = false;
        pressedMegek = false;
        pressedSy2 = false;
        pressedMegek2 = false;
        characters.characters.Clear();
        Falls();

    }

    public void ReturnOptions()
    {
        animator.SetBool("Options", false);

    }

    public void ReturnShop()
    {
        animator.SetBool("Shop", false);

    }

    public void ReturnControls()
    {
        animator.SetBool("Controls", false);

    }


















}
