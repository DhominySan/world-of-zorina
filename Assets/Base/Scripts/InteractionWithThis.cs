using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithThis : MonoBehaviour
{
 [SerializeField] private List<GameObject> objectsToEnable;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (var obj in objectsToEnable)
            {
                obj.SetActive(true);
            }
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (var obj in objectsToEnable)
            {
                obj.SetActive(false);
            }
        }
    }
}