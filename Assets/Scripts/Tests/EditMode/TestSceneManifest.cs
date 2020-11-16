using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;


namespace Tests
{
    public class TestSceneManifest
    {
        [Test]
        public void AllManifestValuesInTranslator()
        {
            Type manifestType = typeof(SceneManifest);
            Array manifestScenes = manifestType.GetEnumValues();

            foreach (SceneManifest scene in manifestScenes)
            {
                Assert.That(
                    () => {
                        SceneManifestTranslator.Translate(scene);
                    },
                            Throws.Nothing,
                            "There are scenes in the SceneManifest that "
                            + "haven't been added to the translation.");
            }
        }
    }
}
