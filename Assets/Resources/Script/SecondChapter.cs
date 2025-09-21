using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondChapter : MonoBehaviour
{
    public GameObject[] subChapter;
    public string choosedOutfit;
    public int subChapterIndex;

    void OnEnable()
    {
        foreach (GameObject sub in subChapter)
        {
            sub.SetActive(false);
        }
        subChapterIndex = 0;
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }

    void DefaultSubChapter(int index)
    {
        switch (index)
        {
            case 0:
                StartCoroutine(IntroSequence());
                break;
            case 1:
                AudioHandler.instance.PlaySFX("ParkAmbience");
                characterImage.sprite = defaultOutfit;
                break;
            case 2:
                anon1.sprite = anon1default;
                anon2.sprite = anon2default;
                charaAnimator.enabled = false;
                if (choosedOutfit == "casual")
                {
                    chara.sprite = charaCasualNothing;
                }
                else if (choosedOutfit == "blonde")
                {
                    chara.sprite = charaBlondeNothing;
                }
                break;
            case 3:
                if(choosedOutfit == "casual")
                {
                    charaObject.sprite = casualOutfit;
                }
                else if (choosedOutfit == "blonde")
                {
                    charaObject.sprite = blondeOutfit;
                }
                anonDefault.SetActive(true);
                anonRating.SetActive(false);
                interactObject.SetActive(false);
                break;
        }
    }

    #region Introduction

    IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(LoadSubChapter(subChapterIndex));
        Debug.Log("Intro sequence 2 completed");
    }

    #endregion

    #region SubChapter 1
    [Header("Chapter 2.1")]

    public Image characterImage;
    public Sprite defaultOutfit;
    public Sprite blondeOutfit;
    public Sprite casualOutfit;

    public void ChooseOutfit(string outfit)
    {
        choosedOutfit = outfit;
        if (choosedOutfit == "casual")
        {
            characterImage.sprite = casualOutfit;
        }
        else if (choosedOutfit == "blonde")
        {
            characterImage.sprite = blondeOutfit;
        }
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }

    #endregion

    #region SubChapter 2
    [Header("Chapter 2.2")]
    public Animator charaAnimator;
    public Image chara;
    public Image anon1;
    public Image anon2;
    public Sprite anon1default;
    public Sprite anon2default;
    public Sprite anon1Rating;
    public Sprite anon2Rating;
    public Sprite charaBlondeNothing;
    public Sprite charaCasualNothing;
    public Sprite charaBlondeSmile;
    public Sprite charaCasualSmile;

    public void ChooseInteraction(string action)
    {
        StartCoroutine(StartPlayingInteraction(action));
    }

    IEnumerator StartPlayingInteraction(string action)
    {
        switch (action, choosedOutfit)
        {
            case ("smile", "blonde"):
                chara.sprite = charaBlondeSmile;
                yield return new WaitForSeconds(1f);
                anon1.sprite = anon1Rating;
                anon2.sprite = anon2Rating;
                GameData.instance.AddRating(4.2f);
                AudioHandler.instance.PlaySFX("RatingSound");
                break;
            case ("smile", "casual"):
                chara.sprite = charaCasualSmile;
                yield return new WaitForSeconds(1f);
                anon1.sprite = anon1Rating;
                anon2.sprite = anon2Rating;
                GameData.instance.AddRating(4.4f);
                AudioHandler.instance.PlaySFX("RatingSound");
                break;
            case ("nothing", "blonde"):
                GameData.instance.AddRating(3.7f);
                chara.sprite = charaBlondeNothing;
                break;
            case ("nothing", "casual"):
                GameData.instance.AddRating(3.8f);
                chara.sprite = charaCasualNothing;
                break;
            case ("wave", "blonde"):
                charaAnimator.enabled = true;
                charaAnimator.SetTrigger("BlondeWave");
                yield return new WaitForSeconds(1f);
                anon1.sprite = anon1Rating;
                anon2.sprite = anon2Rating;
                GameData.instance.AddRating(3.8f);
                AudioHandler.instance.PlaySFX("RatingSound");
                break;
            case ("wave", "casual"):
                charaAnimator.enabled = true;
                charaAnimator.SetTrigger("CasualWave");
                yield return new WaitForSeconds(1f);
                anon1.sprite = anon1Rating;
                anon2.sprite = anon2Rating;
                GameData.instance.AddRating(3.9f);
                AudioHandler.instance.PlaySFX("RatingSound");
                break;
        }
        yield return new WaitForSeconds(1f);
        charaAnimator.SetTrigger("Default");
        charaAnimator.enabled = false;
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }
    #endregion

    #region SubChapter 3
    [Header("Chapter 2.3")]
    public Image charaObject;
    public GameObject anonDefault;
    public GameObject anonRating;
    public GameObject interactObject;

    public void ChooseReaction(string action)
    {
        StartCoroutine(PlayReaction(action));
    }

    IEnumerator PlayReaction(string action)
    {
        switch (action, choosedOutfit)
        {
            case ("interact", "blonde"):
                interactObject.SetActive(true);
                yield return new WaitForSeconds(1f);
                anonDefault.SetActive(false);
                anonRating.SetActive(true);
                GameData.instance.AddRating(4.4f);
                AudioHandler.instance.PlaySFX("RatingSound");
                break;
            case ("interact", "casual"):
                interactObject.SetActive(true);
                yield return new WaitForSeconds(1f);
                anonDefault.SetActive(false);
                anonRating.SetActive(true);
                GameData.instance.AddRating(4.3f);
                AudioHandler.instance.PlaySFX("RatingSound");
                break;
            case ("ignore", "blonde"):
                GameData.instance.AddRating(2.6f);
                charaObject.sprite = charaBlondeNothing;
                break;
            case ("ignore", "casual"):
                GameData.instance.AddRating(2.4f);
                charaObject.sprite = charaCasualNothing;
                break;
        }
        yield return new WaitForSeconds(2f);
        AudioHandler.instance.StopAllSFX();
        StartCoroutine(MenuHandler.instance.LoadChapter(MenuHandler.GameState.Chapter_3));
        // subChapterIndex = 0;
    }

    #endregion
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
        DefaultSubChapter(subChapterIndex);
        subChapterIndex++;
        if (index != 0)
        {
            TransitionManager.instance.FadeIn();
            yield return new WaitForSeconds(1f);
        }
    }
}
