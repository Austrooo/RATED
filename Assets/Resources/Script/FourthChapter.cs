using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class FourthChapter : MonoBehaviour
{
    public SecondChapter sc;
    public GameObject[] subChapter;
    public int subChapterIndex;

    #region Introduction

    IEnumerator IntroSequence()
    {
        Debug.Log("starting intro seq");
        yield return new WaitForSeconds(2f);
        StartCoroutine(LoadSubChapter(subChapterIndex));
        Debug.Log("Intro sequence 3 completed");
    }

    #endregion

    #region SubChapter1
    [Header("Chapter 4.1")]
    public VideoPlayer vp1;
    public bool hasStarted1 = false;

    #endregion

    #region SubChapter2

    public Animator receptionInteractionAnimatior;
    public GameObject reactionButtons;
    public Image receptionistImage;
    public Sprite defaultReceptionist;
    public Sprite ratingReceptionist;
    public bool reactionToReceptionistChoosed;

    IEnumerator WaitForBeginningWalk()
    {
        reactionButtons.SetActive(false);
        yield return new WaitForSeconds(2f);
        reactionButtons.SetActive(true);
    }
    public void ChooseReactionToReceptionist(string action)
    {
        if (reactionToReceptionistChoosed) return;
        reactionToReceptionistChoosed = true;
        StartCoroutine(PlayReceptionReactionAnimation(action));
    }

    IEnumerator PlayReceptionReactionAnimation(string action)
    {
        switch (action, sc.choosedOutfit)
        {
            case ("continue", "casual"):
                receptionInteractionAnimatior.SetTrigger("ContinueWalkCasual");
                receptionistImage.sprite = ratingReceptionist;
                GameData.instance.AddRating(2.9f);
                AudioHandler.instance.PlaySFX("RatingSound");
                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadSubChapter(subChapterIndex));
                break;
            case ("nod", "casual"):
                receptionInteractionAnimatior.SetTrigger("NodCasual");
                yield return new WaitForSeconds(2f);
                receptionInteractionAnimatior.SetTrigger("ContinueWalkCasual");
                receptionistImage.sprite = ratingReceptionist;
                GameData.instance.AddRating(4.4f);
                AudioHandler.instance.PlaySFX("RatingSound");
                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadSubChapter(subChapterIndex));
                break;
            case ("wave", "casual"):
                receptionInteractionAnimatior.SetTrigger("WaveCasual");
                yield return new WaitForSeconds(2f);
                receptionInteractionAnimatior.SetTrigger("ContinueWalkCasual");
                receptionistImage.sprite = ratingReceptionist;
                GameData.instance.AddRating(4.5f);
                AudioHandler.instance.PlaySFX("RatingSound");
                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadSubChapter(subChapterIndex));
                break;
            case ("continue", "blonde"):
                receptionInteractionAnimatior.SetTrigger("ContinueWalkBlonde");
                receptionistImage.sprite = ratingReceptionist;
                GameData.instance.AddRating(3.2f);
                AudioHandler.instance.PlaySFX("RatingSound");
                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadSubChapter(subChapterIndex));
                break;
            case ("nod", "blonde"):
                receptionInteractionAnimatior.SetTrigger("NodBlonde");
                yield return new WaitForSeconds(2f);
                receptionInteractionAnimatior.SetTrigger("ContinueWalkBlonde");
                receptionistImage.sprite = ratingReceptionist;
                GameData.instance.AddRating(4.1f);
                AudioHandler.instance.PlaySFX("RatingSound");
                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadSubChapter(subChapterIndex));
                break;
            case ("wave", "blonde"):
                receptionInteractionAnimatior.SetTrigger("WaveBlonde");
                yield return new WaitForSeconds(2f);
                receptionInteractionAnimatior.SetTrigger("ContinueWalkBlonde");
                receptionistImage.sprite = ratingReceptionist;
                GameData.instance.AddRating(4.4f);
                AudioHandler.instance.PlaySFX("RatingSound");
                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadSubChapter(subChapterIndex));
                break;
        }
        yield return new WaitForSeconds(1f);
        receptionInteractionAnimatior.SetTrigger("Default");
    }


    #endregion

    #region SubChapter3
    [Header("Chapter 4.3")]
    public VideoClip blondeEnterOffice;
    public VideoClip casualEnterOffice;
    public VideoPlayer vp3;
    public bool hasStarted3 = false;

    #endregion

    #region SubChapter4
    [Header("Chapter 4.4")]
    public bool isCorrect;
    public string username;
    public string password;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Sprite[] blondeTyping;
    public Sprite[] casualTyping;
    public Image typingImage;
    int index = 0;

    public void TypeText()
    {
        AudioHandler.instance.PlaySFX("Type");
        if (sc.choosedOutfit == "blonde")
        {
            if (index > blondeTyping.Length - 1) index = 0;
            typingImage.sprite = blondeTyping[index];
        }
        else if (sc.choosedOutfit == "casual")
        {
            if (index > casualTyping.Length - 1) index = 0;
            typingImage.sprite = casualTyping[index];
        }
        else
        {
            if (index > blondeTyping.Length - 1) index = 0;
            typingImage.sprite = blondeTyping[index];
        }
        index++;
    }

    public void CheckResult()
    {
        if (isCorrect) return;
        if (usernameInput.text == "" || passwordInput.text == "") return;
        if (usernameInput.text == username && passwordInput.text == password)
        {
            Debug.Log("Login Success");
            isCorrect = true;
            StartCoroutine(LoadSubChapter(subChapterIndex));
        }
        else
        {
            usernameInput.text = "";
            passwordInput.text = "";
            Debug.Log("Login Failed");
        }
    }

    #endregion

    #region SubChapter5
    [Header("Chapter 4.5")]

    public VideoClip blondeWorking;
    public VideoClip casualWorking;
    public VideoPlayer vp5;
    public bool hasStarted5 = false;

    #endregion

    #region SubChapter6
    [Header("Chapter 4.6")]
    public string reaction;
    public void ChooseReaction(string action)
    {
        reaction = action;
        reactionFromChara = reaction;
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }

    #endregion

    #region SubChapter7
    [Header("Chapter 4.7")]
    public VideoClip funnyVideoCasual;
    public VideoClip funnyVideoBlonde;
    public VideoClip ignoreCasual;
    public VideoClip ignoreBlonde;
    public VideoClip emphatizeCasual;
    public VideoClip emphatizeBlonde;
    public VideoPlayer vp7;
    public bool hasStarted7;

    #endregion

    #region SubChapter8

    public string reactionFromChara;
    public Image coworkerImage;
    public Image charaImage;
    public Sprite casualSprite;
    public Sprite blondeSprite;
    public Sprite coworkerhappy;
    public Sprite coworkerAngry;

    IEnumerator CoworkerGivingRating()
    {
        if (sc.choosedOutfit == "blonde")
        {
            charaImage.sprite = blondeSprite;
        }
        else if (sc.choosedOutfit == "casual")
        {
            charaImage.sprite = casualSprite;
        }

        yield return new WaitForSeconds(1.5f);

        switch (reactionFromChara)
        {
            case "emphatize":
                coworkerImage.sprite = coworkerhappy;
                break;
            case "funny":
                coworkerImage.sprite = coworkerhappy;
                break;
            case "ignore":
                coworkerImage.sprite = coworkerAngry;
                break;
        }
        AudioHandler.instance.PlaySFX("RatingSound");
    }

    #endregion
    private void OnEnable()
    {
        subChapterIndex = 0;
        foreach (GameObject sub in subChapter)
        {
            sub.SetActive(false);
        }
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }

    void Update()
    {
        #region SubChapter1
        if (vp1.isPlaying) hasStarted1 = true;
        if (hasStarted1 && !vp1.isPlaying)
        {
            StartCoroutine(LoadSubChapter(subChapterIndex));
            AudioHandler.instance.StopAllSFX();
            hasStarted1 = false;
        }
        #endregion

        #region SubChapter3
        if (vp3.isPlaying) hasStarted3 = true;
        if (hasStarted3 && !vp3.isPlaying)
        {
            StartCoroutine(LoadSubChapter(subChapterIndex));
            hasStarted3 = false;
        }
        #endregion

        #region SubChapter5
        if (vp5.isPlaying) hasStarted5 = true;
        if (hasStarted5 && !vp5.isPlaying)
        {
            StartCoroutine(LoadSubChapter(subChapterIndex));
            AudioHandler.instance.StopAllSFX();
            hasStarted5 = false;
        }
        #endregion

        #region SubChapter7

        if (vp7.isPlaying) hasStarted7 = true;
        if (hasStarted7 && !vp7.isPlaying)
        {
            StartCoroutine(MenuHandler.instance.LoadChapter(MenuHandler.GameState.Chapter_5));
            hasStarted7 = false;
        }
        #endregion
    }

    void defaultSubChapter()
    {
        switch (subChapterIndex)
        {
            case 0:
                StartCoroutine(IntroSequence());
                break;
            case 1:
                hasStarted1 = false;
                AudioHandler.instance.PlaySFX("SlidingDoorOpen");
                break;
            case 2:
                StartCoroutine(WaitForBeginningWalk());
                receptionistImage.sprite = defaultReceptionist;
                reactionToReceptionistChoosed = false;
                switch (sc.choosedOutfit)
                {
                    case "blonde":
                        receptionInteractionAnimatior.SetTrigger("BlondeOutfit");
                        break;
                    case "casual":
                        receptionInteractionAnimatior.SetTrigger("CasualOutfit");
                        break;
                    default:
                        receptionInteractionAnimatior.SetTrigger("BlondeOutfit");
                        break;
                }
                break;
            case 3:
                if (sc.choosedOutfit == "casual")
                {
                    vp3.clip = casualEnterOffice;
                }
                else if (sc.choosedOutfit == "blonde")
                {
                    vp3.clip = blondeEnterOffice;
                }
                else vp3.clip = blondeEnterOffice;
                break;
            case 4:
                usernameInput.text = "";
                passwordInput.text = "";
                index = 0;
                isCorrect = false;
                break;
            case 5:
                AudioHandler.instance.PlaySFX("KeyboardTyping");
                if (sc.choosedOutfit == "casual")
                {
                    vp5.clip = casualWorking;
                }
                else if (sc.choosedOutfit == "blonde")
                {
                    vp5.clip = blondeWorking;
                }
                else vp5.clip = blondeWorking;
                break;
            case 6:
                reaction = "";
                break;
            case 7:
                switch (sc.choosedOutfit, reaction)
                {
                    case ("blonde", "funny"):
                        GameData.instance.AddRating(3.2f);
                        vp7.clip = funnyVideoBlonde;
                        break;
                    case ("casual", "funny"):
                        GameData.instance.AddRating(3.6f);
                        vp7.clip = funnyVideoCasual;
                        break;
                    case ("blonde", "ignore"):
                        GameData.instance.AddRating(3.1f);
                        vp7.clip = ignoreBlonde;
                        break;
                    case ("casual", "ignore"):
                        GameData.instance.AddRating(3.1f);
                        vp7.clip = ignoreCasual;
                        break;
                    case ("blonde", "emphatize"):
                        GameData.instance.AddRating(4.5f);
                        vp7.clip = emphatizeBlonde;
                        break;
                    case ("casual", "emphatize"):
                        GameData.instance.AddRating(4.5f);
                        vp7.clip = emphatizeCasual;
                        break;
                }
                break;
            case 8:
                StartCoroutine(CoworkerGivingRating());
                break;
        }
    }

    IEnumerator LoadSubChapter(int index)
    {
        if (index != 0)
        {
            TransitionManager.instance.FadeOut();
            yield return new WaitForSeconds(1f);
        }
        foreach (GameObject sub in subChapter)
        {
            sub.SetActive(false);
        }
        subChapter[index].SetActive(true);
        defaultSubChapter();
        subChapterIndex++;
        if (index != 0)
        {
            TransitionManager.instance.FadeIn();
            yield return new WaitForSeconds(1f);
        }
    }
}
