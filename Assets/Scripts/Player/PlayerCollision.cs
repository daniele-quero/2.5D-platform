using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement _pm;
    void Start()
    {
        _pm = GetComponent<PlayerMovement>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.y == -1f)
        {
            Vector3 vel = _pm.Velocity;
            vel.y = -0.6f;
            vel.x *= 0.9f;
            _pm.Velocity = vel;
        }

        switch (hit.gameObject.tag)
        {
            case "jumpableWall":
                {
                    _pm.HitWallNormal = hit.normal;
                    _pm.CanWallJump = true;
                    break;
                }
            case "movable":
                {
                    _pm.PushMovables(hit);
                    break;
                }
            default:
                break;
        }

    }
}
