using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExperimentConfigFilesDownloader : MonoBehaviour
{

    CExperimentConfiguration experimentConfiguration;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

        //CExperimentConfiguration tempExperiment = new CExperimentConfiguration();

        //tempExperiment.dateTime = System.DateTime.Now.ToString();
        //tempExperiment.experimentTitle = "Rotation Gain experiment";
        //tempExperiment.experimentDescription = "Rotation Gain experiment...";

        //CExperimentConfigurationOneParticipant tempOneParticipant = new CExperimentConfigurationOneParticipant();
        //tempOneParticipant.participantId = 1;

        //CExperimentConfigurationBlock tempBlock = new CExperimentConfigurationBlock();
        //tempBlock.blockNumber = 1;

        //CExperimentConfigurationTrial tempTrial = new CExperimentConfigurationTrial();
        //tempTrial.trialNumber = 1;
        //tempTrial.audioActivated = true;
        //tempTrial.videoActivated = false;
        //tempTrial.spatializationMode = 0;
        //tempTrial.customITDActivated = true;
        //tempTrial.typeITD = "individual";

        //tempBlock.trials.Add(tempTrial);

        //tempOneParticipant.blocks.Add(tempBlock);
        //tempExperiment.participantList.Add(tempOneParticipant);

        //Debug.Log(JsonUtility.ToJson(tempExperiment));

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DowndloadExperimentConfigurationJsonFile(string url)
    {
        StartCoroutine(CoroutineDowndloadExperimentConfigurationJsonFile(url));    //Download and Load the product model from web-server             
    }
    
        
    IEnumerator CoroutineDowndloadExperimentConfigurationJsonFile(string url)
    {
        //WWW request = new WWW(productModelUrl);
        UnityWebRequest fileWWW = UnityWebRequest.Get(url);
        fileWWW.SendWebRequest();

        //Show downloaded progress
        while (!fileWWW.isDone)
        {
            Debug.Log("Loading Experiment configuration: " + Mathf.Round(fileWWW.downloadProgress * 100.0f).ToString() + "%");
            yield return new WaitForSeconds(0.1f);
        }


        //Has been download correctly?
        if (fileWWW.isNetworkError || fileWWW.isHttpError)
        {
            string errorMessage = "Error: Experiment Configuration cannot be download from the URL specified." + fileWWW.error;
            Debug.LogError("WWW error: " + url + " : " + fileWWW.error);         
        }
        else
        {
            experimentConfiguration = JsonUtility.FromJson<CExperimentConfiguration>(fileWWW.downloadHandler.text);   //Pass to class object from the JSON file downloaded

            if (experimentConfiguration == null)
            {
                Debug.Log("Error: The product model has a bad format");
            } else
            {
                Debug.Log("Experiment configuration loaded correctly!!!");
                ActionsAfterLoadExperimentConfiguration();
            }
        }
    }

    void ActionsAfterLoadExperimentConfiguration()
    {
        Debug.Log(experimentConfiguration.experimentTitle);
        // Doing anything        
        // For example, sendding the data to another method of another class
        this.GetComponent<ExperimentLogic>().ActionsAfterLoadExperiment(experimentConfiguration);
    }
}
