using UnityEngine;
using Zenject;

public class Box : MonoBehaviour
{
    BoxStateFactory _stateFactory;
    BoxState _state;

    [Inject]
    public void Construct(BoxStateFactory stateFactory)
    {
        _stateFactory = stateFactory;
    }


    #region Position of the box in the grid

    public byte Row { get; set; }
    public byte Column { get; set; }

    #endregion


    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public Quaternion Rotation
    {
        get { return transform.rotation; }
        set { transform.rotation = value; }
    }

    public void Start()
    {
        //ChangeState(BoxStates.NotMoving);
    }

    public void Update()
    {
        if (_state != null)
        {
            _state.Update();
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        _state.OnTriggerEnter(collider);
    }

    public void ChangeState(BoxStates state)
    {
        if (_state != null)
        {
            _state.Dispose();
            _state = null;
        }

        _state = _stateFactory.CreateState(state);
        _state.Start();
    }

    public class Factory : Factory<Box>
    {
    }
}
