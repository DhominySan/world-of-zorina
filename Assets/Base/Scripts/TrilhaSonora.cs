using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrilhaSonora : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.Instance.PlayMusic("Level1");
    }
}
