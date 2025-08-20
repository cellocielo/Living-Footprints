using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TextFadeParticleSystems : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] messages;
    public float fadeDuration = 1f;
    public float displayTime = 2f;

    [Header("Particle System A")]
    public ParticleSystem particlesA;
    public int startLineA = 0;
    public int endLineA = 0;

    [Header("Particle System B")]
    public ParticleSystem particlesB;
    public int startLineB = 0;
    public int endLineB = 0;

    [Header("Particle System C")]
    public ParticleSystem particlesC;
    public int startLineC = 0;
    public int endLineC = 0;

    [Header("Particle System D")]
    public ParticleSystem particlesD;
    public int startLineD = 0;
    public int endLineD = 0;

    private void Start()
    {
        if (particlesA != null) particlesA.Stop();
        if (particlesB != null) particlesB.Stop();
        if (particlesC != null) particlesC.Stop();
        if (particlesD != null) particlesD.Stop();

        StartCoroutine(CycleText());
    }

    IEnumerator CycleText() {
        for (int index = 0; index < messages.Length; index++)
        {
            textComponent.text = messages[index];

            yield return StartCoroutine(FadeTextAlpha(0, 1));

            HandleParticles(particlesA, index, startLineA, endLineA);
            HandleParticles(particlesB, index, startLineB, endLineB);
            HandleParticles(particlesC, index, startLineC, endLineC);
            HandleParticles(particlesD, index, startLineD, endLineD);

            yield return new WaitForSeconds(displayTime);

            yield return StartCoroutine(FadeTextAlpha(1, 0));
        }

        textComponent.text = "";

        SceneStateManager.LoadAppropriateScene();
    }

    void HandleParticles(ParticleSystem ps, int index, int start, int end)  {
        if (ps == null) return;

        if (index >= start && index <= end)
        {
            if (!ps.isPlaying)
                ps.Play();
        }
        else
        {
            if (ps.isPlaying)
                ps.Stop();
        }
    }

    IEnumerator FadeTextAlpha(float from, float to) {
        float elapsed = 0f;
        Color c = textComponent.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            textComponent.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        textComponent.color = new Color(c.r, c.g, c.b, to);
    }
}
