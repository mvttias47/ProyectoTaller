using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingController : MonoBehaviour
{
    public Animator heartAnimator;       // Animator del corazón
    public Animator lungsAnimator;       // Animator de los pulmones
    public Animator silhouetteAnimator;  // Animator de la silueta
    public AudioClip breathingFast;      // Audio de respiración agitada
    public AudioClip breathingCalm;      // Audio de respiración calmada
    public AudioClip advertenciaClip;    // Audio de advertencia
    public Button saludoButton;          // Botón de saludo
    public Button profundaButton;        // Botón de respiración profunda
    public Button agitadaButton;         // Botón de respiración agitada

    private AudioSource audioSource;     
    private bool saludoPresionado = false; 

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

       
        profundaButton.onClick.AddListener(() => IntentarRespiracion("profunda"));
        agitadaButton.onClick.AddListener(() => IntentarRespiracion("agitada"));
        
        
        saludoButton.onClick.AddListener(SaludoButtonPressed);
    }

   
    public void SaludoButtonPressed()
    {
        saludoPresionado = true;
        Debug.Log("Botón de saludo presionado, ahora se pueden usar los botones de respiración.");

       
        FindObjectOfType<OrgansAppearanceController>().ShowOrgans();
    }

    
    private void IntentarRespiracion(string tipo)
    {
        if (!saludoPresionado)
        {
            PlayAdvertencia(); 
            return;
        }

       
        if (tipo == "profunda")
        {
            CalmBreathing();
        }
        else if (tipo == "agitada")
        {
            FastBreathing();
        }
    }

    
    public void CalmBreathing()
    {
        heartAnimator.SetTrigger("CalmBreathingTrigger");
        lungsAnimator.SetTrigger("CalmBreathingTrigger");

        if (silhouetteAnimator != null)
        {
            silhouetteAnimator.SetTrigger("RelaxedBreathingTrigger");
        }
        else
        {
            Debug.LogError("El Animator de la silueta no está asignado en el BreathingController.");
        }

        PlayAudio(breathingCalm);
    }

  
    public void FastBreathing()
    {
        heartAnimator.SetTrigger("FastBreathingTrigger");
        lungsAnimator.SetTrigger("FastBreathingTrigger");

        PlayAudio(breathingFast);
    }

    
    private void PlayAudio(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        
        audioSource.clip = clip;
        audioSource.Play();
    }

   
    private void PlayAdvertencia()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = advertenciaClip;
            audioSource.Play();
            Debug.Log("Reproduciendo advertencia: Presiona el botón de saludo primero.");
        }
    }
}