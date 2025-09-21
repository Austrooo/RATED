using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FirstChapter : MonoBehaviour
{
    public GameObject[] subChapter;
    public int currentSubChapter;

    #region Introduction
    IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(2f);
        currentSubChapter++;
        StartCoroutine(LoadSubChapter(currentSubChapter));
        Debug.Log("Intro sequence completed");
    }
    #endregion

    #region SubChapter1
    [Header("Chapter 1.1")]
    public VideoPlayer vp;
    public bool hasStarted = false;
    #endregion

    #region SubChapter2
    [Header("Chapter 1.2")]
    public Image displayedCharacter;
    public GameObject outfitButton;
    public Sprite sleepOutfit;
    public Sprite gymOutfit;

    public void ChangeToGymOutfit()
    {
        displayedCharacter.sprite = gymOutfit;
        outfitButton.SetActive(false);
        currentSubChapter++;
        StartCoroutine(LoadSubChapter(currentSubChapter));
    }

    #endregion

    #region SubChapter3
    [Header("Chapter 1.3")]
    public Image musicBG;
    public Sprite[] musicBGSprites;
    public bool hasChosenAudio;

    public void PlayAudio(string name)
    {
        if (hasChosenAudio) return;
        if (AudioHandler.instance.isMusicPlaying(name))
        {
            hasChosenAudio = true;
            currentSubChapter++;
            StartCoroutine(LoadSubChapter(currentSubChapter));
            return;
        }
        musicBG.enabled = true;
        switch (name)
        {
            case "Orchestra":
                musicBG.sprite = musicBGSprites[0];
                break;
            case "Pop":
                musicBG.sprite = musicBGSprites[1];
                break;
            case "Chill":
                musicBG.sprite = musicBGSprites[2];
                break;
            case "Rock":
                musicBG.sprite = musicBGSprites[3];
                break;
        }
        AudioHandler.instance.PlayMusic(name);
    }

    #endregion

    #region SubChapter4
    [Header("Chapter 1.4")]

    public GameObject chooseObject;
    public GameObject treadmilObject;
    public GameObject benchObject;
    public Image runningImage;
    public Image runningButtonImage;
    public Sprite[] runningSprites;
    public Sprite[] runningButtonSprites;
    public Image benchImage;
    public Sprite[] benchingSprites;
    public int index = 0;
    public int clickCount = 0;
    public bool isAnimating = false;

    public void ChooseSport(string sport)
    {
        switch (sport)
        {
            case "Treadmil":
                GameData.instance.AddRating(3.7f);
                treadmilObject.SetActive(true);
                benchObject.SetActive(false);
                break;
            case "Bench":
                GameData.instance.AddRating(4f);
                treadmilObject.SetActive(false);
                benchObject.SetActive(true);
                break;
        }
        AudioHandler.instance.PlaySFX("ManSatisfied");
        chooseObject.SetActive(false);
    }

    IEnumerator AnimateCharacter(string sport)
    {
        runningButtonImage?.gameObject.SetActive(false);
        benchImage.GetComponent<Button>().enabled = false;
        if (sport == "Treadmil")
        {
            AudioHandler.instance.PlaySFX("Treadmill");
        }
        int cycle = 0;
        while (true)
        {
            ChangeSprite();
            Debug.Log("cycle: " + cycle);
            if (sport == "Bench")
            {
                yield return new WaitForSeconds(0.5f);
                if (cycle < 10)
                {
                    // Debug.Log("test");
                    AudioHandler.instance.PlaySFX("BenchPress");
                }
                // Only trigger this block exactly at cycle 10
                if (cycle == 10)
                {
                    currentSubChapter++;
                    StartCoroutine(LoadSubChapter(currentSubChapter));
                }
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                if (cycle == 20)
                {
                    currentSubChapter++;
                    StartCoroutine(LoadSubChapter(currentSubChapter));
                }
            }
            if (cycle >= 30)
            {
                benchImage.GetComponent<Button>().enabled = true;
                runningButtonImage.gameObject.SetActive(true);
                yield break;
            }
            cycle++;
        }
    }

    public void ChangeSprite()
    {
        index++;
        clickCount++;
        if (treadmilObject.activeSelf)
        {
            if (index >= runningSprites.Length) index = 0;
            runningImage.sprite = runningSprites[index];
            runningButtonImage.sprite = runningButtonSprites[index];
        }
        else if (benchObject.activeSelf)
        {
            if (index >= benchingSprites.Length) index = 0;
            benchImage.sprite = benchingSprites[index];
            if(clickCount < 10)AudioHandler.instance.PlaySFX("BenchPress");
        }
        if (!isAnimating && clickCount == 10)
        {
            if(treadmilObject.activeSelf)
                StartCoroutine(AnimateCharacter("Treadmil"));
            else if (benchObject.activeSelf)
                StartCoroutine(AnimateCharacter("Bench"));
        }
    }


    #endregion

    #region SubChapter5
    [Header("Chapter 1.5")]
    public Image influencerImage;
    public Sprite influencerNormal;
    public Sprite influencerHappy;
    public Sprite influencerAnnoyed;
    public Sprite influencerGivingGoodRating;
    public Sprite influencerGivingBadRating;

    public void ChooseReaction(string reaction)
    {
        StartCoroutine(PlayReaction(reaction));
    }

    IEnumerator PlayReaction(string reaction)
    {
        switch (reaction)
        {
            case "Happy":
                influencerImage.sprite = influencerHappy;
                AudioHandler.instance.PlaySFX("GirlSatisfied");
                yield return new WaitForSeconds(1f);
                influencerImage.sprite = influencerGivingGoodRating;
                GameData.instance.AddRating(4f);
                AudioHandler.instance.PlaySFX("RatingSound");
                break;
            case "Annoyed":
                influencerImage.sprite = influencerAnnoyed;
                AudioHandler.instance.PlaySFX("GirlDisatisfied");
                yield return new WaitForSeconds(1f);
                influencerImage.sprite = influencerGivingBadRating;
                GameData.instance.AddRating(2.2f);
                AudioHandler.instance.PlaySFX("RatingSound");
                break;
            case "ignore":
                influencerImage.sprite = influencerNormal;
                yield return new WaitForSeconds(1f);
                influencerImage.sprite = influencerGivingGoodRating;
                GameData.instance.AddRating(3.3f);
                AudioHandler.instance.PlaySFX("RatingSound");
                break;
        }
        currentSubChapter++;
        StartCoroutine(LoadSubChapter(currentSubChapter));
    }

    #endregion

    #region SubChapter6
    [Header("Chapter 1.6")]
    public GameObject replyObject;
    public Image runnerImage;
    public Sprite defaultRunnerSprite;
    public Sprite runnerGivingRating;
    public void ShowReplyObject(bool status)
    {
        StartCoroutine(ReplyObjectEnum(status));
    }

    IEnumerator ReplyObjectEnum(bool status)
    {
        replyObject.SetActive(status);
        yield return new WaitForSeconds(2f);
        runnerImage.sprite = status ? runnerGivingRating : defaultRunnerSprite;
        if (status)
        {
            GameData.instance.AddRating(4.7f);
            AudioHandler.instance.PlaySFX("RatingSound");
        }
        else GameData.instance.AddRating(2.3f);
        // currentSubChapter = 0;
        StartCoroutine(MenuHandler.instance.LoadChapter(MenuHandler.GameState.Chapter_2));
    }

    #endregion

    void OnEnable()
    {
        foreach (GameObject sub in subChapter)
        {
            sub.SetActive(false);
        }
        currentSubChapter = 0;
        StartCoroutine(LoadSubChapter(currentSubChapter));
    }

    void Update()
    {
        #region SubChapter1
        if (vp.isPlaying) hasStarted = true;
        if (hasStarted && !vp.isPlaying && currentSubChapter == 1)
        {
            currentSubChapter++;
            StartCoroutine(LoadSubChapter(currentSubChapter));
            hasStarted = false;

        }
        #endregion

    }

    public void defaultSubChapter(int index)
    {
        switch (index)
        {
            case 0:
                AudioHandler.instance.PlayMusic("Gameplay");
                break;
            case 1:
                break;
            case 2:
                displayedCharacter.sprite = sleepOutfit;
                break;
            case 3:
                hasChosenAudio = false;
                musicBG.enabled = false;
                break;
            case 4:
                treadmilObject.SetActive(false);
                benchObject.SetActive(false);
                chooseObject.SetActive(true);
                index = 0;
                clickCount = 0;
                isAnimating = false;
                break;
            case 5:
                influencerImage.sprite = influencerNormal;
                break;
            case 6:
                runnerImage.sprite = defaultRunnerSprite;
                replyObject.SetActive(false);
                break;
        }
    }

    IEnumerator LoadSubChapter(int index)
    {
        AudioHandler.instance.StopAllSFX();
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
        defaultSubChapter(index);
        if (index != 0)
        {
            TransitionManager.instance.FadeIn();
            yield return new WaitForSeconds(1f);
        }
        if (index == 0)
        {
            StartCoroutine(IntroSequence());
        }
    }
}
