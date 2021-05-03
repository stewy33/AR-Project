# AR-Project

The game is comprised of two scenes: Launcher and ARRoom. Launcher is the first scene a user encounters when they open the app - they are prompted for their name. After clicking on “Play”, the client is taken to ARRoom.

<img src="launcher screen.jpeg" width=24%/> <img src="game screen 1.jpeg" width=24%/><img src="game screen 2.jpeg" width=24%/><img src="game screen 3.jpeg" width=24%/>

The GameManager and Launcher script are responsible for the overall flow of the game. PlayerNameInputField handles and stores the player’s name inputted on the initial screen. PlayerController keeps the player’s head anchored to their position in world coordinates. SoccerBallController is responsible for soccer ball update synchronization, which takes into account lag in the soccer ball.

**Assets/Scripts/CalibrateWorld.cs** - This script is attached to the ARSessionOrigin object in the ARRoom scene. It waits until the Image Tracking API detects the world center marker or a foot marker. If it detects the world center marker, it sets the game's origin and rotation to be that of the marker, allowing for correct localization for all players in the environment. If it detects a foot marker, it creates a foot object and has it track the foot marker as it moves.

**mediapipe_objectron_test.ipynb** - We wanted to test viability of using MediaPipe's 3-D Object Detector (Objectron) for our foot tracking. In this notebook we measured inference time on a video as well as accuracy on individual photos with different angles, distances, number of feet, and kinds of shoes.

