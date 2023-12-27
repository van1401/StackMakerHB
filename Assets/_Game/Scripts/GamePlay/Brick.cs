using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    [SerializeField] bool isCollect = false;
    [SerializeField] GameObject brick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isCollect)
        {
            isCollect = true;
            brick.SetActive(false);
            other.GetComponent<Player>().AddBrick();
        }
    }
}
