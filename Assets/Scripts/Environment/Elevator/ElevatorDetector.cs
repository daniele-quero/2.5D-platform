using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDetector : MonoBehaviour
{
    [SerializeField]
    private int _coinCost;

    [SerializeField]
    private MeshRenderer _elevatorLed;

    [SerializeField]
    private ElevatorMovement _elevator;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            bool hasCoins;
            if (hasCoins = HasEnoughCoins(other.gameObject))
            {
                _elevatorLed.material.color = Color.green;
                if(Input.GetKey(KeyCode.E))
                {
                    UIManager.Instance.SetElevatorText("Elevator on its way...");
                    _elevator.CallElevator();
                }
            }

            UIManager.Instance.UpdateElevatorText(hasCoins);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _elevatorLed.material.color = Color.red;
        UIManager.Instance.SetElevatorText("");
    }

    private bool HasEnoughCoins(GameObject player)
    {
        return player.GetComponent<PlayerStats>().Coins >= _coinCost;
    }
}
