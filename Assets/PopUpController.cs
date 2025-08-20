using UnityEngine;

public class PopupController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Animator animator;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowPopup() {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        animator.Play("PopupFadeIn");
    }

    public void HidePopup() {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}