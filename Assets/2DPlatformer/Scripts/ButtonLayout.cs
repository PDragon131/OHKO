using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLayout : MonoBehaviour {

    public bool player1;
    public bool player2;
    public bool player3;
    public bool player4;

    public GameManager characters;

    void Start ()
    {
        characters = GameManager.instance;
        SetupControls();

    }

    public void SetupControls()
    {
        PlayerPlatformerController ppc = GetComponentInChildren<PlayerPlatformerController>();

        if (player1)
        {
            characters.SpawnPlayer1Character();
            ppc.inputHorizontal = "HorizontalPlayer1";
            ppc.inputJump = "JumpPlayer1";
            ppc.inputAttack = "AttackPlayer1";
            ppc.buttonDash = KeyCode.E;
            ppc.buttonDefense = KeyCode.Q;
            ppc.buttonTP = KeyCode.R;
            
        }
        else if(player2)
        {
            characters.SpawnPlayer2Character();
            ppc.inputHorizontal = "HorizontalPlayer2";
            ppc.inputJump = "JumpPlayer2";
            ppc.inputAttack = "AttackPlayer2";
            ppc.buttonDash = KeyCode.Minus;
            ppc.buttonDefense = KeyCode.RightControl;
            ppc.buttonTP = KeyCode.Keypad1;
        }
        else if (player3)
        {
            characters.SpawnPlayer3Character();
            ppc.inputHorizontal = "HorizontalPlayer3";
            ppc.inputJump = "JumpPlayer3";
            ppc.inputAttack = "AttackPlayer3";
            ppc.buttonDash = KeyCode.Joystick1Button0;
            ppc.buttonDefense = KeyCode.Joystick1Button3;
            ppc.buttonTP = KeyCode.Joystick1Button5;
        }

        else if (player4)
        {
            characters.SpawnPlayer3Character();
            ppc.inputHorizontal = "HorizontalPlayer4";
            ppc.inputJump = "JumpPlayer4";
            ppc.inputAttack = "AttackPlayer4";
            ppc.buttonDash = KeyCode.Joystick2Button0;
            ppc.buttonDefense = KeyCode.Joystick2Button3;
            ppc.buttonTP = KeyCode.Joystick2Button5;
        }

    }
}
