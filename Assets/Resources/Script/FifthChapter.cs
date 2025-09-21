using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class FifthChapter : MonoBehaviour
{
    public SecondChapter sc;
    public GameObject[] subChapter;
    public int subChapterIndex;

    void OnEnable()
    {
        subChapterIndex = 0;
        foreach (GameObject sub in subChapter)
        {
            sub.SetActive(false);
        }
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }

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
    public void PressDoor()
    {
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }

    #endregion

    #region SubChapter2
    [Header("Chapter 5.2")]

    //DoorOpening
    public Image bgImage;
    public Sprite defaultBG;
    public Animator doorAnimator;

    //Door
    public GameObject doorObject;
    public int keyCount;
    public TMP_Text keyCodeText;

    public void PressKeyPadButton(string number)
    {
        keyCount++;
        keyCodeText.text += number;
        if (keyCount == 4)
        {
            StartCoroutine(WaitForDoorAnimation());
        }
    }

    IEnumerator WaitForDoorAnimation()
    {
        doorAnimator.enabled = true;
        doorObject.SetActive(false);
        AudioHandler.instance.PlaySFX("DoorOpen");
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }

    #endregion

    #region SubChapter3

    public VideoPlayer vp;
    public VideoClip casualEnding;
    public VideoClip blondeEnding;
    public bool hasStarted;

    #endregion

    #region SubChapter4

    public Image endingImage;
    public Sprite happyEnding;
    public Sprite normalEnding;
    public Sprite fakeEnding;

    public void ChooseEnding()
    {
        if (GameData.instance.playerRating >= 0 && GameData.instance.playerRating <= 2)
        {
            endingImage.sprite = happyEnding;
        }
        else if (GameData.instance.playerRating >= 2 && GameData.instance.playerRating <= 4)
        {
            endingImage.sprite = normalEnding;
        }
        else if (GameData.instance.playerRating >= 4 && GameData.instance.playerRating <= 5)
        {
            endingImage.sprite = fakeEnding;
        }
    }

    #endregion
    void Update()
    {
        #region SubChapter3
        if (vp.isPlaying) hasStarted = true;
        if (hasStarted && !vp.isPlaying)
        {
            StartCoroutine(LoadSubChapter(subChapterIndex));
            hasStarted = false;
        }
        #endregion

        if (subChapterIndex >= subChapter.Length)
        {
            if (Input.anyKeyDown)
            {
                MenuHandler.instance.BackToMainMenu();
            }
        }
    }
    void defaultSubChapter()
    {
        switch (subChapterIndex)
        {
            case 0:
                AudioHandler.instance.StopMusic();
                StartCoroutine(IntroSequence());
                break;
            case 1:
                break;
            case 2:
                keyCount = 0;
                doorObject.SetActive(true);
                doorAnimator.enabled = false;
                bgImage.sprite = defaultBG;
                break;
            case 3:
                // Debug.Log("kosong yak ini");
                AudioHandler.instance.PlaySFX("OpenMaskEnding");
                if (sc.choosedOutfit == "casual")
                {
                    vp.clip = casualEnding;
                }
                else if (sc.choosedOutfit == "blonde")
                {
                    vp.clip = blondeEnding;
                }
                vp.Play();
                break;
            case 4:
                ChooseEnding();
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
