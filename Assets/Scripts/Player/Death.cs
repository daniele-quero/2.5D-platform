using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public void DeathLogic()
    {
        GetComponent<PlayerStats>().AddLife(-1);
        GetComponent<PlayerAnimation>().SetDeathAnimationParameters();
        UIManager.Instance.StartCoroutine("DeathDisplayRoutine");
        GetComponentInParent<PlayerMovement>().Velocity = Vector3.zero;
    }

    private IEnumerator SlowRespawnRoutine()
    {
        PlayerMovement pm = GetComponentInParent<PlayerMovement>();
        float speed = Vector3.Distance(transform.position, pm.StartingPosition) / 1.5f;
        yield return new WaitForSeconds(1.8f);
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        while (transform.position != pm.StartingPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, pm.StartingPosition, speed * 0.02f);
            yield return wait;
        }
    }
}
