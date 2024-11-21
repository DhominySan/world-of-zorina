using UnityEngine;

public class Combo : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] comboSounds;

    private int currentCombo = 0;
    private bool canReceiveInput = true;
    private bool isAttacking = false;
    private bool inputQueued = false;

    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int Attack1 = Animator.StringToHash("Attack1");
    private static readonly int Attack2 = Animator.StringToHash("Attack2");
    private static readonly int Attack3 = Animator.StringToHash("Attack3");

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
        
        animator.SetBool(IsAttacking, true);

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
                break;
            case 2:
                animator.SetTrigger(Attack2);
                Debug.Log("Combo 2 triggered");
                break;
            case 3:
                animator.SetTrigger(Attack3);
                Debug.Log("Combo 3 triggered");
                break;
        }

        if (comboSounds.Length >= currentCombo && comboSounds[currentCombo - 1] != null)
        {
            audioSource.PlayOneShot(comboSounds[currentCombo - 1]);
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
            animator.SetBool(IsAttacking, false);
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
        animator.SetBool(IsAttacking, false);
    }
}
