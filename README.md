# TrueSight
This application serves as a simulation tool for navigation solutions tailored to assist visually impaired individuals. 
This project contains: 
- a 3D environment designed for immersive navigation experiences
- an intelligent path-finding guide that utilizes NavMesh technology for efficient route planning.
- an adaptable haptic interface, featuring customizable vibration settings which can be conveniently saved and loaded as per user preferences. 
- an additional mode to simulate the experience of visual impairment, enhancing the application's relevance and utility for understanding the challenges faced by visually impaired users.

## Quick start 

To begin using the application, start by selecting a pre-defined vibration preset or customizing your own vibration curves in the right panel when you select the `Player` object. After setting up your preferences, launch the game. For enabling the "Visual Impaired Mode," simply check the checkbox for the `Plane` component in the settings before starting the application.

## Documentation Technique

### Scenes
This application features two distinct scenes, accessible within the `Assets/Scenes` directory. The `Demo` scene serves as the primary environment for demonstrations, providing a comprehensive showcase of the application's capabilities. Additionally, the `Test` scene functions as a dedicated testing environment, specifically designed to facilitate a deeper understanding and exploration of NavMesh usage.

### Prefabs

- `Assets/Prefab/Player/Player.prefab` This is a cylinder that represents the player, controllable via mouse and keyboard.

### Scripts

#### CollisionDetection
Script used for detecting collisions between game objects. 

#### CSVExporter
Script used for exporting data to a CSV file.

#### Driver
Script used for managing communication with a microcontroller in the project. It's part of the hapticDriver namespace.

#### FPController
Script used for implementing a first-person character controller. It's attached to a game object, typically the player character, and controls the character's movement and camera look direction.

#### Metrics
Script used for tracking and managing various metrics in the project. It is designed to accurately measure the frequency of collisions and calculate the total time required to complete a given trajectory.

#### PathFinding 
Script used for implementing pathfinding functionality for game objects. 
- `target`: This public GameObject variable represents the target that the game object is trying to reach.
- `path`: This private NavMeshPath variable is used to store the path that the game object will follow to reach the target.
- `vm`: This public VibrationManager_minimal variable is serialized for editing in the Unity editor. It's used to manage vibrations for haptic feedback.

#### VibrationManager_minimal
Script used for managing vibrations in the game. 
- `vibIntensity`: This public byte array variable represents the intensity of the vibrations. It's an array of 4 bytes, each of which can range from 0 to 255.
- `directionValue`: This public float variable represents the direction of the vibrations

#### VibSetting 
Script used for managing the AnimationCurves, contolling vibration intensity. 

#### VibSettingsLoader
Script used for loading vibration settings, specifically by accessing a designated folder that contains all the vibration presets.


