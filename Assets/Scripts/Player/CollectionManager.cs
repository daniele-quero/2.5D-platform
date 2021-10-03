using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "coin":
                CoinCollection(other);
                break;
            default:
                break;
        }
    }

    private void CoinCollection(Collider other)
    {
        GetComponent<PlayerStats>().AddCoin((int)other.GetComponent<CollectibleStats>().value);
        DestroyCollectible(other.transform);        
    }

    private void DestroyCollectible(Transform t)
    {
        if (t.parent != null)
            GameObject.Destroy(t.parent.gameObject);

        else
            GameObject.Destroy(t.gameObject);
    }
}
