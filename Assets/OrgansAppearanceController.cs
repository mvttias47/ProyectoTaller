using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrgansAppearanceController : MonoBehaviour
{
    public GameObject heart; // Objeto principal del corazón
    public GameObject lungs; // Objeto principal de los pulmones

    private Renderer heartRenderer;
    private Renderer[] lungRenderers; // Array para almacenar los renderers significativos de los pulmones

    void Start()
    {
        // Obtener el Renderer del subobjeto "(null)" dentro del corazón
        Transform heartSubObject = heart.transform.Find("(null)");
        if (heartSubObject != null)
        {
            heartRenderer = heartSubObject.GetComponent<Renderer>();
        }

        // Filtrar y obtener solo los renderers específicos en los pulmones (rib1 a rib12, sternum y xiphoid)
        Renderer[] allLungRenderers = lungs.GetComponentsInChildren<Renderer>();
        lungRenderers = System.Array.FindAll(allLungRenderers, renderer =>
        {
            string name = renderer.gameObject.name;
            return (name.StartsWith("rib") && int.TryParse(name.Substring(3), out int ribNumber) && ribNumber >= 1 && ribNumber <= 12)
                   || name == "sternum" 
                   || name == "xiphoid";
        });

        // Configurar opacidad inicial en 0 (totalmente transparente) en todos los materiales del corazón y pulmones
        if (heartRenderer != null) SetAlpha(heartRenderer, 0f);
        foreach (Renderer renderer in lungRenderers)
        {
            SetAlpha(renderer, 0f);
        }
    }

    // Método que se llamará cuando se presione el botón de saludo
    public void ShowOrgans()
    {
        StartCoroutine(AppearHeartThenLungs());
    }

    private IEnumerator AppearHeartThenLungs()
    {
        // Aparece el corazón primero
        yield return StartCoroutine(FadeIn(heartRenderer));
        
        // Luego aparece cada uno de los pulmones al mismo tiempo
        yield return StartCoroutine(FadeInAllLungs());
    }

    private IEnumerator FadeIn(Renderer organRenderer)
    {
        for (float alpha = 0f; alpha <= 1f; alpha += 0.02f)
        {
            SetAlpha(organRenderer, alpha);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator FadeInAllLungs()
    {
        // Aparecer gradualmente todos los pulmones al mismo tiempo
        for (float alpha = 0f; alpha <= 1f; alpha += 0.02f)
        {
            foreach (Renderer renderer in lungRenderers)
            {
                SetAlpha(renderer, alpha);
            }
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