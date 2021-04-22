﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    //public string Level1,Lobby;

    [SerializeField] string[] Levels;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {

        UnlockingLobbyNFirstLevel();
        //Locking other levels
        //for(int i=2;i<Levels.Length;i++)
        //    SetLevelStatus(Levels[i], LevelStatus.Locked);
    }

    public void UnlockingLobbyNFirstLevel()
    {
        //Unlocking Lobby
        if (GetLevelStatus(Levels[0]) == LevelStatus.Locked)
        {
            SetLevelStatus(Levels[0], LevelStatus.Unlocked);
        }
        //Unlocking Level 1
        if (GetLevelStatus(Levels[1]) == LevelStatus.Locked)
        {
            SetLevelStatus(Levels[1], LevelStatus.Unlocked);
        }
    }

    public void MarkCurrentLevelComplete()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //set level status to complete
        SetLevelStatus(currentScene.name, LevelStatus.Completed);
        //unlock next level
        //int nextSceneIndex = scene.buildIndex + 1;
        //Scene nextScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex);
        //SetLevelStatus(nextScene.name, LevelStatus.Unlocked);
        //Debug.Log(nextScene);

        int currentSceneIndex = Array.FindIndex(Levels, level => level == currentScene.name);
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex < Levels.Length)
        {
            SetLevelStatus(Levels[nextSceneIndex], LevelStatus.Unlocked);
        }
    }

    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus levelStatus= (LevelStatus) PlayerPrefs.GetInt(level,0);
        return levelStatus;
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
        //Debug.Log("Setting Level: "+ level + " Status: "+levelStatus);
    }
}
