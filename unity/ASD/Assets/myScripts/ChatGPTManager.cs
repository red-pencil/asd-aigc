using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OpenAI;

public class ChatGPTManager : MonoBehaviour
{
    [Header("Private:")]
    [SerializeField] private List<ChatMessage> messages = new List<ChatMessage>();
    [SerializeField] private List<string> conversation = new List<string>();
    [SerializeField] private Vector3 point;
    public OnResponseEvent OnResponse;

    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> {}

    private OpenAIApi openAI = new OpenAIApi();
    
    
    
    public async void AskChatGPT(string newText)
    {
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";

        messages.Add(newMessage);
        conversation.Add(newMessage.Content);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);

        if(response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            Debug.Log(chatResponse.Content);
            conversation.Add(chatResponse.Content);

            OnResponse.Invoke(chatResponse.Content);

        }


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
