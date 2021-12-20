# Hypercasual-Runner

Time of development: ~20hr

Difficulties:
    --The interaction between the rope physics and the auto-movement of a runner game
    --A fitting UI for gameplay and mobile resolutions

Code implementation details:
    --The levels' information is stored in a .json file in resources. This file is read at the loading of the game, allowing for easily introducing new games and iterating on their gameplay. Furthermore, the main menu is adjusted according to the number of levels in the file, so updating the screen for new levels is done automatically. This can be found in the GameManager and the JsonReader classes.
    --Namespaces are used to ensure abstraction.
    --The game is set as landscape only to incentivize the use of two fingers. In testing the game, the verticality of the game because of the grappling rope felt like a hindrance for portrait view.

Why is this game hypercasual:
    This game only has one mechanic apart from the auto-movement, the grappling rope, which helps in making a more accesible game. On the other hand this mechanic is based on physics, inciting the players to better their scores with little adjustments in their gameplay. Furthermore the use of the rope can also both speed up or slow down the character, helping all players to adjust how they face the level, but we also tie the fast completion of the level with a higher score to push for shorter levels.

How to play:
    --The character moves constantly forward, except for when it reaches the end goal.
    --Tap and hold on the purple platforms to throw the rope and hang on them. The character will keep moving, so it will swing on it gaining momentum to reach further.
    --The grappling rope has a surprising reach, so try aiming at further platforms and gain more speed!
    --Red platforms will kill the player. Brown spheres will give the player one more use of its rope. Golden coins reward some score.
    --The score for the level is based on fast completion, the amount of coins obtained through the level and how fast the character reaches in the yard stick.
    --The player also dies when reaching out of boundaries.

How to run the game:
    --The apk should be a simple install and play.
    --As the apk has not been signed, it might require authorizing the installation from an unknown source.
    --The apk has been tested in a Samsung Galaxy A5, Samsung Galaxy A70 and Samsung Galaxy A71.

Known Issues:
    --The skybox turns black in two most recent Samsung Galaxy versions. (On investigation)

Github: https://github.com/Delon-K/Hypercasual-Runner
 
Made in Unity 2020.3.24f1
Made with Assets from Unity Store:
--Cherry Blossom UI Pack: https://assetstore.unity.com/packages/2d/gui/icons/cherry-blossom-gui-pack-147391
--Hyper-Casual Stickman Characters: https://assetstore.unity.com/packages/3d/characters/humanoids/hyper-casual-stickman-characters-205790
--Animations from Mixamo
--Free Pixel Font: https://assetstore.unity.com/packages/2d/fonts/free-pixel-font-thaleah-140059
--Skybox Add-on: https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-add-on-136594