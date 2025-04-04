using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveExperience : MonoBehaviour
{
    public GameObject boxPrefab;
    public List<GameObject> spawnedBoxes = new List<GameObject>();


    // When the experience starts, it should run the LoadScene method
    void Start()
    {
        LoadScene();
    }

    public void SpawnedBox(Vector3 position)
    {
        GameObject newBox = Instantiate(boxPrefab, position, Quaternion.identity);
        spawnedBoxes.Add(newBox);
    }


    // Method to save the scene's current state
    public void SaveScene()
    {
        PlayerPrefs.SetInt("BoxCount", spawnedBoxes.Count);
        for (int i = 0; i < spawnedBoxes.Count; i++)
        {
            PlayerPrefs.SetFloat ("BoxX"+ i, spawnedBoxes[i].transform.position.x);
            PlayerPrefs.SetFloat ("BoxY"+ i, spawnedBoxes[i].transform.position.y);
            PlayerPrefs.SetFloat ("BoxZ"+ i, spawnedBoxes[i].transform.position.z);
        }

        PlayerPrefs.Save();
        Debug.Log("Scene Saved!");
    }


    // Method to load the scene's current state
    void LoadScene()
    {
        if (PlayerPrefs.HasKey("BoxCount"))
        {
            int boxCount = PlayerPrefs.GetInt("BoxCount");

            for (int i = 0; i < boxCount; i++)
            {
                float x = PlayerPrefs.GetFloat ("BoxX" + i);
                float y = PlayerPrefs.GetFloat ("BoxY" + i);
                float z = PlayerPrefs.GetFloat ("BoxZ" + i);
                SpawnedBox (new Vector3(x, y, z));
            }

            Debug.Log("Scene Loaded!");
        }
    }

    // Method to load the next scene on button press
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1) // Prevents out-of-range error
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }


    // Method to load the previous scene on button press
     public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex > 0) // Prevents loading before the first scene
        {
            SceneManager.LoadScene(currentSceneIndex - 1);
        }
    }
}
