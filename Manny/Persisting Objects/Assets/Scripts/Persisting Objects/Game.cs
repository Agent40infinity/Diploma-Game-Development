﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : PersistableObject
{
    public KeyCode createkey = KeyCode.C;
    public KeyCode destroyKey = KeyCode.X;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;

    const int saveVersion = 3;
    float creationProgress, destructionProgress;

    [SerializeField] ShapeFactory shapeFactory;
    List<Shape> shapes;
    public PersistentStorage storage;

    public float CreationSpeed { get; set; }
    public float DestructionSpeed { get; set; }

    Random.State mainRandomState;
    [SerializeField] bool reseedOnLoad;

    public void Awake()
    {
        shapes = new List<Shape>();
    }

    public void Start()
    {
        BeginNewGame();
        mainRandomState = Random.state;
        //StartCoroutine(LoadLevel(1));
    }

    public void Update()
    {
        if (Input.GetKeyDown(createkey))
        {
            CreateShape();
        }
        else if (Input.GetKeyDown(destroyKey))
        {
            DestroyShape();
        }
        else if (Input.GetKey(newGameKey))
        {
            BeginNewGame();
        }
        else if (Input.GetKeyDown(saveKey))
        {
            storage.Save(this, saveVersion);
        }
        else if (Input.GetKeyDown(loadKey))
        {
            BeginNewGame();
            storage.Load(this);
        }

        creationProgress += Time.deltaTime * CreationSpeed;
        while (creationProgress >= 1f)
        {
            creationProgress -= 1f;
            CreateShape();
        }

        destructionProgress += Time.deltaTime * DestructionSpeed;
        while (destructionProgress >= 1f)
        {
            destructionProgress -= 1f;
            DestroyShape();
        }
    }

    public void CreateShape()
    {
        Shape instance = shapeFactory.GetRandom();
        Transform t = instance.transform;
        t.localPosition = GameLevel.Current.SpawnPoint;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        instance.SetColor(Random.ColorHSV(hueMin: 0f, hueMax: 1f, saturationMin:0.5f, saturationMax:1f, valueMin:0.25f, valueMax:1f, alphaMin:1f, alphaMax:1f));
        shapes.Add(instance);
    }

    public void DestroyShape()
    {
        if (shapes.Count > 0)
        {
            int index = Random.Range(0, shapes.Count);
            Destroy(shapes[index].gameObject);
            int lastIndex = shapes.Count - 1;
            shapes[index] = shapes[lastIndex];
            shapes.RemoveAt(lastIndex);
        }
    }

    public void BeginNewGame()
    {
        Random.state = mainRandomState;
        int seed = Random.Range(0, int.MaxValue) ^ (int)Time.unscaledTime;
        mainRandomState = Random.state;
        Random.InitState(seed);

        for (int i = 0; i < shapes.Count; i++)
        {
            Destroy(shapes[i].gameObject);
        }
        shapes.Clear();
    }

    public override void Save(GameDataWriter writer)
    {
        writer.Write(shapes.Count);
        writer.Write(Random.state);
        GameLevel.Current.Save(writer);
        for (int i = 0; i < shapes.Count; i++)
        {
            writer.Write(shapes[i].ShapeId);
            writer.Write(shapes[i].MaterialId);
            shapes[i].Save(writer);
        }
    }

    public override void Load(GameDataReader reader)
    {
        int version = reader.Version;
        if (version > saveVersion)
        {
            Debug.LogError("Unsupported future save version " + version);
            return;
        }
        StartCoroutine(LoadGame(reader));
    }

    IEnumerator LoadGame (GameDataReader reader)
    {
        int version = reader.Version;
        int count = version <= 0 ? -version : reader.ReadInt();
        if (version >= 3)
        {
            Random.State state = reader.ReadRandomState();
            if (!reseedOnLoad)
            {
                Random.state = state;
            }
        }

        yield return LoadLevel(version < 2 ? 1 : reader.ReadInt());
        if (version >= 3)
        {
            GameLevel.Current.Load(reader);
        }

        for (int i = 0; i < count; i++)
        {
            int shapeId = version > 0 ? reader.ReadInt() : 0;
            int materialId = version > 0 ? reader.ReadInt() : 0;
            Shape instance = shapeFactory.Get(shapeId, materialId);
            instance.Load(reader);
            shapes.Add(instance);
        }
    }
}
