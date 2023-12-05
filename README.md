# Maze-Game-3D
this is a Unity 3D Game with premade Maze environment player need to go through the maze to find the exit 
during navigating the maze there are some challenges to the player where obstacles are there. if player interacted with the obstacles the player will lose and have to start again from the start point. there are some checkpoints in the game when reached to them the players data will be saved and when restarting the game player will automatically start from the last chevkpoint.
1- Gamemanager- handles the data of game from retrieving the players checkpoint data to handling gameover. the checkpoint data is saved in the json.
2- ThirdPersonMovement- attached on the player and used cinemachine camera with WASD keys to navigate the maze.
3- Scoremanager- shows the time in seconds that player is taking to navigate the maze also saves when checkpoint reached.
4- Checkpointmanager- saves the checkpoint data in JSON once player reached to the checkpoint including players position to elapsed time
5- Obstacles - there is base class for obstacles and depending on that there are type of obstacles like Trap and Floating platform.
6- obstaclemanager- obstacle manager holds the list of all obstacles in the scene and depending on the players position it activates the obstacle which is close to the player.
7- AudioManager- there is one audio.manager class which includes the serializable class to specify between different type of audios and when player interacts with specific thing in the game we can easily play corresponding sound by passing simple enum types

future enhancements
1- adding sounds
2- creating Scriptable objects for audio manager and obstacles which will hold their respective data and we can easily use that data in our game

