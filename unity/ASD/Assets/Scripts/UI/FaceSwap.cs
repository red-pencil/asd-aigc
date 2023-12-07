using FaceFusion;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;
using System.Threading.Tasks;

public class FaceSwap : MonoBehaviour
{
    /// <summary>
    /// 显示相机渲染的画面
    /// </summary>
    [SerializeField] RawImage rawImg;
    /// <summary>
    /// 融合模板图
    /// </summary>
    /// 
    [SerializeField] Texture2D curTex;
    [SerializeField] Texture2D[] curTexArray;
    
    [SerializeField] Texture2D targetFusionTex;
    [SerializeField] Texture2D[] targetFusionTexArray;
    /// <summary>
    /// 融合结果显示
    /// </summary>
    // Leon [SerializeField] RawImage resultImg;
    [SerializeField] RawImage[] resultImgArray;

    /// <summary>
    /// 截图保存的路径
    /// </summary>
    private string path;

    public int count = 0;
    public int resultCount = 0;
    public int globalIndex = 0;

    void Start()
    {
        path = Application.streamingAssetsPath + "/";
        // leon
        // WebCamera.Instance.InitCamera(800,600);
        // WebCamera.Instance.OpenCamera();
        // rawImg.texture = WebCamera.Instance.renderTex;
    }

    private void FunsionFace()
    {
        // WebCamera.Instance.SaveScreenShot(path);  //leon
        // var curTex = WebCamera.Instance.lastShotText;  //leon
        

        //序列化字典内容到json格式 上传到百度ai
        Dictionary<string,object> dict = new Dictionary<string,object>();
        dict.Add("version","4.0");
        dict.Add("alpha",0);
        ImageInfo imgTemplate = new ImageInfo();
         //
        Texture2D decopmpresseTex = targetFusionTex.DeCompress();
        //
        imgTemplate.image = FaceMerge.Instance.Texture2DToBase64(decopmpresseTex);
        imgTemplate.image_type = "BASE64";
        imgTemplate.quality_control = "NONE";
        dict.Add("image_template", imgTemplate);
        ImageInfo imgTarget = new ImageInfo();
       
        imgTarget.image = FaceMerge.Instance.Texture2DToBase64(curTex);
        imgTarget.image_type = "BASE64";
        imgTarget.quality_control = "NONE";
        dict.Add("image_target",imgTarget);
        dict.Add("merge_degree", "COMPLETE");

        string json = JsonMapper.ToJson(dict); // 反序列化用了 litjson的工具，使用JsonUtility序列化dict会是空的

        FaceMerge.Instance.PostFaceMerge(json, OnFaceMerge);

    }

    private void FunsionFaceArray()
    {
        // WebCamera.Instance.SaveScreenShot(path);  //leon
        // var curTex = WebCamera.Instance.lastShotText;  //leon
        

        //序列化字典内容到json格式 上传到百度ai
        Dictionary<string,object> dict = new Dictionary<string,object>();
        dict.Add("version","4.0");
        dict.Add("alpha",0);
        ImageInfo imgTemplate = new ImageInfo();
         //
        Texture2D decopmpresseTex = targetFusionTexArray[count].DeCompress();
        //
        imgTemplate.image = FaceMerge.Instance.Texture2DToBase64(decopmpresseTex);
        imgTemplate.image_type = "BASE64";
        imgTemplate.quality_control = "NONE";
        dict.Add("image_template", imgTemplate);
        ImageInfo imgTarget = new ImageInfo();
       
        imgTarget.image = FaceMerge.Instance.Texture2DToBase64(curTex);
        imgTarget.image_type = "BASE64";
        imgTarget.quality_control = "NONE";
        dict.Add("image_target",imgTarget);
        dict.Add("merge_degree", "COMPLETE");

        string json = JsonMapper.ToJson(dict); // 反序列化用了 litjson的工具，使用JsonUtility序列化dict会是空的

        FaceMerge.Instance.PostFaceMerge(json, OnFaceMerge);

    }

