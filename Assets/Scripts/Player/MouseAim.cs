using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField, Range(2f,5f)]
    private float _gizmoLenght = 2f;

    private Vector2 _mousePosition;

    private Vector2 _lookDir;

    private Vector2 _playerPositionOnScreen;

    private float _viewAngle;
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_player.position, _gizmoLenght);
        Gizmos.DrawLine(_player.position, _mousePosition);
        Gizmos.DrawCube(_mousePosition, new Vector2(0.5f,0.5f));
    }

    private void Update()
    {
        _playerPositionOnScreen = Camera.main.WorldToViewportPoint(_player.position);

        FaceTheMouse();
    }

    private void FaceTheMouse()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lookDir = _mousePosition - (Vector2)transform.position;
        _viewAngle = Mathf.Atan2(_mousePosition.y, _mousePosition.x) * Mathf.Rad2Deg - 90;
        _player.rotation = Quaternion.Euler(new Vector3(0f, 0f, _viewAngle));
    }
}
