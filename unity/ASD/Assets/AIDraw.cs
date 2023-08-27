using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;

using System.Collections.Generic;


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

        private OpenAIApi openai = new OpenAIApi();

        private void Start()
        {
            button.onClick.AddListener(SendImageRequest);

            for(int i = 0; i < inputFieldArray.Count; i++)
            {
                promptArray.Add(inputFieldArray[i].text);
            }
        }

        private void Update()
        {
            for (int i = 0; i < inputFieldArray.Count; i++)
            {
                promptArray[i] = inputFieldArray[i].text;
            }
        }

        private async void SendImageRequest()
        {
            button.enabled = false;
                
            for (int i = 0; i < inputFieldArray.Count; i++)
            {
                imageArray[i].sprite = null;
                
                inputFieldArray[i].enabled = false;
                GameObject loadingLabelNow = imageArray[i].transform.GetChild(0).gameObject;
                loadingLabelNow.SetActive(true);

                var response = await openai.CreateImage(new CreateImageRequest
                {
                    Prompt = promptArray[i],
                    Size = ImageSize.Size256
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
                        var sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), Vector2.zero, 1f);
                        imageArray[i].sprite = sprite;
                    }
                }
                else
                {
                    Debug.LogWarning("No image was created from this prompt.");
                }

                
                inputFieldArray[i].enabled = true;
                loadingLabelNow.SetActive(false);

            }

            button.enabled = true;
        }
    }
}
