using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class MyChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        
        public GameObject promptOject;
        [SerializeField] private string prompt = "";
        [SerializeField] private string promptTemplate;

        public string messageSent, messageReply;

        private void Start()
        {
            promptTemplate = promptOject.GetComponent<PromptIO>().promptFull;
            // promptTemplate = "What's your version?";
            
            SendReplyAuto(promptTemplate);
            button.onClick.AddListener(SendReply);
        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendToGPT()
        {
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message); 
                Debug.Log("[Reply] " + message.Content);
                messageReply = message.Content;

                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }

        private void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            messages.Add(newMessage);

            Debug.Log("[Sent] " + newMessage.Content);
            messageSent = newMessage.Content;
            SendToGPT();
        }

        private void SendReplyAuto(string promptAuto)
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            if (messages.Count == 0) newMessage.Content = promptAuto; 

            AppendMessage(newMessage);
            
            messages.Add(newMessage);

            Debug.Log("[SentAuto] " + newMessage.Content);
            messageSent = newMessage.Content;
            SendToGPT();
        }
    
    }
}
