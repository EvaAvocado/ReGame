using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Textbox : MonoBehaviour
{
    [Header("Textbox Settings")] [SerializeField]
    private bool usesCharacterInfo;

    [Header("Textbox References")]
    //Text
    [SerializeField]
    public TMP_Text dialogueText;

    [HideInInspector] public string dialogue;

    //Character Name
    //[SerializeField] private TMP_Text nameText;
    //[SerializeField] private Image nameTextImage;

    //Character Sprite
    //[SerializeField] private Image characterSpriteBackground;
    //[SerializeField] private Image characterSpriteImage;

    [Header("Button Settings")] [SerializeField]
    private Button _chooseButton1;

    [SerializeField] private TMP_Text _button1Text;
    [SerializeField] private Button _chooseButton2;
    [SerializeField] private TMP_Text _button2Text;
    [SerializeField] private Button _chooseButton3;
    [SerializeField] private TMP_Text _button3Text;
    [SerializeField] private bool _isNeedButtonToMid;

    private bool _isButton;

    private CharacterProfile myCharater;

    [Header("Audio Settings")]
    [SerializeField]
    [Tooltip("Audio blips are AudioClips that play as each letter is typed out in the dialogue box.")]
    private bool useAudioBlips;

    [SerializeField] [Tooltip("This will be used as the default audio clip if not specified by character profile.")]
    private AudioClip[] defaultClips;

    private AudioSource audioSource;

    [Header("Text Animation Settings")] [SerializeField]
    private TextAnimations textAnimations;

    public static event Action<Textbox> OnPress;
    public static event Action OnPressButton1;
    public static event Action OnPressButton2;
    public static event Action OnPressButton3;

    public DialogueController DialogueController;

    public Coroutine currentCoroutine;

    public bool IsButton => _isButton;

    public void StopSpawn()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
    }

    public void ButtonPress(int number)
    {
        if (number == 1)
        {
            DialogueController.EndDialogue();
            OnPressButton1?.Invoke();
        }
        else if (number == 2)
        {
            DialogueController.EndDialogue();
            OnPressButton2?.Invoke();
        }
        else
        {
            DialogueController.EndDialogue();
            OnPressButton3?.Invoke();
        }
    }

    private void OnMouseDown()
    {
        OnPress?.Invoke(this);
    }

    #region Init

    public void InitializeTextbox(string dialogue)
    {
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);

        audioSource = GetComponent<AudioSource>();
        this.dialogue = dialogue;

        dialogueText.transform.localPosition =
            new Vector3(dialogueText.transform.localPosition.x, 0, dialogueText.transform.localPosition.z);

        if (_chooseButton1 != null) _chooseButton1.gameObject.SetActive(false);
        if (_chooseButton2 != null) _chooseButton2.gameObject.SetActive(false);
        if (_chooseButton3 != null) _chooseButton3.gameObject.SetActive(false);
        
        _isButton = false;

        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void InitializeTextbox(string dialogue, string nameButton1, string nameButton2)
    {
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);

        audioSource = GetComponent<AudioSource>();
        this.dialogue = dialogue;

        if (_chooseButton1 != null) _button1Text.text = nameButton1;
        if (_chooseButton2 != null) _button2Text.text = nameButton2;
        if (_chooseButton3 != null) _chooseButton3.gameObject.SetActive(false);

        _isButton = true;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void InitializeTextbox(string dialogue, string nameButton3)
    {
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);

        audioSource = GetComponent<AudioSource>();
        this.dialogue = dialogue;

        if (_chooseButton3 != null)
        {
            _button3Text.text = nameButton3;
            /*if (_isNeedButtonToMid) _chooseButton3.transform.localPosition =
                new Vector3(0, _chooseButton3.transform.localPosition.y, _chooseButton3.transform.localPosition.z);*/
        }

        if (_chooseButton1 != null) _chooseButton1.gameObject.SetActive(false);
        if (_chooseButton2 != null) _chooseButton2.gameObject.SetActive(false);

        _isButton = true;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void InitializeTextbox(string dialogue, string nameButton1, string nameButton2, Action onPressButton1,
        Action onPressButton2)
    {
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);

        audioSource = GetComponent<AudioSource>();
        this.dialogue = dialogue;

        if (_chooseButton1 != null) _button1Text.text = nameButton1;
        if (_chooseButton2 != null) _button2Text.text = nameButton2;
        if (_chooseButton3 != null) _chooseButton3.gameObject.SetActive(false);

        OnPressButton1 = onPressButton1;
        OnPressButton2 = onPressButton2;

        _isButton = true;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void InitializeTextbox(string dialogue, string nameButton3, Action onPressButton3)
    {
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);

        audioSource = GetComponent<AudioSource>();
        this.dialogue = dialogue;

        if (_chooseButton3 != null)
        {
            _button3Text.text = nameButton3;
            /*if (_isNeedButtonToMid) _chooseButton3.transform.localPosition =
                new Vector3(0, _chooseButton3.transform.localPosition.y, _chooseButton3.transform.localPosition.z);*/
        }

        if (_chooseButton1 != null) _chooseButton1.gameObject.SetActive(false);
        if (_chooseButton2 != null) _chooseButton2.gameObject.SetActive(false);

        OnPressButton3 = onPressButton3;

        _isButton = true;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    #endregion

    public void DisplayText()
    {
        dialogueText.text = dialogue;
    }

    public void DisplayText(float typeSpeed)
    {
        dialogueText.text = "";

        currentCoroutine = StartCoroutine(OneLetterAtAtime(typeSpeed));
    }

    private IEnumerator OneLetterAtAtime(float typeSpeed)
    {
        //Regex patterns for detecting RichText tags in our string
        string styleTextPattern = @">[^<]+</"; //Regex pattern to find '> SOMETHING <' 
        string openTagPattern = @"(<[^>/]+>)+"; //Pattern to find <x><y><z>
        string closeTagPattern = @"(</[^>]+>)+"; //Pattern to fid </x></y></z>

        string cleanDialogue = dialogue;

        //Get our substrings that are styled
        List<StyleTextChunk> styleTextChunks = new List<StyleTextChunk>();
        int indexOffset = 0; //This is used to compensate for loss of data when removing <animate> tags
        var regexMatches = Regex.Matches(dialogue, styleTextPattern);
        for (int i = 0; i < regexMatches.Count; i++)
        {
            StyleTextChunk styleChunk = new StyleTextChunk();

            styleChunk.styledText = regexMatches[i].ToString().Replace(">", "").Replace("</", "");
            styleChunk.openTagString = Regex.Matches(dialogue, openTagPattern)[i].ToString();
            styleChunk.closeTagString = Regex.Matches(dialogue, closeTagPattern)[i].ToString();

            //If we find animate tags in the chunk, then we need to store it and remove from open/close tags
            if (styleChunk.openTagString.Contains("<animate"))
            {
                int animateTagStartIndex = styleChunk.openTagString.IndexOf("<animate");
                int animateTagEndIndex = 0;
                for (int x = animateTagStartIndex; x < styleChunk.openTagString.Length; x++)
                {
                    if (styleChunk.openTagString[x] == '>')
                    {
                        animateTagEndIndex = x;
                        break;
                    }
                }

                string animateTagString = styleChunk.openTagString.Substring(animateTagStartIndex,
                    animateTagEndIndex - animateTagStartIndex + 1);
                styleChunk.usesAnimations = true;
                styleChunk.animationTags = animateTagString;
            }
            else
            {
                styleChunk.usesAnimations = false;
            }

            styleChunk.styledTextStartIndex =
                regexMatches[i].Index - styleChunk.openTagString.Length + 1 -
                indexOffset; //Uses offset to compensate for when animate tags are removed

            //Remove open and close tags from our clean dialogue
            cleanDialogue = cleanDialogue.Replace(styleChunk.openTagString, "");
            cleanDialogue = cleanDialogue.Replace(styleChunk.closeTagString, "");

            //If we used animations in this chunk, then we need to remove them from the open/close tags manually
            if (styleChunk.usesAnimations)
            {
                styleChunk.openTagString = styleChunk.openTagString.Replace(styleChunk.animationTags, "");
                styleChunk.closeTagString = styleChunk.closeTagString.Replace("</animate>", "");

                if (i > 0)
                {
                    int removeAmount = 0;
                    for (int x = i; x > 0; x--)
                    {
                        removeAmount += styleTextChunks[x - 1].GetLength() - styleTextChunks[x - 1].styledText.Length;
                    }

                    styleChunk.animationStartIndex = styleChunk.styledTextStartIndex - removeAmount;
                    styleChunk.animationEndIndex = styleChunk.animationStartIndex + styleChunk.styledText.Length;
                }
                else
                {
                    styleChunk.animationStartIndex = styleChunk.styledTextStartIndex;
                    styleChunk.animationEndIndex = styleChunk.animationStartIndex + styleChunk.styledText.Length;
                }
            }

            //Reset our offset if needed
            indexOffset = styleChunk.usesAnimations
                ? indexOffset + (styleChunk.animationTags.Length + "</animate>".Length)
                : indexOffset;

            styleTextChunks.Add(styleChunk);
        }

        if (textAnimations != null)
        {
            foreach (StyleTextChunk chunk in styleTextChunks.Where(txt => txt.usesAnimations))
            {
                // Debug.Log(chunk.animationTags);
                TextAnimationInfo animationSettings = new TextAnimationInfo(chunk.animationStartIndex,
                    chunk.animationEndIndex, chunk.animationTags.Replace("<animate=", "").Replace(">", ""));
                textAnimations.AddAnimationInfo(animationSettings);
            }
        }

        int workingIndex = 0;
        string displayText = "";
        int styleChunkIndex = 0;


        foreach (char letter in cleanDialogue)
        {
            if (styleTextChunks.Count > 0)
            {
                StyleTextChunk currentStyleChunk = styleTextChunks[styleChunkIndex];

                if (workingIndex < currentStyleChunk.styledTextStartIndex)
                {
                    // Debug.Log("Normal Letter: " + letter);
                    displayText += letter;
                }
                else if (workingIndex == currentStyleChunk.styledTextStartIndex)
                {
                    // Debug.Log("Starting Tags: " + letter);
                    displayText += currentStyleChunk.openTagString;
                    displayText += letter;
                    workingIndex = displayText.Length - 1;
                    displayText += currentStyleChunk.closeTagString;
                }
                else if (workingIndex > currentStyleChunk.styledTextStartIndex && workingIndex <
                         currentStyleChunk.styledTextStartIndex + currentStyleChunk.styledText.Length +
                         currentStyleChunk.openTagString.Length)
                {
                    // Debug.Log("Inside Tags: " + letter);
                    displayText = displayText.Insert(workingIndex, letter.ToString());
                }

                if (workingIndex >= currentStyleChunk.styledTextStartIndex + currentStyleChunk.styledText.Length +
                    currentStyleChunk.openTagString.Length)
                {
                    // Debug.Log("Exiting Style: " + letter);
                    workingIndex = displayText.Length;
                    styleChunkIndex++;
                    if (styleChunkIndex >= styleTextChunks.Count - 1)
                    {
                        styleChunkIndex = styleTextChunks.Count - 1;
                    }

                    displayText += letter;
                }
            }
            else
            {
                displayText += letter;
            }


            workingIndex++;

            dialogueText.text = displayText;

            if (audioSource != null)
            {
                if (useAudioBlips)
                {
                    if (myCharater != null)
                    {
                        if (myCharater.speechSFXBlips.Length > 0)
                        {
                            audioSource.clip =
                                myCharater.speechSFXBlips[Random.Range(0, myCharater.speechSFXBlips.Length)];
                        }
                        else
                        {
                            audioSource.clip = defaultClips[Random.Range(0, defaultClips.Length)];
                        }
                    }
                    else
                    {
                        if (defaultClips.Length > 0)
                        {
                            audioSource.clip = defaultClips[Random.Range(0, defaultClips.Length)];
                        }
                    }

                    audioSource.Play();
                }
            }


            if (typeSpeed != 0)
            {
                yield return new WaitForSeconds(typeSpeed);
            }
        }
    }

    struct StyleTextChunk
    {
        public string openTagString;
        public string closeTagString;

        public int styledTextStartIndex;
        public string styledText;

        public bool usesAnimations;
        public int animationStartIndex;
        public int animationEndIndex;
        public string animationTags;

        public int GetLength()
        {
            return openTagString.Length + styledText.Length + closeTagString.Length;
        }

        public void PrintInfo()
        {
            Debug.Log("New Style Chunk: \n" +
                      "Index: " +
                      styledTextStartIndex +
                      "\n" +
                      "String: " +
                      styledText +
                      "\n" +
                      "Uses Animations" + usesAnimations + "\n"
                      + "Animation Tags: " + animationTags);
            // "Open Tag: " +
            // openTagString +
            // "\n" +
            // "Close Tag: " +
            // closeTagString);
        }
    }
}