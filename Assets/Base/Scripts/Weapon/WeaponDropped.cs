using TMPro;
using UnityEditor.U2D;
using UnityEngine;

public class WeaponDropped : MonoBehaviour
{

    private Canvas worldSpaceCanvas;
    private Transform textObject;
    private TextMeshPro itemTextMesh;
    [SerializeField] private WeaponData weaponData;
    private SpriteRenderer droppedWeaponSprite;

    public void Start()
    {
        worldSpaceCanvas = FindObjectOfType<Canvas>();
        textObject = transform.Find("ItemText");
        textObject.gameObject.SetActive(false);

        droppedWeaponSprite = GetComponent<SpriteRenderer>();
        droppedWeaponSprite.sprite = weaponData.weaponSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            textObject.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Weapon: " + weaponData.name);
            GameObject droppedWeaponObject = textObject.transform.parent.gameObject;
            Destroy(droppedWeaponObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textObject.gameObject.SetActive(false);
        }
    }

}
