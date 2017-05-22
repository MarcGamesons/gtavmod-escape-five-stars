// ==================================================================================================================================
//
// This file is part of https://github.com/MarcGamesons/gtavmod-escape-five-stars which is released under UNLICENSE.
// Go to https://github.com/MarcGamesons/gtavmod-escape-five-stars/blob/master/UNLICENSE for full license details.
//
// For support open a new issue at https://github.com/MarcGamesons/gtavmod-escape-five-stars/issues/new.
//
// ==================================================================================================================================
using System;
using System.Windows.Forms;
using GTA;

public class EscapeFiveStars : Script
{
    // This is used to set the time in seconds that you have to survive. DEFAULT is 300 seconds (5 Minutes).
    // If you want to change this value you also have to change the value in line 56 to the same value.
    //
    int TimeUntilReatreat = 60;

    // This timer is used to determine since how many seconds the player is in wanted state.
    //
    int playerWantedSinceSeconds = 0;

    int playerWantedLevel = 0;

    // This bool is used to determine if the player has started the escape timer.
    //
    bool isEscapeTimerStarted = false;

    // Set a cooldown so the game only runs the function about every second.
    //
    int Cooldown = 500;

    // Save the current GameTime for later use.
    //
    int GametimeReference = Game.GameTime;


    public EscapeFiveStars()
    {
        Tick += OnTick;
        KeyUp += onKeyUp;
    }

    void OnTick(object sender, EventArgs e)
    {
        if (Game.GameTime > GametimeReference + Cooldown)
        {
            // Use the cooldown.
            //
            GametimeReference = Game.GameTime + Cooldown;

            // Variable to store current wanted level.
            //
            playerWantedLevel = Game.Player.WantedLevel;

            switch (playerWantedLevel)
            {
                case 1:
                    ScriptLogic();
                    break;
                case 2:
                    ScriptLogic();
                    break;
                case 3:
                    ScriptLogic();
                    break;
                case 4:
                    ScriptLogic();
                    break;
                case 5:
                    ScriptLogic();
                    break;
                default:
                    break;
            }
        }
    }

    // Detect keyUp events.
    //
    private void onKeyUp(object sender, KeyEventArgs e)
    {
        // Detect if the player pressed the H key.
        //
        if (e.KeyCode == Keys.T)
        {
            // Start the escape timer.
            //
            isEscapeTimerStarted = true;
        }
    }

    private void ScriptLogic()
    {
        // Increase the "timer".
        //
        playerWantedSinceSeconds++;

        // Notify the player that he/she can start the escape timer.
        //
        if (playerWantedSinceSeconds >= 60)
        {
            UI.Notify("You can now press T to start the escape timer.", true);
        }

        // If you are playing with a mod that uses the 6 star system change the 5 to a 6
        // 
        if (Game.Player.WantedLevel == 5 || isEscapeTimerStarted)
        {
            // If the time runs out...
            //
            if (TimeUntilReatreat <= 1)
            {
                // ...set the wanted level of the Player to 0.
                //
                Game.Player.WantedLevel = 0;

                // If the wanted level of the Player is 0...
                //
                if (Game.Player.WantedLevel == 0)
                {
                    // Disable the bool that is used to determine if the escape timer is started.
                    //
                    isEscapeTimerStarted = false;

                    // ...reset the timer to 300.
                    //
                    TimeUntilReatreat = 60;
                }
            }
            else
            {
                // Decrease the timer.
                //
                TimeUntilReatreat--;

                // Show the time left on screen as a subtitle.
                //
                UI.ShowSubtitle("Survive! Time until troops retreat: " + TimeUntilReatreat, 600);
            }
        }
    }

}
