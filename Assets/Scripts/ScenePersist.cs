using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton pattern script placed on scene persitant prefab to keep it from being destroyed
public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        //makes sure to keep the same game object, but when a new game starts it destroys old one
        if(numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist() 
    {
        Destroy(gameObject);
    }
}
