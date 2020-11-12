# SceneManagement

The **SceneChanger** class allows any GameObject to change to any scene at any time with little overhead.  It implements a method `bool ChangeToScene(SceneManifest scene)` that will switch to a new scene and return `true` if it was successful, and another method `bool IsLoaded()` that returns `false` after a scene is sent to be loaded but before its loading has finished, and `true` after that.

**SceneManifest** is an enum located in `/Assets/Scenes/SceneManifest.cs` that gives a code-referencable name to all scenes.  When a designer wants to add a scene to the game, they need to make sure to also add it to `SceneManifest.cs`.  A good future thing to do with this is have all entries in the scene manifest added to the Build Settings at compile time, which is possible since C# has type introspection.
