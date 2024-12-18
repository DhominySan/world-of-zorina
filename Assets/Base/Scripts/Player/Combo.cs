using UnityEngine;

public class Combo : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] comboSounds;
    [SerializeField] private SoundManager soundManager;

    private int currentCombo = 0;
    private bool canReceiveInput = true;
    private bool isAttacking = false;
    private bool inputQueued = false;

    private static readonly int IsComboActive = Animator.StringToHash("IsComboActive");
    private static readonly int Attack1 = Animator.StringToHash("Attack1");
    private static readonly int Attack2 = Animator.StringToHash("Attack2");
    private static readonly int Attack3 = Animator.StringToHash("Attack3");

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.O)) return;
        
        if (canReceiveInput)
        {
            if (isAttacking)
            {
                if (currentCombo < 3)
                {
                    inputQueued = true;
                    currentCombo++;
                    TriggerAttack();
                }
            }
            else
            {
                currentCombo = 1;
                TriggerAttack();
            }
        }
    }

    private void TriggerAttack()
    {
        canReceiveInput = false;
        isAttacking = true;
        
        animator.SetBool(IsComboActive, true);

        // Reset all triggers first
        animator.ResetTrigger(Attack1);
        animator.ResetTrigger(Attack2);
        animator.ResetTrigger(Attack3);

        // Then set the new trigger
        switch (currentCombo)
        {
            case 1:
                animator.SetTrigger(Attack1);
                Debug.Log("Combo 1 triggered");
                soundManager.PlaySound2D("ComboSound1");
                break;
            case 2:
                animator.SetTrigger(Attack2);
                Debug.Log("Combo 2 triggered");
                soundManager.PlaySound2D("ComboSound2");
                break;
            case 3:
                animator.SetTrigger(Attack3);
                Debug.Log("Combo 3 triggered");
                soundManager.PlaySound2D("ComboSound3");
                break;
        }
    }

    // Called by animation events
    public void EnableComboInput()
    {
        canReceiveInput = true;
        if (inputQueued)
        {
            inputQueued = false;
        }
    }

    // Called by animation events at the end of attack animations
    public void OnAttackComplete()
    {
        if (!inputQueued)
        {
            isAttacking = false;
            animator.SetBool(IsComboActive, false);
            ResetCombo();
        }
    }

    private void ResetCombo()
    {
        Debug.Log("Resetting combo: " + currentCombo);
        currentCombo = 0;
        isAttacking = false;
        canReceiveInput = true;
        inputQueued = false;
        animator.SetBool(IsComboActive, false);
    }

    private void Passos()
    {
        soundManager.PlaySound2D("PassosSound");
    }
}
