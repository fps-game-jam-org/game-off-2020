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
    DummyScene0,
    DummyScene1,
    SceneChanger,
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
        sceneNames.Add(SceneManifest.DummyScene0, "DummyScene0");
        sceneNames.Add(SceneManifest.DummyScene1, "DummyScene1");
        sceneNames.Add(SceneManifest.SceneChanger, "SceneChanger");

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
