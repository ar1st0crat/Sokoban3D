using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    private enum SceneState
    {
        Reset,
        Preload,
        Load,
        Unload,
        Postload,
        Ready,
        Run,
        Count
    }

    private static MainController _mainController;

    private string _nextSceneName;
    private string _currentSceneName;

    private AsyncOperation _resourceUnloadTask;
    private AsyncOperation _sceneLoadTask;
    private SceneState _sceneState;
    private delegate void UpdateDelegate();
    private UpdateDelegate[] _updateDelegates;

    //
    // called by other controllers:
    // MenuController, GameController
    //
    public static void SwitchScene(string nextSceneName)
    {
        if (_mainController != null)
        {
            if (_mainController._currentSceneName != nextSceneName)
            {
                _mainController._nextSceneName = nextSceneName;
                _mainController._sceneState = SceneState.Reset;
            }
        }
    }

    protected void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _mainController = this;

        _updateDelegates = new UpdateDelegate[(int)SceneState.Count];
        _updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
        _updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
        _updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
        _updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
        _updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
        _updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
        _updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

        SwitchScene("menu");
    }

    protected void OnDestroy()
    {
        if (_updateDelegates != null)
        {
            for (int i = 0; i < _updateDelegates.Length; i++)
            {
                _updateDelegates[i] = null;
            }
        }
        _updateDelegates = null;

        if (_mainController != null)
        {
            _mainController = null;
        }
    }

    protected void Update()
    {
        var ev = _updateDelegates[(int)_sceneState];
        if (ev != null)
        {
            _updateDelegates[(int)_sceneState]();
        }
    }

    private void UpdateSceneReset()
    {
        System.GC.Collect();
        _sceneState = SceneState.Preload;
    }

    private void UpdateScenePreload()
    {
        _sceneLoadTask = SceneManager.LoadSceneAsync(_nextSceneName);
        _sceneState = SceneState.Load;
    }

    private void UpdateSceneLoad()
    {
        if (_sceneLoadTask.isDone)
        {
            _sceneState = SceneState.Unload;
        }
        //else
        //update scene loading progress
    }

    private void UpdateSceneUnload()
    {
        if (_resourceUnloadTask == null)
        {
            _resourceUnloadTask = Resources.UnloadUnusedAssets();
        }
        else
        {
            if (!_resourceUnloadTask.isDone)
            {
                _resourceUnloadTask = null;
                _sceneState = SceneState.Postload;
            }
        }
    }

    private void UpdateScenePostload()
    {
        _currentSceneName = _nextSceneName;
        _sceneState = SceneState.Ready;
    }

    private void UpdateSceneReady()
    {
        System.GC.Collect();
        _sceneState = SceneState.Run;
    }

    private void UpdateSceneRun()
    {
        if (_currentSceneName != _nextSceneName)
        {
            _sceneState = SceneState.Reset;
        }
    }
}
