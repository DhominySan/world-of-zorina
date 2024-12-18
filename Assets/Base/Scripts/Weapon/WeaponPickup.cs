using TMPro;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    private Canvas worldSpaceCanva;
    private Transform TextObjet;
    private TextMeshPro itemText;

    public void Start(){
        worldSpaceCanva = FindObjectOfType<Canvas>();
        TextObjet = transform.Find("ItemText");
        TextObjet.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.CompareTag("Player")){
            TextObjet.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)){
            Debug.Log("GetWeapon");
            TextObjet.parent.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            TextObjet.gameObject.SetActive(false);
        }
    }
    
}
