using System.Collections;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private static PauseGame instance;
    private bool isPaused = false;
    private bool canPause = true; // Variável de controle para o intervalo de pausa
    public GameObject player; // Referência ao objeto Player
    public GameObject pausePanel; // Referência ao painel de pausa

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Verifica se a tecla Esc foi pressionada e se o intervalo de pausa foi respeitado
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        // Verifica se o diálogo está ativo
        if (DialogueManager.Instance.isDialogueActive)
        {
            return; // Não permite pausar se o diálogo estiver ativo
        }

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // Pausa ou retoma o jogo

        // Ativa ou desativa o painel de pausa
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused); // Ativa o painel se estiver pausado
        }

        // Desabilita ou habilita scripts do Player
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        CharacterController2D characterController = player.GetComponent<CharacterController2D>();
        Attack attack = player.GetComponent<Attack>();

        if (playerMovement != null)
        {
            playerMovement.enabled = !isPaused; // Desabilita o script se estiver pausado
        }
        if (characterController != null)
        {
            characterController.enabled = !isPaused; // Desabilita o script se estiver pausado
        }
        if (attack != null)
        {
            attack.enabled = !isPaused; // Desabilita o script se estiver pausado
        }

        // Inicia o intervalo de 2 segundos antes de permitir outra pausa
        StartCoroutine(PauseCooldown());
    }

    private IEnumerator PauseCooldown()
    {
        canPause = false; // Bloqueia a possibilidade de pausar novamente
        yield return new WaitForSecondsRealtime(1); // Aguarda 2 segundos em tempo real
        canPause = true; // Libera a pausa novamente
    }

    public static bool IsGamePaused()
    {
        return instance.isPaused; // Método para verificar se o jogo está pausado
    }
}
