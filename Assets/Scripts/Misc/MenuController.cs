using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync("level");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
