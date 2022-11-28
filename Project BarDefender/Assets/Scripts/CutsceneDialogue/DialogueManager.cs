using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.Interactions;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _characterNameHolder;
    [SerializeField] private TextMeshProUGUI _dialogueHolder;
    [SerializeField] private TextMeshProUGUI _instructionDialogue;
    [SerializeField] bool _loadChoicePanelAfterDialogue = false;
    [SerializeField] GameObject _choicePanel = null;
    [SerializeField] public string _progressInstruction;
    [SerializeField] public string _continueInstruction;

    

    //[SerializeField] private Image _characterHead;
    //[SerializeField] private Image _characterBody;


    private bool _dialogueStarted = false;
    private bool _firstTime = true;
    // Queue for FIFO
    private Queue<string> sentences;

    public bool DialogueStarted => _dialogueStarted;

    private void Awake()
    {
        _instructionDialogue.text = _progressInstruction;
        _instructionDialogue.enabled = true;
        _firstTime = true;
        sentences = new Queue<string>();

        if (_choicePanel != null)
        {
            _choicePanel.SetActive(false);
        }
    }

    // Load in new CharacterName and start new queue
    public void StartDialogue(DialogueData dialogueData)
    {
        _dialogueStarted = true;

        //_characterHead.sprite = dialogueData.CharacterHead;
        //_characterBody.sprite = dialogueData.CharacterBody;

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
        }

        if (!_firstTime)
        {
            _instructionDialogue.enabled = false;
        }

        else
        {
            _firstTime = false;
        }
    }

    public void EndDialogue()
    {
        _dialogueStarted = false;
    }

    public void EndCutscene()
    {
        _instructionDialogue.text = _continueInstruction;
        _instructionDialogue.enabled = true;

        if (_loadChoicePanelAfterDialogue)
        {
            _dialoguePanel.SetActive(false);
            _choicePanel.SetActive(true);
        }
    }
}
