using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneYeah(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
