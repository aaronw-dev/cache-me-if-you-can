using UnityEngine;
using UnityEngine.SceneManagement;
public class GlobalSceneManager : MonoBehaviour
{
    private static GlobalSceneManager global;

    void Awake()
    {
        /*
        if (global == null)
        {
            global = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}