// ==================================================================================================================================
//
// This file is part of https://github.com/MarcGamesons/gtavmod-escape-five-stars which is released under UNLICENSE.
// Go to https://github.com/MarcGamesons/gtavmod-escape-five-stars/blob/master/UNLICENSE for full license details.
//
// For support open a new issue at https://github.com/MarcGamesons/gtavmod-escape-five-stars/issues/new.
//
// ==================================================================================================================================
using System;
using GTA;

public class EscapeFiveStars : Script
{
    // This is used to set the time in seconds that you have to survive.
    // If you want to change this value you also have to change the value in line 48 to the same value.
    //
    int TimeUntilReatreat = 1000;
    
    // Set a cooldown so the game only runs the function about every second.
    //
    int Cooldown = 500;

    // Save the current GameTime for later use.
    //
    int GametimeReference = Game.GameTime;


    public EscapeFiveStars()
    {
        Tick += OnTick;
    }
    
    void OnTick(object sender, EventArgs e)
    {

        // If you are playing with a mod that uses the 6 star system change the 5 to a 6
        // 
        if (Game.GameTime > GametimeReference + Cooldown && Game.Player.WantedLevel == 5)
        {
            GametimeReference = Game.GameTime + Cooldown;

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
                    // ...reset the timer to 1000.
                    //
                    TimeUntilReatreat = 1000;
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
