using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;
public class BossEyeTracker : MonoBehaviour
{

    private Vector2 _playerDistanceComparison;
    public static Vector3 Direction;

    
    void Update()
    {
        Vector3 playerPos = PlayerTracker.Instance.Player.transform.position;
            _playerDistanceComparison = playerPos;
            Direction = playerPos - transform.position;

            float angle = Mathf.Atan2(Direction.y, Direction.x);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg + 90));
    }
}
