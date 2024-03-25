using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Place on level exit prefab to facilitate loading next level
public class LevelExit : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1;

    //Execute coroutine if player enters exit
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    //Load the next level in the build order and reset scene persist
    IEnumerator LoadNextLevel() 
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