    public void FunsionFaceInput(Texture2D inputTexture, int index)
    {
        // WebCamera.Instance.SaveScreenShot(path);  //leon
        // var curTex = WebCamera.Instance.lastShotText;  //leon
        globalIndex = index;
        
        Debug.Log("swap begin");
        resultImgArray[globalIndex].texture = inputTexture;

        //序列化字典内容到json格式 上传到百度ai
        Dictionary<string,object> dict = new Dictionary<string,object>();
        dict.Add("version","4.0");
        dict.Add("alpha",0);
        ImageInfo imgTemplate = new ImageInfo();
        //
        Texture2D decopmpresseTex = inputTexture.DeCompress();
        //


        imgTemplate.image = FaceMerge.Instance.Texture2DToBase64(decopmpresseTex);
        imgTemplate.image_type = "BASE64";
        imgTemplate.quality_control = "NONE";
        dict.Add("image_template", imgTemplate);
        ImageInfo imgTarget = new ImageInfo();
       
        imgTarget.image = FaceMerge.Instance.Texture2DToBase64(curTex);
        imgTarget.image_type = "BASE64";
        imgTarget.quality_control = "NONE";
        dict.Add("image_target", imgTarget);
        dict.Add("merge_degree", "COMPLETE");

        string json = JsonMapper.ToJson(dict); // 反序列化用了 litjson的工具，使用JsonUtility序列化dict会是空的

        FaceMerge.Instance.PostFaceMerge(json, OnFaceMergeArray);

        Debug.Log("swap finish");

        //return resultImg.texture;

    }

    
    public void FunsionFaceInputEmotion(Texture2D inputTexture, int index, int emotionIndex = 0)
    {
        // WebCamera.Instance.SaveScreenShot(path);  //leon
        // var curTex = WebCamera.Instance.lastShotText;  //leon
        globalIndex = index;
        
        Debug.Log("swap begin");
        resultImgArray[globalIndex].texture = inputTexture;

        //序列化字典内容到json格式 上传到百度ai
        Dictionary<string,object> dict = new Dictionary<string,object>();
        dict.Add("version","4.0");
        dict.Add("alpha",0);
        ImageInfo imgTemplate = new ImageInfo();
        //
        Texture2D decompresseTex = inputTexture.DeCompress();
        //


        imgTemplate.image = FaceMerge.Instance.Texture2DToBase64(decompresseTex);
        imgTemplate.image_type = "BASE64";
        imgTemplate.quality_control = "NONE";
        dict.Add("image_template", imgTemplate);
        ImageInfo imgTarget = new ImageInfo();
        //
        Texture2D decompressCurTex = curTexArray[emotionIndex].DeCompress();
       
        imgTarget.image = FaceMerge.Instance.Texture2DToBase64(decompressCurTex);
        imgTarget.image_type = "BASE64";
        imgTarget.quality_control = "NONE";
        dict.Add("image_target", imgTarget);
        dict.Add("merge_degree", "COMPLETE");

        string json = JsonMapper.ToJson(dict); // 反序列化用了 litjson的工具，使用JsonUtility序列化dict会是空的

        FaceMerge.Instance.PostFaceMerge(json, OnFaceMergeEmotion);

        Debug.Log("swap finish");

        //return resultImg.texture;

    }


