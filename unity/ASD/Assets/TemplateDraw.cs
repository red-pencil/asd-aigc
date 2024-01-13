using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;


public class TemplateDraw : MonoBehaviour
{
    public List<string> pathArray = new List<string>();
    public int pageCount = 26;
    public string templateName = "";

    public List<Image> imageArray = new List<Image>();
    public List<RawImage> rawimgArray = new List<RawImage>();
    // public int pageTotal = 26;

    [SerializeField] private GameObject storyObject;
    [SerializeField] private StoryScript storyScript;
    [SerializeField] private GameObject faceswapObject;
    [SerializeField] private GameObject pageSwitchObject;
    [SerializeField] private int currentPageIndex;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPageIndex = pageSwitchObject.GetComponent<storyPageSwitch>().activePageIndex - 1;
    }


    public async void DrawFromTemplate()
    {
        
        for (int i = 1; i < pageCount; i++)
        {
            Texture2D texture = new Texture2D(2, 2);

            byte[] imgBytes = System.IO.File.ReadAllBytes("./Assets/MyTemplates/" + templateName + '/' + i.ToString() + ".png");
            Debug.Log("Read Photo:" + i.ToString() );

            texture.LoadImage(imgBytes);
            //texture.Reinitialize(512, 512);
            rawimgArray[i].texture = texture;

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 1400, 1400), Vector2.zero, 1f);
            imageArray[i].sprite = sprite;

        }
        
        for (int i = 1; i < pageCount; i++)
        {
            Texture2D texture = new Texture2D(2, 2);

            byte[] imgBytes = System.IO.File.ReadAllBytes("./Assets/MyTemplates/" + templateName + '/' + i.ToString() + ".png");
            Debug.Log("Read Photo:" + i.ToString() );

            texture.LoadImage(imgBytes);

            Debug.Log("Swap Begin" + i.ToString() );
            faceswapObject.GetComponent<FaceSwap>().FunsionFaceInput(texture, i);
            Debug.Log("Swap End" + i.ToString() );
            Debug.Log("finished" + faceswapObject.GetComponent<FaceSwap>().resultCount);
            Debug.Log("began" + i);
            while (faceswapObject.GetComponent<FaceSwap>().resultCount < (i+1)) await Task.Yield();

            Debug.Log("Finish");
            // rawimgArray[i].texture = texture;
        }

        // var sprite = Sprite.Create(texture, new Rect(0, 0, 512, 512), Vector2.zero, 1f);
        // imageArray[i].sprite = sprite;
        // image texture2d
        // rawimage texture

        
    }

    public void SwapEmotion(int emotionIndex)
    {
        int i = currentPageIndex;
        Texture2D texture = new Texture2D(2, 2);

        byte[] imgBytes = System.IO.File.ReadAllBytes("./Assets/MyTemplates/" + templateName + '/' + i.ToString() + ".png");
        Debug.Log("Read Photo:" + i.ToString() );

        texture.LoadImage(imgBytes);

        Debug.Log("Swap Begin" + i.ToString() );
        faceswapObject.GetComponent<FaceSwap>().FunsionFaceInputEmotion(texture, i, emotionIndex);
        Debug.Log("Swap End" + i.ToString() );
        Debug.Log(faceswapObject.GetComponent<FaceSwap>().resultCount);
        Debug.Log(i);
       
        Debug.Log("Finish");


    }

    public void ReadFromJson()
    {
        storyScript = storyObject.GetComponent<StoryIO>().slides;
        foreach (PageScript pageScript in storyScript.pages)
        {
            pathArray.Add(pageScript.prompt);
            // templateName = int.Parse(pageScript.templatePath);
            
        }
        // pageCount = pathArray.Count;
    }
}
