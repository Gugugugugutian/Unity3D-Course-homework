using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Core : MonoBehaviour
{
    public int initialLevel = 0; // Player's First level
    private int streak; // Player's score

    public Sound sound; 

    //  For random number generation 
    private System.Random rand_generator;
    private int seed;

    public GameObject player;    // Player
    public GameDispCore DisplayModule; // Display texts on screen. 

    /* Current game state:
     *  0:  Preparing
        1:  Playing     */
    public int game_state;


    /* Signal of game player state:
     * 0:   Normal.
     * 1:   Player died.
     * 2:   Player finish task. 
     */
    public int signal;
    public int cur_level;

    /* Level 1
     * Generate black pillars(cylinders),
     * when player runs into those pillars, 
     * he/she will lose this game
     */
    public CylinderGenerator cylinderGenerator;

    /* Level 2
     * Pick flowers 
     * and stand on the correct platform. 
     * Or you will lose this game.
     */
    public FlowerController flowerController; 
    public int color_flower, color_platform;

    /* Level 3
     * Stand on the mountain top and find
     * the silme with crown here. You 
     * must finish it before time ends. 
     */
    public SlimeController slimeController;
    public GameObject slimeOntheMountainTop;

    void lose()
    {
        sound.playSound();
        DisplayModule.ChangeLine("Game Ended.", 1);
        DisplayModule.ChangeLine("Score: " + streak.ToString(), 2);
        DisplayModule.ChangeLine("Q: Quit, R: Restart. ", 3);
        game_state = 0;
        signal = 0;
        DisplayModule.signal = 0;
        cylinderGenerator.cylindars_count = 0;
        cylinderGenerator.cylindars_size = 0;
    }

    void start()
    {
        DisplayModule.ChangeLine("Don't run into black pillars. ", 1);
        DisplayModule.ChangeLine("Last Score: " + streak.ToString(), 2);
        DisplayModule.ChangeLine("Q: Quit, R: Restart. ", 3);
        game_state = 1;
        cylinderGenerator.cylindars_count = 15;
        cylinderGenerator.cylindars_size = 1;
        streak = 0;
        signal = 0;
        DisplayModule.signal = 1;
        generateLevel(initialLevel);
        sound.playSound();
    }

    void win(int point = 1)
    {
        DisplayModule.signal = 0;
        streak += point;
        DisplayModule.ChangeLine("Score: " + streak.ToString(), 2);
        generateLevel(rand_generator.Next(0, 3));
        sound.playSound();
    }

    void task_complete()
    {
        signal = 2;
        DisplayModule.ChangeLine("You did it! ", 1);
        DisplayModule.countdown = (DisplayModule.countdown < 3) ? DisplayModule.countdown : 3;
        sound.playSound();
    }

    void generateLevel(int level)
    {
        cur_level = level;
        if (level == 0)
        {
            // Bigger Cylinders or More Cylinders.
            int p = rand_generator.Next(0, 2);
            switch (p)
            {
                case 0:
                    cylinderGenerator.cylindars_count += 5;
                    DisplayModule.ChangeLine("More pillars now. ", 1);
                    break;
                case 1:
                    cylinderGenerator.cylindars_size += 0.33f;
                    DisplayModule.ChangeLine("Bigger pillars now. ", 1);
                    break;
            }
            DisplayModule.countdown = 5;
            signal = 2;
        }
        else if (level == 1)
        {
            // Pick Flower and Stand On the Platform.
            /* COLORS:
             * 0:   Red
             * 1:   Yellow
             * 2:   Purple
             * 3:   White
             */
            string info_log = "Pick a "; 
            color_flower = rand_generator.Next(0, 4);
            color_platform = rand_generator.Next(0, 3);
            flowerController.flow_color = color_flower;
            flowerController.plat_color = color_platform;
            flowerController.signal = 0;
            switch (color_flower)
            {
                case 0:
                    info_log += "Red";
                    break;
                case 1:
                    info_log += "Yellow";
                    break;
                case 2:
                    info_log += "Purple";
                    break;
                case 3:
                    info_log += "White";
                    break;
            }
            info_log += " flower and stand on ";
            switch (color_platform)
            {
                case 0:
                    info_log += "Red";
                    break;
                case 1:
                    info_log += "Yellow";
                    break;
                case 2:
                    info_log += "Purple";
                    break;
            }

            DisplayModule.ChangeLine(info_log, 1);
            DisplayModule.countdown = 15;
            signal = 0;
        }
        else if (level == 2)
        {
            // Catch the Slime On the Mountain top. 
            slimeController.signal = 0;
            DisplayModule.ChangeLine("Catch the Slime On the Mountain top. ", 1);
            DisplayModule.countdown = 15;
            signal = 0;
        }
        else
        {
            print("Failed to load the level.");
            lose();
            DisplayModule.ChangeLine("There occurs an error in game. Please contact developer for help. ", 1);
        }
        DisplayModule.signal = 1;
        sound.playSound();
    }

    void Start()
    {
        seed = (int)DateTime.Now.Ticks;
        rand_generator = new System.Random(seed);   // use current time as seed. 
    }

    // Update is called once per frame
    void Update()
    {
        if (game_state > 0)
        {
            // Game Active
            if(player != null){
                Collider playerCollider = player.GetComponent<Collider>();
                
                foreach (var pillar in cylinderGenerator.pillars)
                {
                    Collider pillarCollider = pillar.GetComponent<Collider>();
                    // 检测碰撞
                    if (playerCollider != null && pillarCollider != null && playerCollider.bounds.Intersects(pillarCollider.bounds))
                    {
                        // 玩家和圆柱体发生碰撞，调用lose函数
                        lose();
                    }
                }
            }
            switch (cur_level)
            {
                case 0:
                    // more pillars or bigger pillars
                    break;
                case 1:
                    // flowers mode
                    DisplayModule.ChangeLine(flowerController.displayText, 2);
                    if (flowerController.signal == 1)
                    {
                        flowerController.signal = 0;
                        task_complete();
                    }
                    break;
                case 2:
                    // catch the slime
                    if (slimeController.signal == 1)
                    {
                        slimeController.signal = 0;
                        task_complete();
                    }
                    break;
                default: 
                    break;
            }

            if (player.transform.position.y < -2f)
            {
                // lose by break border
                lose();
            }
            if (DisplayModule.countdown == 0)
            {
                // continue or lose by signal
                if (signal == 2)
                {
                    win();
                }
                else
                {
                    lose();
                }
            }
        }
        else
        {
            // Game Not Active
        }

        // Press R to start. 
        if (Input.GetKeyDown(KeyCode.R))
        {
            start();
            print("Activating the game...");
        }

        // Press Q to exit. 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
            print("Quiting the game...");
        }
    }
}