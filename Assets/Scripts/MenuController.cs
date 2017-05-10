using UnityEngine;

public class MenuController : MonoBehaviour
{
    private static MenuController _menuController;

    protected void Awake()
    {
        _menuController = this;
    }

    protected void OnDestroy()
    {
        if (_menuController != null)
        {
            _menuController = null;
        }
    }
    
    public void Play()
    {
        MainController.SwitchScene("level");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
