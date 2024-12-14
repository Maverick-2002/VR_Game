using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MachineTimeline : MonoBehaviour
{
    [SerializeField] List<BaseInteractable> interactables;
    [SerializeField] Slider slider;
    [ShowNonSerializedField] const float waitTime = 1.5f;
    [ProgressBar("Time Left", waitTime, EColor.Blue)]
    [SerializeField] float timeLeft = 0.0f;
    public float sliderfillvalue = 0f;
    [ShowNonSerializedField] int currentIndex = -1;
    [SerializeField] GameObject NextButton; 
    [SerializeField] GameObject PrevButton;
    [SerializeField] GameObject product;
    [SerializeField] GameObject productUI;


    bool canInteract = true;

    private void Awake()
    {
        currentIndex = -1;
        canInteract = true;
        slider.maxValue = interactables.Count * 0.15f;
        PrevButton.SetActive(false);
        product.SetActive(false);
        productUI.SetActive(false);
    }
    [Button]
    public void Next()
    {
        PrevButton.SetActive(true);
        if (currentIndex == interactables.Count - 1 || !canInteract) { return; }
        currentIndex++;
        interactables[currentIndex].Interact();
        sliderfillvalue += 0.15f;
        AnimateSlider(sliderfillvalue, 1.5f);
        if (currentIndex == interactables.Count - 1) {
            NextButton.SetActive(false);
            PrevButton.SetActive(true);
            product.SetActive(true);
            productUI.SetActive(true);

        }
        StartCoroutine(ResetWait());
    }

    [Button]
    public void Previous() 
    { 
        NextButton.SetActive(true) ;
        product.SetActive(false);
        productUI.SetActive(false);
        if(currentIndex < 0 || !canInteract) { return; }
        interactables[currentIndex].Interact();
        currentIndex--;
        sliderfillvalue -= 0.15f;
        AnimateSlider(sliderfillvalue, 1.5f);
        if (currentIndex < 0) {
            PrevButton.SetActive(false);
            NextButton.SetActive(true);

        }

        StartCoroutine(ResetWait());
    }
    private IEnumerator ResetWait()
    {
        canInteract = false;
        timeLeft = waitTime;
        while (timeLeft > 0.0f)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        timeLeft = 0.0f;
        canInteract = true;
    }
    public void AnimateSlider(float value, float time)
    {
        slider.DOValue(value, time).SetEase(Ease.InOutQuad);
    }
}
