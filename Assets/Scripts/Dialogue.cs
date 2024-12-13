using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textspeed;
    private int index;
    public Animator animator;
    public GameObject button;

    // Start is called before the first frame update
    void OnEnable()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        button.SetActive(false);
        
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLines());

    }
    IEnumerator TypeLines()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }
        if (animator != null)
        {
            animator.SetBool("Idle", true);
        }
        button.SetActive(true);
    }
}
