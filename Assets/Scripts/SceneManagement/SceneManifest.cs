using System.Collections.Generic;


public enum SceneManifest
{
    // To add a level whose filename is `my_level.unity`, first add
    // a line that looks like the following (name changed obviously).
    // Then read the instructions below
    // `public class SceneManifestTranslator`
    // 
    // MyLevel,
    // 
    TestLevel,
    Title,
    Credits,
    DummyLevel,
}

public class SceneManifestTranslator
{
    public static string Translate(SceneManifest scene)
    {
        var sceneNames = new Dictionary<SceneManifest, string>();
        // To add a level whose filename is `my_level.unity`, next add
        // a line below that looks like this.
        //
        // sceneNames.Add(SceneManifest.MyLevel, "my_level");
        // 
        sceneNames.Add(SceneManifest.TestLevel, "testLevel");
        sceneNames.Add(SceneManifest.Title, "Title");
        sceneNames.Add(SceneManifest.Credits, "Credits");
        sceneNames.Add(SceneManifest.DummyLevel, "DummyLevel");

        // Add the line at least above here
        try 
        {
            return sceneNames[scene];
        }
        catch (KeyNotFoundException) 
        {
            throw new KeyNotFoundException($"Scene {scene} does not "
                                           + "exist to translate.");
        }
    }
}
