using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField, Range(2f,5f)]
    private float _gizmoLenght = 2f;

    [SerializeField]
    private Camera _cam;

    private Vector2 _mousePosition;

    private Vector2 _lookDir;

    private Vector2 _playerPositionOnScreen;

    private float _viewAngle;

    public Rigidbody2D rb;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_player.position, _gizmoLenght);
        Gizmos.DrawLine(_player.position, _mousePosition);
        Gizmos.DrawCube(_mousePosition, new Vector2(0.5f,0.5f));
    }

    private void Update()
    {
        _playerPositionOnScreen = _cam.WorldToViewportPoint(_player.position); //do i need this? or do I just use player.position

        _mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate()
    {
        FaceTheMouse(_mousePosition);
    }

    private void FaceTheMouse(Vector2 mousePos)
    {
        _lookDir = _mousePosition - (Vector2)transform.position;
        _viewAngle = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = _viewAngle;
        //_player.rotation = Quaternion.Euler(new Vector3(0f, 0f, _viewAngle));  
    }
}
