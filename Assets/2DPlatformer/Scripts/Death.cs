using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;
    private int player1Lives;
    private int player2Lives;
    private int player3Lives;
    private int player4Lives;
    private bool death;
    private GameManager numberOfChar;
    public int playerCount;

    public GameObject Menu;
    public Text score;

    private void Start()
    {
        numberOfChar = GameManager.instance;
        playerCount = numberOfChar.characters.Count;

        if(playerCount == 2)
         {
            player1 = GameObject.Find("Player1");
            player2 = GameObject.Find("Player2");

            player1Lives = player1.GetComponentInChildren<PlayerPlatformerController>().lives;
            player2Lives = player2.GetComponentInChildren<PlayerPlatformerController>().lives;

            score.text = "Player1 lives: " + player1Lives + "                     Player2 Lives: " + player2Lives;

            death = true;
        }

        if (playerCount == 3)
        {
            player1 = GameObject.Find("Player1");
            player2 = GameObject.Find("Player2");
            player3 = GameObject.Find("Player3");

            player1Lives = player1.GetComponentInChildren<PlayerPlatformerController>().lives;
            player2Lives = player2.GetComponentInChildren<PlayerPlatformerController>().lives;
            player3Lives = player3.GetComponentInChildren<PlayerPlatformerController>().lives;

            score.text = "Player1 lives: " + player1Lives + "                  Player2 Lives: " + player2Lives +
            "                  Player3 Lives: " + player3Lives;

            death = true;
        }

        if (playerCount == 4)
        {
            player1 = GameObject.Find("Player1");
            player2 = GameObject.Find("Player2");
            player3 = GameObject.Find("Player3");
            player4 = GameObject.Find("Player4");

            player1Lives = player1.GetComponentInChildren<PlayerPlatformerController>().lives;
            player2Lives = player2.GetComponentInChildren<PlayerPlatformerController>().lives;
            player3Lives = player3.GetComponentInChildren<PlayerPlatformerController>().lives;
            player4Lives = player4.GetComponentInChildren<PlayerPlatformerController>().lives;

            score.text = "Player1 lives: " + player1Lives + "               Player2 Lives: " + player2Lives + 
            "               Player3 Lives: " + player3Lives + "          Player4 Lives: " + player4Lives;

            death = true;
        }


    }


    void RefreshScore()
    {
        if(playerCount == 2)
        {
            player1Lives = player1.GetComponentInChildren<PlayerPlatformerController>().lives;
            player2Lives = player2.GetComponentInChildren<PlayerPlatformerController>().lives;

            score.text = "Player1 lives: " + player1Lives + "                     Player2 Lives: " + player2Lives;
        }
        else if(playerCount == 3)
            {
            player1Lives = player1.GetComponentInChildren<PlayerPlatformerController>().lives;
            player2Lives = player2.GetComponentInChildren<PlayerPlatformerController>().lives;
            player3Lives = player3.GetComponentInChildren<PlayerPlatformerController>().lives;

            score.text = "Player1 lives: " + player1Lives + "                  Player2 Lives: " + player2Lives + "                  Player3 Lives: " + player3Lives;
        }
        else if (playerCount == 4)
        {
            player1Lives = player1.GetComponentInChildren<PlayerPlatformerController>().lives;
            player2Lives = player2.GetComponentInChildren<PlayerPlatformerController>().lives;
            player3Lives = player3.GetComponentInChildren<PlayerPlatformerController>().lives;
            player4Lives = player4.GetComponentInChildren<PlayerPlatformerController>().lives;

            score.text = "Player1 lives: " + player1Lives + "               Player2 Lives: " + player2Lives 
            + "               Player3 Lives: " + player3Lives + "          Player4 Lives: " + player4Lives;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && death)
        {
            other.GetComponent<PlayerPlatformerController>().lives -= 1;
            RefreshScore();
            if(other.GetComponent<PlayerPlatformerController>().lives > 0)
            {
                other.transform.localPosition = Vector2.zero;
                other.GetComponent<Collider2D>().isTrigger = false;
                other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                other.GetComponent<PlayerPlatformerController>().StartGuard();
                death = false;
            }
            CheckForEnd();
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            death = true;
        }
            
    }

    void CheckForEnd()
    {
        if(playerCount == 2)
        {
            if(player1Lives == 0)
        {
                score.text = "OHKO \n Player2 Wins";
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
        else if (player2Lives == 0)
            {
                score.text = "OHKO \n Player1 Wins";
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        else if(playerCount == 3)
        {
            if (player1Lives == 0 && player2Lives == 0)
            {
                score.text = "OHKO \n Player3 Wins";
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (player2Lives == 0 && player3Lives == 0)
            {
                score.text = "OHKO \n Player1 Wins";
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (player1Lives == 0 && player3Lives == 0)
            {
                score.text = "OHKO \n Player2 Wins";
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        else if (playerCount == 4)
        {
            if (player1Lives == 0 && player2Lives == 0 && player4Lives == 0)
            {
                score.text = "OHKO \n Player3 Wins";
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (player2Lives == 0 && player3Lives == 0 && player4Lives == 0)
            {
                score.text = "OHKO \n Player1 Wins";
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (player1Lives == 0 && player3Lives == 0 && player4Lives == 0)
            {
                score.text = "OHKO \n Player2 Wins";
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (player1Lives == 0 && player3Lives == 0 && player2Lives == 0)
            {
                score.text = "OHKO \n Player4 Wins";
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
        }

    }

}
