using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OpenAI
{
    public class StoryRefine : MonoBehaviour
    {
        [SerializeField] private string refineAdd;
        private string refineGpt;
        [SerializeField] private List<string> originalPrompt, refinedPrompt;
        public GameObject storyObject, gptObject;
        public int pageIndex = 0;
        private StoryScript slides;
        private string oldReply;
        private bool gptRefresh;

        // Start is called before the first frame update
        void Start()
        {
            refineAdd = "Chinese boy, asian children, black hair, black eye, photorealsitic, color photo, real scenario, human face, real human";
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ReadPrompt()
        {
            originalPrompt = new List<string>();
            refinedPrompt = new List<string>();
            slides = storyObject.GetComponent<StoryIO>().slides;
            pageIndex = slides.pages.Count;
            for (int i = 0; i < pageIndex; i++)
            {
                // Debug.Log(slides.pages[i].prompt); 
                // Debug.Log(originalPrompt);
                originalPrompt.Add(slides.pages[i].prompt);
                refinedPrompt.Add(slides.pages[i].prompt);
            }
        }
       
        public void WritePrompt()
        {
            for (int i = 0; i < pageIndex; i++)
            {
                slides.pages[i].prompt = refinedPrompt[i];
                Debug.Log(slides.pages[i].prompt);
            }
            storyObject.GetComponent<StoryIO>().slides = slides;
            storyObject.GetComponent<StoryIO>().SaveJson();
        }

        public void PromptAdd()
        {
            for (int i = 0; i < pageIndex; i++)
            {
                //Debug.Log(refinedPrompt[i]);
                refinedPrompt[i] = refinedPrompt[i] + refineAdd;
                //Debug.Log(refinedPrompt[i]);
            }
        }


        public void PromptGpt()
        {

            StartCoroutine(WaitGPT());
            // for (int i = 0; i < pageIndex; i++)
            // {
            //     //Debug.Log(refinedPrompt[i]);
            //     refinedPrompt[i] = refinedPrompt[i] + refineAdd;
            //     //Debug.Log(refinedPrompt[i]);
                
            //     if gptRefresh 
                
            // }
        }

        private IEnumerator WaitGPT()
        {
            for (int index = 0; index < pageIndex; index++)
            {
                

                Debug.Log ("old reply");
                Debug.Log(gptObject.GetComponent<MyChatGPT>().messageReply);
                oldReply = gptObject.GetComponent<MyChatGPT>().messageReply;
                gptObject.GetComponent<MyChatGPT>().SendReplyInput("Read this:" + refinedPrompt[index] + "Write a prompt for OpenAI Dall-E to generate an photorealistic image");
                Debug.Log ("Send to GPT");

                Debug.Log("Begin Wait");
                yield return new WaitUntil(Refresh);
                Debug.Log("End Wait");

                Debug.Log ("GPT Replyed");
                Debug.Log (gptObject.GetComponent<MyChatGPT>().messageReply);
                refinedPrompt[index] = gptObject.GetComponent<MyChatGPT>().messageReply;
                
                refinedPrompt[index] = refinedPrompt[index] + refineAdd;
                if (refinedPrompt[index].IndexOf("sorry") != -1)
                {
                    refinedPrompt[index] = originalPrompt[index] + refineAdd;
                }
                oldReply = gptObject.GetComponent<MyChatGPT>().messageReply;

                WritePrompt();

            }
            
            
            
            // while (oldReply == gptObject.GetComponent<MyChatGPT>().messageReply)
            // {
            //     yield return null;
            // }
            
        }

        private bool Refresh()
        {
            if (oldReply == gptObject.GetComponent<MyChatGPT>().messageReply)
            {
                //Debug.Log("Same");
                gptRefresh = false;
                return false;
                
            }
            else
            {
                Debug.Log("GPT Refresh");
                gptRefresh = true;
                return true;
                
            }
        }


        public void RefineWithAdd()
        {
            ReadPrompt();
            PromptAdd();
            WritePrompt();
        }

        public void RefineWithGpt()
        {
            gptRefresh = true;
            ReadPrompt();
            PromptGpt();
            
        }




    }

}