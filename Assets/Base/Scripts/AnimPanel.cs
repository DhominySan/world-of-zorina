using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPanel : MonoBehaviour
{
 public Vector3 targetScale = new Vector3(1, 1, 1);
    public float animationDuration = 1.0f;
    private bool isAnimating = false;
    private bool isClosing = false;
    private Vector3 initialScale;
    private float elapsedTime = 0f;
    private Vector3 originalTargetScale;

    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        initialScale = transform.localScale;
        originalTargetScale = targetScale;
        OpenAnimation(targetScale, animationDuration);
    }

    void Update()
    {
        if (isAnimating)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);



            if (t >= 1.0f)
            {
                isAnimating = false;

                if (isClosing){
                    gameObject.SetActive(false);
                    isClosing = false;
                    targetScale = originalTargetScale;
                }
            }
        }
    }

    public void OpenAnimation(Vector3 newTargetScale, float duration)
    {
        targetScale = newTargetScale;
        animationDuration = duration;
        initialScale = transform.localScale;
        elapsedTime = 0f;
        isAnimating = true;
    }

    public void CloseAnimation(Vector3 newInitialScale, float duration)
    {
        initialScale = transform.localScale;
        targetScale = newInitialScale; 
        animationDuration = duration;
        elapsedTime = 0f;
        isAnimating = true;
        isClosing = true;
    }
void OnDisable()
{
    transform.localScale = new Vector3(0.13f, 0.13f, 0.13f); // Define a escala para 0.13 ao desativar
}
    public void OnClickCloseAnimation(){
        CloseAnimation(initialScale, animationDuration);
    }
}
