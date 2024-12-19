using TMPro;
using UnityEditor.U2D;
using UnityEngine;

public class WeaponDropped : MonoBehaviour
{

    [SerializeField] private Canvas worldSpaceCanvas;
    [SerializeField] private Transform textObject;
    [SerializeField] private WeaponData weaponData;
    private SpriteRenderer droppedWeaponSprite;
    private GameObject player;

    private bool canBePickedUp;

    private void Awake() {
        droppedWeaponSprite = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        textObject.gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player");
        droppedWeaponSprite.sprite = weaponData.weaponSprite;
    }

    private void Update() {
        if(canBePickedUp && Input.GetKeyDown(KeyCode.E)){
            Debug.Log("Weapon: " + weaponData.name);
            WeaponManager weaponManager = player.GetComponentInChildren<WeaponManager>();
            weaponManager.WeaponPickup(weaponData);
            GameObject droppedWeaponObject = textObject.transform.parent.gameObject;
            Destroy(droppedWeaponObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            textObject.gameObject.SetActive(true);
            canBePickedUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textObject.gameObject.SetActive(false);
            canBePickedUp = false;
        }
    }

}
