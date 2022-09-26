using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using API_3DTI_Common;
using System;
using UnityEngine.EventSystems;

public class ExperimentLogic : MonoBehaviour
{
    public API_3DTI_Spatializer api_3DTI_Spatializer;
    CExperimentConfiguration experimentConfiguration;
    CParticipantData participantData;
    private GameObject canvasData; //ANA
    private GameObject eventSystem; //ANA
    private GameObject end;


    int trialNumber = -2;   //Because we have two trial for trainning


    void Awake()
    {
        api_3DTI_Spatializer = Camera.main.GetComponent<API_3DTI_Spatializer>();
        canvasData = GameObject.FindGameObjectWithTag("canvasData");  //ANA
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem"); //ANA
        end = GameObject.FindGameObjectWithTag("Fin"); //ANA
        eventSystem.GetComponent<OVRInputModule>().enabled = false; //ANA 
        end.SetActive(false); //ANA

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadExperimentConfigurationFile_fromdiskfile();


    }


    // Update is called once per frame
    void Update()
    {
    }

    void LoadExperimentConfigurationFile_fromdiskfile()
    {
        string modelsPath = Application.dataPath.Substring(0, Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")).LastIndexOf("/"));
        //string fileName = "file2.json";
        string fileName = "Experiment_GainOfRotation.json";
        string url = "file:///" + modelsPath + "/ExperimentConfigurationFiles/" + fileName;


        LoadExperimentConfigurationFile(url);

    }

    void LoadExperimentConfigurationFile(string fileUrl)
    {
        Debug.Log("Trying to download file: " + fileUrl);
        this.GetComponent<ExperimentConfigFilesDownloader>().DowndloadExperimentConfigurationJsonFile(fileUrl);
    }

    public void ActionsAfterLoadExperiment(CExperimentConfiguration _experimentConfiguration)
    {
        experimentConfiguration = _experimentConfiguration;


    }

    public void NextTrial()
    {
        Debug.Log("NEXT TRIAL");
        /*
        if (trialNumber == 18 && participantData.blockNumber < 3)
        {
            //SaveBlock();
            participantData.blockNumber++;
            trialNumber = 0;
            Debug.Log("Block Nr: " + participantData.blockNumber);

        }
        else if (participantData.blockNumber == 4)
        {
            //SaveBlock(); 
            Debug.Log("END OF EXPERIMENT");
            

        }
        */

        if (trialNumber < 18)
        {
            trialNumber++;

            Debug.Log("Trial Nr: " + trialNumber);
            //this.GetComponent<TrialConfigurator>().SetTrial(api_3DTI_Spatializer, experimentConfiguration.participantList[participantData.id_participant - 1], participantData, trialNumber);

            CExperimentConfigurationOneParticipant _participantData = experimentConfiguration.participantList.Find(r => r.participantId == participantData.id_participant);
            this.GetComponent<TrialConfigurator>().SetTrial2(api_3DTI_Spatializer, _participantData, participantData, trialNumber);
        }
        else    //End of block
        {
            end.SetActive(true); //ANA

        }
    }

    //ANA
    public void CallPartData()
    {

        participantData = Get_Participant_Data();

        eventSystem.GetComponent<OVRInputModule>().enabled = true;  //Active OVR input module 

        canvasData.SetActive(false);

        //Printing on screen the Experiment information
        Debug.Log("Participant ID: " + participantData.id_participant);
        Debug.Log("Head Radius: " + participantData.GetHeadRadius());
        Debug.Log("Block Nr: " + participantData.blockNumber);



        NextTrial();
    }
    //ANA



    private CParticipantData Get_Participant_Data()
    {
        //Read info participant
        GameObject id, block, head;
        id = GameObject.FindGameObjectWithTag("ID");
        int userID = int.Parse(id.gameObject.transform.Find("Text").GetComponent<Text>().text, System.Globalization.CultureInfo.InvariantCulture);

        block = GameObject.FindGameObjectWithTag("BLOCK");
        int userBlock = int.Parse(block.gameObject.transform.Find("Text").GetComponent<Text>().text, System.Globalization.CultureInfo.InvariantCulture);

        head = GameObject.FindGameObjectWithTag("HEAD");
        float userHead = float.Parse(head.gameObject.transform.Find("Text").GetComponent<Text>().text, System.Globalization.CultureInfo.InvariantCulture);

        return new CParticipantData(userID, userBlock, (userHead / 100));
        //return new CParticipantData(1, 2, 0.3f);

    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}






