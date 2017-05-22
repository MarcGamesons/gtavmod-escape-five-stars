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
    int TimeUntilReatreat = 300;

    // This timer is used to determine since how many seconds the player is in wanted state.
    //
    int playerWantedSinceSeconds = 0;

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
        // Only Tick if player is in wanted state.
        //
        if (Game.Player.WantedLevel == 1 || Game.Player.WantedLevel == 2 || Game.Player.WantedLevel == 3 || Game.Player.WantedLevel == 4 || Game.Player.WantedLevel == 5)
        {
            Tick += OnTick;
            KeyUp += onKeyUp;
        }
    }

    void OnTick(object sender, EventArgs e)
    {
        if (Game.GameTime > GametimeReference + Cooldown)
        {
            GametimeReference = Game.GameTime + Cooldown;

            // Increase the "timer".
            //
            playerWantedSinceSeconds++;

            // DEBUG
            //
            UI.ShowSubtitle("playerWantedSinceSeconds: " + playerWantedSinceSeconds, 3000);

            /*if(playerWantedSinceSeconds >= 1)
            {
                if(isEscapeTimerStarted)
                {
                    UI.ShowSubtitle("TRUE", 3000);
                }
            }*/

            // If you are playing with a mod that uses the 6 star system change the 5 to a 6
            // 
            if (Game.Player.WantedLevel == 5)
            {
                // If the time runs out...
                //
                if (TimeUntilReatreat <= 0)
                {
                    // ...set the wanted level of the Player to 0.
                    //
                    Game.Player.WantedLevel = 0;

                    // If the wanted level of the Player is 0...
                    //
                    if (Game.Player.WantedLevel == 0)
                    {
                        // ...reset the timer to 300.
                        //
                        TimeUntilReatreat = 300;
                    }
                }
                else
                {
                    // Decrease the timer.
                    //
                    TimeUntilReatreat--;

                    // Show the time left on screen as a subtitle.
                    //
                    UI.ShowSubtitle("Survive! Time until troops retreat: " + TimeUntilReatreat, 3000);
                }
            }
        }
    }

    private void onKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.H)
        {
            isEscapeTimerStarted = true;
            UI.ShowSubtitle("You have pressed the H key.", 3000);
        }
    }
}
