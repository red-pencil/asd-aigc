using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Globalization;


namespace OpenAI
{
    public class AIDraw : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private InputField inputField;
        public List<InputField> inputFieldArray = new List<InputField>();
        public List<string> promptArray = new List<string>();
        [SerializeField] private Image image;
        public List<Image> imageArray= new List<Image>();
        [SerializeField] private GameObject loadingLabel;
        [SerializeField] private GameObject storyOject;
        [SerializeField] private StoryScript storyScript;

        [SerializeField] private GameObject faceswapObject;
        [SerializeField] private GameObject pageObject;

        private int pageCount = 0;
        

        private OpenAIApi openai = new OpenAIApi();

        private void Start()
        {
            button.onClick.AddListener(SendImageRequest);

            // for(int i = 0; i < inputFieldArray.Count; i++)
            // {
            //     promptArray.Add(inputFieldArray[i].text);
            // }
        }

        private void Update()
        {
            // for (int i = 0; i < inputFieldArray.Count; i++)
            // {
            //     promptArray[i] = inputFieldArray[i].text;
            // }
        }

        public void ReadFromJson()
        {
            storyScript = storyOject.GetComponent<StoryIO>().slides;
            foreach (PageScript pageScript in storyScript.pages)
            {
                promptArray.Add(pageScript.prompt);
                
            }
            pageCount = promptArray.Count;
        }

        public async void SendRequest(int i)
        {
            imageArray[i].sprite = null;
                
            // inputFieldArray[i].enabled = false;
            GameObject loadingLabelNow = imageArray[i].transform.GetChild(0).gameObject;
            loadingLabelNow.SetActive(true);

            var response = await openai.CreateImage(new CreateImageRequest
            {
                Prompt = promptArray[i],
                Size = ImageSize.Size512
            });

            if (response.Data != null && response.Data.Count > 0)
            {
                using(var request = new UnityWebRequest(response.Data[0].Url))
                {
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SetRequestHeader("Access-Control-Allow-Origin", "*");
                    request.SendWebRequest();

                    while (!request.isDone) await Task.Yield();

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(request.downloadHandler.data);

                    

                    var sprite = Sprite.Create(texture, new Rect(0, 0, 512, 512), Vector2.zero, 1f);
                    imageArray[i].sprite = sprite;

                    SaveTextureAsPNG(texture, i);
                    faceswapObject.GetComponent<FaceSwap>().FunsionFaceInput(texture, i);
                }
                Debug.Log("Draw " + i.ToString() + " : " + promptArray[i]);

            }
            else
            {
                Debug.LogWarning("No image was created from this prompt.");
            }

            
            // inputFieldArray[i].enabled = true;
            loadingLabelNow.SetActive(false);
        }


        public async void SendImageRequest()
        {
            button.enabled = false;
                
            for (int i = 0; i < pageCount; i++)
            {
                imageArray[i].sprite = null;
                
                // inputFieldArray[i].enabled = false;
                GameObject loadingLabelNow = imageArray[i].transform.GetChild(0).gameObject;
                loadingLabelNow.SetActive(true);

                var response = await openai.CreateImage(new CreateImageRequest
                {
                    Prompt = promptArray[i],
                    Size = ImageSize.Size512
                });

                if (response.Data != null && response.Data.Count > 0)
                {
                    using(var request = new UnityWebRequest(response.Data[0].Url))
                    {
                        request.downloadHandler = new DownloadHandlerBuffer();
                        request.SetRequestHeader("Access-Control-Allow-Origin", "*");
                        request.SendWebRequest();

                        while (!request.isDone) await Task.Yield();

                        Texture2D texture = new Texture2D(2, 2);
                        texture.LoadImage(request.downloadHandler.data);

                        

                        var sprite = Sprite.Create(texture, new Rect(0, 0, 512, 512), Vector2.zero, 1f);
                        imageArray[i].sprite = sprite;

                        SaveTextureAsPNG(texture, i);
                        faceswapObject.GetComponent<FaceSwap>().FunsionFaceInput(texture, i);
                    }
                    Debug.Log("Draw " + i.ToString() + " : " + promptArray[i]);

                }
                else
                {
                    Debug.LogWarning("No image was created from this prompt.");
                }

                
                // inputFieldArray[i].enabled = true;
                loadingLabelNow.SetActive(false);

            }

            button.enabled = true;
        }

        public void RefreshRequest()
        {
            SendRequest(pageObject.GetComponent<storyPageSwitch>().activePageIndex - 1);
        }

        public static void SaveTextureAsPNG(Texture2D _texture, int order, string _fullPath = "./Assets/MyData/AIGC/")
        {
            byte[] _bytes =_texture.EncodeToPNG();
            System.IO.File.WriteAllBytes(_fullPath + DateTime.Now.ToString("yyyyMMddhhmm") + "aigc" + order.ToString() + ".PNG", _bytes);
            Debug.Log(_bytes.Length/1024  + "Kb was saved as: " + _fullPath + DateTime.Now.ToString());
        }
    }
}
