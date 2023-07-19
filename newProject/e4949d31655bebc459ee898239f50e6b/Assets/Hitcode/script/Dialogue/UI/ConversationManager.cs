using Hitcode_RoomEscape;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DialogueEditor
{
    public class ConversationManager : MonoBehaviour
    {
        private const float TRANSITION_TIME = 0.2f; // Transition time for fades

        public static ConversationManager Instance { get; private set; }

        public delegate void ConversationStartEvent();
        public delegate void ConversationEndEvent();

        public static ConversationStartEvent OnConversationStarted;
        public static ConversationEndEvent OnConversationEnded;

        private enum eState
        {
            TransitioningDialogueBoxOn,
            ScrollingText,
            TransitioningOptionsOn,
            Idle,
            TransitioningOptionsOff,
            TransitioningDialogueOff,
            Off,
            NONE,
        }

        // User-Facing options
        // Drawn by custom inspector
        //public bool ScrollText;
        //public float ScrollSpeed = 1;
        public Sprite BackgroundImage;
        public bool BackgroundImageSliced;
        public Sprite OptionImage;
        public bool OptionImageSliced;

        // Non-User facing 
        // Not exposed via custom inspector
        //
        // Base panels
        public RectTransform DialoguePanel;
        public RectTransform OptionsPanel;
        public RectTransform clickArea;
        // Dialogue UI
        public Image DialogueBackground;
        public Image NpcIcon;
        //public TMPro.TextMeshProUGUI NameText;
        //public TMPro.TextMeshProUGUI DialogueText;
        public Text NameText;
        public Text DialogueText;
        // Components
        public AudioSource AudioPlayer;
        // Prefabs
        public UIConversationButton ButtonPrefab;
        // Default values
        public Sprite BlankSprite;


        // Private
        private float m_elapsedScrollTime;
        private int m_scrollIndex;
        public int m_targetScrollTextCount;
        private eState m_state;
        private float m_stateTime;
        private Conversation m_conversation;
        private List<UIConversationButton> m_uiOptions;

        private SpeechNode m_pendingDialogue;
        private OptionNode m_selectedOption;
        private SpeechNode m_currentSpeech;

        public float typeWritor;
        //--------------------------------------
        // Awake, Start, Destroy
        //--------------------------------------

        private void Awake()
        {
            // Destroy myself if I am not the singleton
            if (Instance != null && Instance != this)
            {
                GameObject.Destroy(this.gameObject);
            }
            Instance = this;

            m_uiOptions = new List<UIConversationButton>();
        }

        private void Start()
        {
            NpcIcon.sprite = BlankSprite;
            DialogueText.text = "";
            TurnOffUI();
        }

        private void OnDestroy()
        {
            Instance = null;
        }




        //--------------------------------------
        // Update
        //--------------------------------------

        private void Update()
        {
            return;
            switch (m_state)
            {
                case eState.TransitioningDialogueBoxOn:
                    {
                        m_stateTime += Time.deltaTime;
                        float t = m_stateTime / TRANSITION_TIME;

                        if (t > 1)
                        {
                            DoSpeech(m_pendingDialogue);
                            return;
                        }

                        SetColorAlpha(DialogueBackground, t);
                        SetColorAlpha(NpcIcon, t);
                        SetColorAlpha(NameText, t);
                    }
                    break;



                case eState.TransitioningOptionsOn:
                    {
                        m_stateTime += Time.deltaTime;
                        float t = m_stateTime / TRANSITION_TIME;

                        if (t > 1)
                        {
                            SetState(eState.Idle);
                            return;
                        }

                        for (int i = 0; i < m_uiOptions.Count; i++)
                            m_uiOptions[i].SetAlpha(t);
                    }
                    break;

                case eState.Idle:
                    {
                        m_stateTime += Time.deltaTime;

                        //if (m_currentSpeech.AutomaticallyAdvance)
                        //{
                        //    if (m_currentSpeech.Dialogue != null || m_currentSpeech.Options == null || m_currentSpeech.Options.Count == 0)
                        //    {
                        //        if (m_stateTime > m_currentSpeech.TimeUntilAdvance)
                        //        {
                        //            SetState(eState.TransitioningOptionsOff);
                        //        }
                        //    }
                        //}
                    }
                    break;

                case eState.TransitioningOptionsOff:
                    {
                        m_stateTime += Time.deltaTime;
                        float t = m_stateTime / TRANSITION_TIME;

                        if (t > 1)
                        {
                            ClearOptions();

                            //if (m_currentSpeech.AutomaticallyAdvance)
                            //{
                            //    if (m_currentSpeech.Dialogue != null)
                            //    {
                            //        DoSpeech(m_currentSpeech.Dialogue);
                            //        return;
                            //    }
                            //    else if (m_currentSpeech.Options == null || m_currentSpeech.Options.Count == 0)
                            //    {
                            //        EndConversation();
                            //        return;
                            //    }  
                            //}

                            if (m_selectedOption == null)
                            {
                                EndConversation();
                                return;
                            }

                            SpeechNode nextAction = m_selectedOption.Dialogue;
                            if (nextAction == null)
                            {
                                EndConversation();
                            }
                            else
                            {
                                DoSpeech(nextAction);
                            }
                            return;
                        }


                        for (int i = 0; i < m_uiOptions.Count; i++)
                            m_uiOptions[i].SetAlpha(1 - t);

                        SetColorAlpha(DialogueText, 1 - t);
                    }
                    break;

                case eState.TransitioningDialogueOff:
                    {
                        m_stateTime += Time.deltaTime;
                        float t = m_stateTime / TRANSITION_TIME;

                        if (t > 1)
                        {
                            TurnOffUI();
                            return;
                        }

                        SetColorAlpha(DialogueBackground, 1 - t);
                        SetColorAlpha(NpcIcon, 1 - t);
                        SetColorAlpha(NameText, 1 - t);
                    }
                    break;
            }
        }






        //--------------------------------------
        // Set state
        //--------------------------------------

        private void SetState(eState newState)
        {
            switch (m_state)
            {
                case eState.TransitioningOptionsOff:
                    m_selectedOption = null;
                    break;
            }

            m_state = newState;
            m_stateTime = 0f;

            switch (m_state)
            {
                case eState.TransitioningDialogueBoxOn:
                    {
                        //SetColorAlpha(DialogueBackground, 1);
                        //SetColorAlpha(NpcIcon, 1);
                        //SetColorAlpha(NameText, 1);

                        DialogueText.text = "";
                        NameText.text = m_pendingDialogue.Name;
                        NpcIcon.sprite = m_pendingDialogue.Icon != null ? m_pendingDialogue.Icon : BlankSprite;

                        DoSpeech(m_pendingDialogue);
                    }
                    break;

                case eState.ScrollingText:
                    {
                        //SetColorAlpha(DialogueText, 1);

                        if (m_targetScrollTextCount == 0)
                        {
                            SetState(eState.TransitioningOptionsOn);
                            return;
                        }
                    }
                    break;

                case eState.TransitioningOptionsOn:
                    {
                        for (int i = 0; i < m_uiOptions.Count; i++)
                        {
                            m_uiOptions[i].gameObject.SetActive(true);
                            //m_uiOptions[i].SetAlpha(1);
                        }

                    }
                    break;
                case eState.TransitioningOptionsOff:


                    ClearOptions();


                    if (m_selectedOption == null)
                    {
                        EndConversation();
                        return;
                    }

                    SpeechNode nextAction = m_selectedOption.Dialogue;
                    if (nextAction == null)
                    {
                        EndConversation();
                    }
                    else
                    {
                        DoSpeech(nextAction);
                    }
                    return;




                    break;
                case eState.TransitioningDialogueOff:
                    {
                        TurnOffUI();
                    }
                    break;
            }
        }




        //--------------------------------------
        // Start / End Conversation
        //--------------------------------------

        public void StartConversation(NPCConversation conversation)
        {
            m_conversation = conversation.Deserialize();
            if (OnConversationStarted != null)
                OnConversationStarted.Invoke();

            TurnOnUI();
            m_pendingDialogue = m_conversation.Root;
            SetState(eState.TransitioningDialogueBoxOn);
        }

        public void EndConversation()
        {
            SetState(eState.TransitioningDialogueOff);

            if (OnConversationEnded != null)
                OnConversationEnded.Invoke();
        }




        //--------------------------------------
        // Do Speech
        //--------------------------------------
        int cTextIndex = 1;
        [HideInInspector]
        public string tempShowText;
        public void DoSpeech(SpeechNode speech)
        {
            if (speech == null)
            {
                EndConversation();
                return;
            }

            m_currentSpeech = speech;

            // Clear current options
            ClearOptions();

            // Set sprite
            if (speech.Icon == null)
            {
                NpcIcon.sprite = BlankSprite;
            }
            else
            {
                NpcIcon.sprite = speech.Icon;
            }

            // Set font
            //if (speech.TMPFont != null)
            //{
            //    DialogueText.font = speech.TMPFont;
            //}
            //else
            //{
            //    DialogueText.font = null;
            //}

            // Set name
            NameText.text = speech.Name;


            //============play text==================
            GameData.Instance.PlayTexts = new List<string>();
            GameData.Instance.PlayTexts.Add(speech.Text);

            if (typeWritor > 0f)
            {
                GameData.Instance.Textlocked1 = true;
                //GameData.Instance.locked = true;
                tempShowText = GameData.Instance.PlayTexts[0];
                cTextIndex = 1;
                GameManager.getInstance().stopSfx("typewriter");
                GameManager.getInstance().playSfx("typewriter");

                StopCoroutine("typeWrite");
                StartCoroutine("typeWrite");
            }
            else
            {
                allShowed();
            }


            // Set text
            //if (string.IsNullOrEmpty(speech.Text))
            //{

            //        DialogueText.text = "";
            //        DialogueText.resizeTextMaxSize = 1;

            //}
            //else
            //{

            //        DialogueText.text = speech.Text;
            //        DialogueText.resizeTextMaxSize = speech.Text.Length;

            //}



            // Call the event
            if (speech.Event != null)
                speech.Event.Invoke();
            //GameManager.instance.stopAllSFX();
            // Play the audio
            if (speech.Audio != null)
            {

                AudioPlayer.clip = speech.Audio;
                AudioPlayer.volume = speech.Volume;
                //AudioPlayer.Play();
                GameManager.instance.playSfx(speech.Audio.name);
            }

            // Display new options
            if (speech.Options.Count > 0)
            {
                for (int i = 0; i < speech.Options.Count; i++)
                {
                    UIConversationButton option = GameObject.Instantiate(ButtonPrefab, OptionsPanel);
                    option.InitButton(speech.Options[i]);
                    option.SetOption(speech.Options[i]);
                    m_uiOptions.Add(option);
                }
                clickArea.GetComponent<ClickAreaTouch>().isOption = true;
                //clickArea.GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                clickArea.GetComponent<ClickAreaTouch>().isOption = false ;
                // Display "Continue" / "End" if we should.
                //bool notAutoAdvance = !speech.AutomaticallyAdvance;
                //bool autoWithOption = (speech.AutomaticallyAdvance && speech.AutoAdvanceShouldDisplayOption);
                //if (notAutoAdvance || autoWithOption)
                //{
                // Else display "continue" button to go to following dialogue
                if (speech.Dialogue != null)
                {
                    //UIConversationButton option = GameObject.Instantiate(ButtonPrefab, OptionsPanel);
                    //option.SetFollowingAction(speech.Dialogue);
                    //m_uiOptions.Add(option);
                    clickArea.GetComponent<ClickAreaTouch>().m_action = speech.Dialogue;

                }
                // Else display "end" button
                else
                {
                    //UIConversationButton option = GameObject.Instantiate(ButtonPrefab, OptionsPanel);
                    //option.SetAsEndConversation();
                    //m_uiOptions.Add(option);
                    clickArea.GetComponent<ClickAreaTouch>().m_action = speech.Dialogue;
                }

                clickArea.GetComponent<Image>().raycastTarget = true;
            }
            //}

            // Set the button sprite and alpha
            for (int i = 0; i < m_uiOptions.Count; i++)
            {
                m_uiOptions[i].SetImage(OptionImage, OptionImageSliced);
                //m_uiOptions[i].SetAlpha(0);
                m_uiOptions[i].gameObject.SetActive(false);
            }

            SetState(eState.ScrollingText);
        }



        //--------------------------------------
        // Option Selected
        //--------------------------------------

        public void OptionSelected(OptionNode option)
        {
            m_selectedOption = option;
            SetState(eState.TransitioningOptionsOff);
        }




        //--------------------------------------
        // Util
        //--------------------------------------

        private void TurnOnUI()
        {
            DialoguePanel.gameObject.SetActive(true);
            OptionsPanel.gameObject.SetActive(true);
            clickArea.gameObject.SetActive(true);
            if (BackgroundImage != null)
            {
                DialogueBackground.sprite = BackgroundImage;

                if (BackgroundImageSliced)
                    DialogueBackground.type = Image.Type.Sliced;
                else
                    DialogueBackground.type = Image.Type.Simple;
            }

            NpcIcon.sprite = BlankSprite;
        }

        private void TurnOffUI()
        {
            DialoguePanel.gameObject.SetActive(false);
            OptionsPanel.gameObject.SetActive(false);
            clickArea.gameObject.SetActive(false);
            SetState(eState.Off);
            #if UNITY_EDITOR
                        // Debug.Log("[ConversationManager]: Conversation UI off.");
            #endif
        }

        private void ClearOptions()
        {
            while (m_uiOptions.Count != 0)
            {
                GameObject.Destroy(m_uiOptions[0].gameObject);
                m_uiOptions.RemoveAt(0);
            }
        }

        private void SetColorAlpha(MaskableGraphic graphic, float a)
        {
            Color col = graphic.color;
            col.a = a;
            graphic.color = col;
        }


        //======================================


        public void showFull()
        {
            cTextIndex = tempShowText.Length;
            DialogueText.text = tempShowText.Substring(0, cTextIndex);
            GameData.Instance.Textlocked1 = true;
            StopCoroutine("typeWrite");
            GameManager.getInstance().stopSfx("typewriter");
            GameData.Instance.PlayTexts.RemoveAt(0);
            cTextIndex = 0;
            StartCoroutine("unLock");
        }

        public void allShowed()
        {
            GameManager.getInstance().stopSfx("typewriter");
            if (GameData.Instance.PlayTexts.Count > 0)
            {
                DialogueText.text = GameData.Instance.PlayTexts[0];
                GameData.Instance.PlayTexts.RemoveAt(0);
            }
            else
            {
                DialogueText.text = "";
            }
            GameData.Instance.Textlocked1 = true;
            StartCoroutine("unLock");
            cTextIndex = 0;
        }


        IEnumerator typeWrite()
        {

            while (cTextIndex < tempShowText.Length)
            {
                yield return new WaitForSeconds(typeWritor);

                string tstr = tempShowText.Substring(0, cTextIndex);
                for (int i = 0; i < (tempShowText.Length - cTextIndex); i++)
                {
                    tstr += " ";
                }
                DialogueText.text = tstr;
                cTextIndex++;
                if (cTextIndex == tempShowText.Length)
                {
                    allShowed();
                    StopCoroutine("typeWrite");
                }
            }
        }

        IEnumerator unLock()
        {
            yield return new WaitForEndOfFrame();
            GameData.Instance.Textlocked1 = false;
        }



    }
}