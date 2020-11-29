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

/// <summary>Used to convert a SceneManifest to a string.</summary>
public class SceneManifestTranslator
{
    static Dictionary<SceneManifest, string> sceneNames;

    static SceneManifestTranslator()
    {
        sceneNames = new Dictionary<SceneManifest, string>
        {
            // To add a level whose filename is `my_level.unity`, next
            // add a line below that looks like this.
            //
            // {SceneManifest.MyLevel, "my_level"},
            // 
            {SceneManifest.TestLevel, "testLevel"},
            {SceneManifest.Title, "Title"},
            {SceneManifest.Credits, "Credits"},
            {SceneManifest.DummyScene0, "DummyScene0"},
            {SceneManifest.DummyScene1, "DummyScene1"},
            {SceneManifest.SceneChanger, "SceneChanger"},
        };
    }

    /// <summary>
    /// Takes a SceneManifest and returns that scene's name
    /// </summary>
    /// <param name="scene">
    /// The SceneManifest that you're interested in converting to a 
    /// string.
    /// </param>
    /// <returns>
    /// A string with the name of the scene, useful to Unity.
    /// </returns>
    public static string Translate(SceneManifest scene)
    {
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
