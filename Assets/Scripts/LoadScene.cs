using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public bool onTrigger = false;
    public string triggerSceneName;

    public void OnTriggerEnter(Collider col)
    {
        if(onTrigger) LoadSceneYeah(triggerSceneName);
    }
    public void LoadSceneYeah(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
