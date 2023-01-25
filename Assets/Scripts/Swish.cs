using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swish : MonoBehaviour
{
    private GameObject swish;
    // Start is called before the first frame update
    void Start()
    {
        swish.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void changeStatus()
    {
        swish.SetActive(true);

    }
}
