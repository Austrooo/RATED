using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class ThirdChapter : MonoBehaviour
{
    public SecondChapter sc;
    public GameObject[] subChapter;
    public string choosedOutfit;
    public Sprite choosedPicture;
    public int subChapterIndex;

    private void OnEnable()
    {
        choosedOutfit = sc.choosedOutfit;
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
    [Header("Chapter 3.1")]

    public VideoClip blondeVC;
    public VideoClip casualVC;
    public VideoPlayer vp;
    public bool hasStarted = false;

    #endregion

    #region SubChapter2
    [Header("Chapter 3.2")]
    public Image BG;
    public Sprite blondeWithPhone;
    public Sprite casualWithPhone;

    public Image pictureInPhone;
    public Sprite[] pictures;
    public int pictureIndex;

    public void ChoosePicture(bool isLeft)
    {
        if (isLeft)
        {
            pictureIndex--;
            if(pictureIndex < 0) pictureIndex = pictures.Length - 1;
            pictureInPhone.sprite = pictures[pictureIndex];
        }
        else
        {
            ++pictureIndex;
            if (pictureIndex >= pictures.Length) pictureIndex = 0;
            pictureInPhone.sprite = pictures[pictureIndex];
        }
    }
    
    public void ConfirmPicture()
    {
        choosedPicture = pictureInPhone.sprite;
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }

    #endregion

    #region SubChapter3
    [Header("Chapter 3.3")]
    public Sprite[] happyPhotos;
    public Sprite[] normalPhotos;
    public Sprite[] tryHardPhotos;
    public PuzzleManager pm;
    public void StartScatterPhotos()
    {
        pm = GetComponent<PuzzleManager>();
        pm.InitializePuzzleGame();
        Debug.Log(choosedPicture.name);
        switch (choosedPicture.name)
        {
            case "Happy photo_0":
                pm.ScatterPieces(happyPhotos);
                break;
            case "Normal photo_0":
                pm.ScatterPieces(normalPhotos);
                break;
            case "Try Hard Photo_0":
                pm.ScatterPieces(tryHardPhotos);
                break;
        }
    }

    IEnumerator PuzzleComplete()
    {
        while (true)
        {
            if (pm.CheckIsPuzzleComplete())
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(LoadSubChapter(subChapterIndex));
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion

    #region SubChapter4
    [Header("Chapter 3.4")]

    public GameObject editPhotoUI;
    public GameObject postUI;
    public GameObject finishEditUI;
    public Image photoImage;
    public Image brightnessLayer;
    public Image colorLayer;
    public Sprite[] brightnessSprite;
    public Sprite[] colorSprite;
    public Button brightnessButton;
    public Button colorButton;
    public Button beautifyButton;
    public Sprite brightnessSelected;
    public Sprite colorSelected;
    public Sprite beautifySelected;
    public Sprite brightnessUnselected;
    public Sprite colorUnselected;
    public Sprite beautifyUnselected;
    public Sprite[] happyPictures;
    public Sprite[] normalPictures;
    public Sprite[] tryHardPictures;
    public Slider slider;
    public string editType;
    private bool brightness = false;
    private int brightnessLevel = 0;
    private bool color = false;
    private int colorLevel = 0;
    private bool beautify = false;
    private int beautifyLevel = 0;
    public void ChooseEdit(string name)
    {
        brightnessButton.image.sprite = brightnessUnselected;
        colorButton.image.sprite = colorUnselected;
        beautifyButton.image.sprite = beautifyUnselected;
        brightness = false;
        color = false;
        beautify = false;
        switch (name)
        {
            case "brightness":
                brightness = true;
                editType = "brightness";
                slider.value = brightnessLevel;
                brightnessButton.image.sprite = brightnessSelected;
                break;
            case "color":
                color = true;
                editType = "color";
                slider.value = colorLevel;
                colorButton.image.sprite = colorSelected;
                break;
            case "beautify":
                beautify = true;
                editType = "beautify";
                slider.value = beautifyLevel;
                beautifyButton.image.sprite = beautifySelected;
                break;
        }
    }

    public void FinishEditing()
    {
        editPhotoUI.SetActive(false);
        finishEditUI.SetActive(false);
        postUI.SetActive(true);
    }

    public void Posting()
    {
        AudioHandler.instance.PlaySFX("Post");
        switch (choosedPicture.name)
        {
            case "Happy photo_0":
                GameData.instance.AddRating(2.8f);
                break;
            case "Normal photo_0":
                GameData.instance.AddRating(4.5f);
                break;
            case "Try Hard Photo_0":
                GameData.instance.AddRating(2.1f);
                break;
        }
        switch (beautifyLevel)
        {
            case 0:
                GameData.instance.AddRating(4f);
                break;
            case 1:
                GameData.instance.AddRating(3.6f);
                break;
            case 2:
                GameData.instance.AddRating(2.2f);
                break;
        }
        StartCoroutine(LoadSubChapter(subChapterIndex));
    }

    #endregion

    #region SubChapter5
    [Header("Chapter 3.5")]
    public Sprite[] heartSprites;
    public Sprite[] happySprites;
    public Sprite[] normalSprites;
    public Sprite[] tryHardSprites;
    public Image heartImage;
    public Image postedImage;
    public TMP_Text likesText;
    public int likes;

    public void SetLikes()
    {
        switch (choosedPicture.name)
        {
            case "Happy photo_0":
                postedImage.sprite = happySprites[beautifyLevel];
                likes = 3500;
                break;
            case "Normal photo_0":
                postedImage.sprite = normalSprites[beautifyLevel];
                likes = 1100;
                break;
            case "Try Hard Photo_0":
                postedImage.sprite = tryHardSprites[beautifyLevel];
                likes = 15000;
                break;
        }
        StartCoroutine(AnimateLikes());
    }

    IEnumerator AnimateLikes()
    {
        float duration = 2f;
        float elapsed = 0f;
        int startLikes = 0;
        int endLikes = likes;
        heartImage.sprite = heartSprites[0];
        int spriteIndex = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            int displayedLikes = Mathf.RoundToInt(Mathf.Lerp(startLikes, endLikes, elapsed / duration));
            likesText.text = ConvertLikeNumber(displayedLikes);
            AudioHandler.instance.PlaySFX("Likes");
            if (displayedLikes % 100 == 0 && displayedLikes != 0)
            {
                heartImage.sprite = heartSprites[spriteIndex];
                spriteIndex++;
                if (spriteIndex >= heartSprites.Length) spriteIndex = 0;
            }
            yield return null;
        }
        likesText.text = ConvertLikeNumber(endLikes);
        heartImage.enabled = false;
        yield return new WaitForSeconds(2f);
        StartCoroutine(MenuHandler.instance.LoadChapter(MenuHandler.GameState.Chapter_4));
        // subChapterIndex = 0;
    }

    string ConvertLikeNumber(int likes)
    {
        if (likes < 1000) return likes.ToString();
        if (likes < 1000000) return (likes / 1000f).ToString("F1") + "K";
        return (likes / 1000000f).ToString("F1") + "M";
    }

    #endregion
    private void Update()
    {
        #region SubChapter1
        if (vp != null && subChapterIndex == 2)
        {
            if (vp.isPlaying) 
            {
                hasStarted = true;
            }
            else if (hasStarted && !vp.isPlaying)
            {
                if (vp.time > 0 && vp.time >= vp.length - 0.1f)
                {
                    AudioHandler.instance.StopAllSFX();
                    StartCoroutine(LoadSubChapter(subChapterIndex));
                    hasStarted = false;
                }
            }
        }
        #endregion

        #region SubChapter4

        switch (editType)
        {
            case "brightness":
                brightnessLevel = (int)slider.value;
                brightnessLayer.sprite = brightnessSprite[brightnessLevel];
                break;
            case "color":
                colorLevel = (int)slider.value;
                colorLayer.sprite = colorSprite[colorLevel];
                break;
            case "beautify":
                beautifyLevel = (int)slider.value;
                switch (choosedPicture.name)
                {
                    case "Happy photo_0":
                        photoImage.sprite = happyPictures[beautifyLevel];
                        break;
                    case "Normal photo_0":
                        photoImage.sprite = normalPictures[beautifyLevel];
                        break;
                    case "Try Hard Photo_0":
                        photoImage.sprite = tryHardPictures[beautifyLevel];
                        break;
                }
                break;
        }

        #endregion
    }
    public void defaultSubChapter()
    {
        switch (subChapterIndex)
        {
            case 0:
                StartCoroutine(IntroSequence());
                break;
            case 1:
                AudioHandler.instance.PlaySFX("TrainRunning");
                if (choosedOutfit == "blonde") vp.clip = blondeVC;
                else if (choosedOutfit == "casual") vp.clip = casualVC;
                else vp.clip = blondeVC;
                vp.Stop();
                vp.time = 0;
                vp.Play();
                hasStarted = false; // Reset the flag
                break;
            case 2:
                if (choosedOutfit == "blonde") BG.sprite = blondeWithPhone;
                else if (choosedOutfit == "casual") BG.sprite = casualWithPhone;
                else BG.sprite = blondeWithPhone;
                break;
            case 3:
                StartScatterPhotos();
                StartCoroutine(PuzzleComplete());
                break;
            case 4:
                editPhotoUI.SetActive(true);
                postUI.SetActive(false);
                finishEditUI.SetActive(true);
                photoImage.sprite = choosedPicture;
                brightnessLevel = 0;
                colorLevel = 0;
                beautifyLevel = 0;
                break;
            case 5:
                heartImage.enabled = true;
                SetLikes();
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
