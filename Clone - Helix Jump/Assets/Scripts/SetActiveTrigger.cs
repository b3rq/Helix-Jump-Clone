using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            gameObject.SetActive(false);
            UIManager.Instance.SetScore(8);
        }
    }
}