using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    public event EventHandler OnLoad;

    public static SaveHandler instance;
    [SerializeField] private GameObject unitGameObject;
    private IUnit unit;

    private void Awake()
    {
        unit = unitGameObject.GetComponent<IUnit>();
        SaveSystem.Init();
    }
    
    public void SaveProcess()
    {
        Save();
    }
    public void LoadProcess()
    {
        Load();
        OnLoad?.Invoke(this, EventArgs.Empty);
    }

    private void Save()
    {
        Vector3 playerPosition = unit.GetPosition();
        CheckPoint lastCheckPoint = unit.GetLastCheckPoint();

        SaveObject saveObject = new SaveObject
        {
            playerPosition = playerPosition,
            checkPoint = lastCheckPoint
    };
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);

        Debug.Log("Saved");
    }

    private void Load()
    {
        // Load
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            Debug.Log("Loaded: " + saveString);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            unit.SetPosition(saveObject.playerPosition);
            unit.SetCheckPoint(saveObject.checkPoint);
        }
        else
        {
            Debug.LogError("No save");
        }
    }


    private class SaveObject
    {
        public Vector3 playerPosition;
        public CheckPoint checkPoint;
    }
}