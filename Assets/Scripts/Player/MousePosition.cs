using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    #region Variables
    [SerializeField] private Camera _mainCam;
    [HideInInspector] public Vector3 _mouseWorldPosition;
    #endregion
    #region Properties
    public Vector3 MouseWorldPosition { get => _mouseWorldPosition; private set => _mouseWorldPosition = value; }
    #endregion

    private void Awake()
    {
        Setup();
    }

    private void OnValidate()
    {
        Setup();
    }

    private void Setup()
    {
        if (_mainCam == null)
            _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        TrackMousePosition();
    }

    private void TrackMousePosition()
    {
        MouseWorldPosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        _mouseWorldPosition.z = 0f;
    }

    public Vector3 CurrentMousePosition()
    {
        return MouseWorldPosition;
    }
}
