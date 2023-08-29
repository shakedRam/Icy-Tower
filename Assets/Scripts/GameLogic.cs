using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject groundPrefab;
    private Rigidbody rb;
    
    private float yPosPlatform;
    private float xPosPlatform;
    private int numOfPlatformsStart;
    private int numOfPlatformsPassed;
    private int platformsForWin;
    private float yPosWin;
    
    private void Awake()
    {
        yPosWin = 84f;
        platformsForWin = 50;
        numOfPlatformsPassed = 0;
        yPosPlatform = -3.25f;
        numOfPlatformsStart = 7;
        for (int i = 0; i < numOfPlatformsStart; i++)
        {
            Instantiate(platformPrefab, new Vector3(Random.Range(-6f, 6f), yPosPlatform, 0f), Quaternion.identity);
            yPosPlatform += 1.5f;
        }
        
    }

    void Update()
    {
        Vector3 characterViewportPos = UnityEngine.Camera.main.WorldToViewportPoint(character.position);
        if (characterViewportPos.y < 0)
        {
            // Character is below the camera's view, trigger "lose" condition.
            Lose();
        }

        if (DestroyPlatformsBelow())
        {
            if (numOfPlatformsPassed > platformsForWin)
            {
                Instantiate(groundPrefab, new Vector3(0f, yPosPlatform, 0f), Quaternion.identity);
            }
            else
            {
                Instantiate(platformPrefab, new Vector3(Random.Range(-6f, 6f), yPosPlatform, 0f), Quaternion.identity);
                yPosPlatform += 1.5f;
            }
        }

        if (character.position.y > yPosWin)
        {
            Debug.Log("Win");
            PlayerPrefs.SetString("winOrLose", "Winner!");
            SceneManager.LoadScene("EndGameScene");
        }
    }

    private void Lose()
    {
        Debug.Log("Lose");
        PlayerPrefs.SetString("winOrLose", "Loser :(");
        SceneManager.LoadScene("EndGameScene");
    }

    private bool DestroyPlatformsBelow()
    {
        bool wasDestroyed = false;
        // Find all GameObjects with the "Platform" tag in the scene.
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        float treshhold = character.transform.position.y - 6f;
        
        foreach (var platform in platforms)
        {
            if (platform.transform.position.y < treshhold)
            {
                Destroy(platform);
                numOfPlatformsPassed++;
                wasDestroyed = true;
            }
        }

        return wasDestroyed;
    }
}
