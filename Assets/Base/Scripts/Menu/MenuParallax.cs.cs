using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;

    private Vector2 startPosition;
    private Vector3 velocity;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        
        // Limitar o alcance do offset
        offset.x = Mathf.Clamp(offset.x, -1f, 1f); // Limita o movimento horizontal
        offset.y = Mathf.Clamp(offset.y, -1f, 1f); // Limita o movimento vertical

        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
    }
}
