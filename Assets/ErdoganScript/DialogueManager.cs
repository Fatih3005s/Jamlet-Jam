using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float charDelay = 0.05f;
    public float waitAfterTyping = 1.5f;

    [TextArea]
    public string[] dialogueLines;

    private int currentLineIndex = 0;
    private Coroutine currentCoroutine;
    private bool isTyping = false;
    private bool isDeleting = false;
    private bool skipToEnd = false;

    void Start()
    {
        currentCoroutine = StartCoroutine(PlayDialogue());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (isTyping)
            {
                skipToEnd = true; // yazı yazılırken tıklanırsa hemen tamamla
            }
            else if (isDeleting)
            {
                StopCoroutine(currentCoroutine);
                textUI.text = "";
                isDeleting = false;
                currentLineIndex++;
                currentCoroutine = StartCoroutine(PlayDialogue());
            }
        }
    }

    IEnumerator PlayDialogue()
    {
        while (currentLineIndex < dialogueLines.Length)
        {
            string fullText = dialogueLines[currentLineIndex];
            textUI.text = "";
            isTyping = true;
            skipToEnd = false;

            // Yazma efekti
            for (int i = 0; i <= fullText.Length; i++)
            {
                if (skipToEnd)
                {
                    textUI.text = fullText;
                    break;
                }

                textUI.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(charDelay);
            }

            isTyping = false;
            yield return new WaitForSeconds(waitAfterTyping);

            // Silme efekti (sağdan sola)
            isDeleting = true;
            for (int i = fullText.Length - 1; i >= 0; i--)
            {
                textUI.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(charDelay);
            }

            textUI.text = "";
            isDeleting = false;

            currentLineIndex++;
        }

    }
}
