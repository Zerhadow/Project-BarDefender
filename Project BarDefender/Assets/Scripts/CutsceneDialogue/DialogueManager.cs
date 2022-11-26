using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _characterNameHolder;
    [SerializeField] private TextMeshProUGUI _dialogueHolder;
    [SerializeField] private TextMeshProUGUI _continueDialogue;
    [SerializeField] private Image _characterHead;
    [SerializeField] private Image _characterBody;


    private bool _dialogueStarted = false;
    // Queue for FIFO
    private Queue<string> sentences;

    public bool DialogueStarted => _dialogueStarted;

    private void Awake()
    {
        _continueDialogue.enabled = false;
        sentences = new Queue<string>();
    }

    // Load in new CharacterName and start new queue
    public void StartDialogue(DialogueData dialogueData)
    {
        _dialogueStarted = true;

        _characterHead.sprite = dialogueData.CharacterHead;
        _characterBody.sprite = dialogueData.CharacterBody;

        // Load in character name into CharacterName UI
        _characterNameHolder.text = dialogueData.CharacterName;

        sentences.Clear();

        // Add all string from DialogueData array to sentences queue
        foreach (string _dialogue in dialogueData.Dialogues){
            sentences.Enqueue(_dialogue);
        }

        // Automatically load in sentence into DialogueBox UI
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // Remove a string from the sentences queue and load it into the DialogueBox UI
        string sentence = sentences.Dequeue();
        _dialogueHolder.text = sentence;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
    }

    public void EndDialogue()
    {
        _dialogueStarted = false;
    }

    public void EndCutscene()
    {
        _continueDialogue.enabled = true;
    }
}
