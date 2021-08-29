using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void Update()
    {
        transform.Translate(-1 * Time.deltaTime, 0, 0);
    }
}
