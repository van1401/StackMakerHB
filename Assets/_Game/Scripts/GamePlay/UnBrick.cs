using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    [SerializeField] bool isCollect = false;
    [SerializeField] GameObject brick;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollect)
        {
            isCollect = true;
            brick.SetActive(true);
            other.GetComponent<Player>().RemoveBrick();
        }
    }
}