    private void OnFaceMerge(string info)
    {
        Debug.Log(info);
        Response response = JsonMapper.ToObject<Response>(info);
        if (response.error_code == 0) // 0 表示成功融合图片
        {
            Debug.Log(response.error_msg);

            string ImgBase64 = response.result.merge_image;

            // resultImgArray.texture = FaceMerge.Instance.Base64ToTexture2D(targetFusionTex.width, targetFusionTex.height, ImgBase64);
            Debug.Log("result count=" + resultCount.ToString());
            if (resultCount < 5)
            {
                resultCount = resultCount + 1;
            }
            else
            {
                resultCount = 0;
            }
            
            
        }
    }

    
 
    
    private void OnFaceMergeArray(string info)
    {
        Debug.Log(info);
        Response response = JsonMapper.ToObject<Response>(info);
        // while (response.error_code != 0) await Task.Yield();
        if (response.error_code == 0) // 0 表示成功融合图片
        {
            Debug.Log(response.error_msg);

            string ImgBase64 = response.result.merge_image;

            // resultImgArray[globalIndex].texture = FaceMerge.Instance.Base64ToTexture2D(targetFusionTex.width, targetFusionTex.height, ImgBase64);
            resultImgArray[resultCount].texture = FaceMerge.Instance.Base64ToTexture2D(512, 512, ImgBase64);
            Debug.Log("result count =" + resultCount.ToString());
            resultCount = resultCount + 1;
            
        } 
        else
        {
            
        }
    }

     private void OnFaceMergeEmotion(string info)
    {
        Debug.Log(info);
        Response response = JsonMapper.ToObject<Response>(info);
        // while (response.error_code != 0) await Task.Yield();
        if (response.error_code == 0) // 0 表示成功融合图片
        {
            Debug.Log(response.error_msg);

            string ImgBase64 = response.result.merge_image;

            // resultImgArray[globalIndex].texture = FaceMerge.Instance.Base64ToTexture2D(targetFusionTex.width, targetFusionTex.height, ImgBase64);
            resultImgArray[globalIndex].texture = FaceMerge.Instance.Base64ToTexture2D(512, 512, ImgBase64);
            Debug.Log("current count =" + globalIndex.ToString());
            // resultCount = resultCount + 1;
            
        } 
        else
        {
            
        }
    }

    private void OnFaceMergeArrayIndex(string info, int index)
    {
        Debug.Log(info);
        Response response = JsonMapper.ToObject<Response>(info);
        if (response.error_code == 0) // 0 表示成功融合图片
        {
            Debug.Log(response.error_msg);
            string ImgBase64 = response.result.merge_image;

            // resultImgArray[globalIndex].texture = FaceMerge.Instance.Base64ToTexture2D(targetFusionTex.width, targetFusionTex.height, ImgBase64);
            resultImgArray[index].texture = FaceMerge.Instance.Base64ToTexture2D(512, 512, ImgBase64);
            Debug.Log("result count=" + index.ToString());
            resultCount = resultCount + 1;
            
        } else
        {
            
        }
    }

    // private void OnFaceMergeArrayo(string info)
    // {
    //     Debug.Log(info);
    //     Response response = JsonMapper.ToObject<Response>(info);
    //     if (response.error_code == 0) // 0 表示成功融合图片
    //     {
    //         Debug.Log(response.error_msg);

    //         string ImgBase64 = response.result.merge_image;

    //         resultImgArray[resultCount].texture = FaceMerge.Instance.Base64ToTexture2D(targetFusionTex.width, targetFusionTex.height, ImgBase64);
    //         Debug.Log("result count=" + resultCount.ToString());
    //         if (resultCount < 5)
    //         {
    //             resultCount = resultCount + 1;
    //         }
    //         else
    //         {
    //             resultCount = 0;
    //         }
            
            
    //     }
    // }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            // leon FunsionFace();
            for(; count < 6; count++)
            {
                FunsionFaceArray();
                Debug.Log("count=" + count.ToString());
                
            }

            count = 0;
        }
    }

    public void startMerge()
    {
        for(; count < 6; count++)
            {
                FunsionFaceArray();
                Debug.Log("count=" + count.ToString());
                
            }

            count = 0;

    }

    public class ImageInfo
    {
        public string image; //图片信息
        public string image_type; //图片类型 BASE64 URL FACE_TOKEN
        public string quality_control; //质量控制 NONE LOW NORMAL HIGH HIGH
    }

}
