using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.TestTools;
using NUnit.Framework;
public class TestSuite
{
    public GameObject game;
    public GameObject[] swords;
    public Text referenceUI;

    [SetUp]
    public void SetUp()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(prefab);

        swords = GameObject.FindGameObjectsWithTag("Swords");

        referenceUI = GameObject.Find("SwordNameText").GetComponent<Text>();
    }

    [UnityTest]
    public IEnumerator GamePrefabLoaded()
    {
        yield return new WaitForEndOfFrame();
        Assert.NotNull(game, "Game exists");
    }

    [UnityTest]
    public IEnumerator SwordsLoaded()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < swords.Length; i++)
        {
            Debug.Log(swords[i]);
            Assert.NotNull(swords[i], swords[i].name);
        }
    }

    [UnityTest]
    public IEnumerator HoverOverItem()
    {
        for (int i = 0; i < swords.Length; i++)
        {
            swords[i].GetComponent<Sword>().onMouseDown.Invoke();
            bool matches;
            matches = swords[i].name.Contains(referenceUI.text);
            Debug.Log(matches + " | Reference Text: " +referenceUI.text + " | sword name: " + swords[i].name);
            Assert.IsTrue(matches, swords[i] + " Matches the display name on UI");
        }
        yield return new WaitForEndOfFrame();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game);
    }
}
