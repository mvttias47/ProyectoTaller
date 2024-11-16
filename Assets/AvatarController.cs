using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public Animator avatarAnimator;
    public GameObject heart; // Objeto principal del corazón
    public GameObject lungs; // Objeto de los pulmones
    public TextMeshProUGUI botDialogo;

    private Renderer heartRenderer;
    private Renderer lungsRenderer;

    void Start()
    {
        // Obtener el Renderer del subobjeto "(null)" dentro de "Human Heart"
        heartRenderer = heart.transform.Find("(null)").GetComponent<Renderer>();
        lungsRenderer = lungs.GetComponentInChildren<Renderer>();

        // Verificar si los Renderers están asignados
        if (heartRenderer == null) Debug.LogError("El Renderer del corazón no se ha encontrado o no está asignado.");
        if (lungsRenderer == null) Debug.LogError("El Renderer de los pulmones no se ha encontrado o no está asignado.");

        // Configurar opacidad inicial en 0 (totalmente transparente) en todos los materiales del corazón y pulmones
        if (heartRenderer != null) SetAlpha(heartRenderer, 0f);
        if (lungsRenderer != null) SetAlpha(lungsRenderer, 0f);

        Debug.Log("Transparencia inicial aplicada.");
    }

    public void ShowWave()
    {
        avatarAnimator.SetTrigger("WaveTrigger");
        botDialogo.text = "¡Bienvenido! Mira tu reflejo... ahora observa cómo tu respiración forma los órganos.";

        // Iniciar el efecto de aparición gradual de los órganos
        StartCoroutine(AppearHeartFirst());
    }

    private IEnumerator AppearHeartFirst()
    {
        yield return StartCoroutine(FadeIn(heartRenderer));
        yield return StartCoroutine(FadeIn(lungsRenderer));
    }

    private IEnumerator FadeIn(Renderer organRenderer)
    {
        for (float alpha = 0f; alpha <= 1f; alpha += 0.02f)
        {
            SetAlpha(organRenderer, alpha);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void SetAlpha(Renderer renderer, float alpha)
    {
        // Itera sobre todos los materiales del Renderer y ajusta el Alpha
        foreach (Material material in renderer.materials)
        {
            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }
}