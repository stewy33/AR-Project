# AR-Project

The game is comprised of two scenes: Launcher and ARRoom. Launcher is the first scene a user encounters when they open the app - they are prompted for their name. After clicking on “Play”, the client is taken to ARRoom.

<img src="launcher screen.jpeg" width=24%/> <img src="game screen 1.jpeg" width=24%/><img src="game screen 2.jpeg" width=24%/><img src="game screen 3.jpeg" width=24%/>

The GameManager and Launcher script are responsible for the overall flow of the game. PlayerNameInputField handles and stores the player’s name inputted on the initial screen. PlayerController keeps the player’s head anchored to their position in world coordinates. SoccerBallController is responsible for soccer ball update synchronization, which takes into account lag in the soccer ball.

Before we started on the AR Room Scene, we did initial testing in a non-AR scene to get Photon networking implemented. The scene consisted of a simple boxed area with a ball in the middle. The master client could move the ball with on screen buttons. We were able have multiple devices connect to the scene and sync the movement of the ball to each client.

**Assets/Scripts/CalibrateWorld.cs** - This script is attached to the ARSessionOrigin object in the ARRoom scene. It waits until the Image Tracking API detects the world center marker or a foot marker. If it detects the world center marker, it sets the game's origin and rotation to be that of the marker, allowing for correct localization for all players in the environment. If it detects a foot marker, it creates a foot object and has it track the foot marker as it moves.

**mediapipe_objectron_test.ipynb** - We wanted to test viability of using MediaPipe's 3-D Object Detector (Objectron) for our foot tracking. In this notebook we measured inference time on a video as well as accuracy on individual photos with different angles, distances, number of feet, and kinds of shoes.

