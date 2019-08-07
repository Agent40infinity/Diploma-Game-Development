using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;
public class TestSuite
{
    private GameManager gameManager;
    private Player player;
    private GameObject game;

    [SetUp]
    public void Setup()
    {
        //Loading and Creating Game
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(prefab);

        //Get GameManager
        gameManager = game.GetComponent<GameManager>();
        //player = Object.FindObjectOfType<Player>();
        player = game.GetComponentInChildren<Player>();
    }

    [UnityTest]
    public IEnumerator GamePrefabLoaded()
    {
        yield return new WaitForEndOfFrame();
        Assert.NotNull(game, "Fuck you");
    }

    [UnityTest]
    public IEnumerator PlayerExists()
    {
        yield return new WaitForEndOfFrame();
        Assert.NotNull(player, "Fuck the Player");

    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game);
    }
}
