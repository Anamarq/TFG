using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using API_3DTI_Common;

public class TrialConfigurator : MonoBehaviour
{
    readonly float factor = 1.2f;

    //public void SetTrial(API_3DTI_Spatializer api_3DTI_Spatializer, CExperimentConfigurationOneParticipant experimentConfiguration, CParticipantData participantData, int trialNumber)
    //{

    //    float _rotationGain = experimentConfiguration.blocks[participantData.blockNumber - 1].trials[trialNumber - 1].rotationGain;
    //    string _typeITD = experimentConfiguration.blocks[participantData.blockNumber - 1].trials[trialNumber - 1].typeITD;
    //    //Codigo de Ana:
    //    GetComponent<ActVideo>().ActivateVideo(experimentConfiguration.blocks[participantData.blockNumber - 1].trials[trialNumber - 1].videoActivated);
    //    GetComponent<StartAp>().SetGain(_rotationGain);
    //    //Codigo de prueba local:       
    //    //Set_Gain(_rotationGain);
    //    //Activate_Video(experimentConfiguration.blocks[participantData.blockNumber - 1].trials[trialNumber - 1].videoActivated);

    //    if (api_3DTI_Spatializer.SetSpatializationMode(experimentConfiguration.blocks[participantData.blockNumber - 1].trials[trialNumber - 1].spatializationMode) == false)
    //    {
    //        Debug.LogError("Spatialisation selection error.");
    //    }

    //    if (api_3DTI_Spatializer.SetHeadRadius(GetHeadRadius(experimentConfiguration.blocks[participantData.blockNumber - 1].trials[trialNumber - 1].typeITD, participantData.GetHeadRadius())) == false)
    //    {
    //        Debug.LogError("SetHeadRadius error.");
    //    }

    //    if (api_3DTI_Spatializer.SetCustomITD(experimentConfiguration.blocks[participantData.blockNumber - 1].trials[trialNumber - 1].customITDActivated) == false)
    //    {
    //        Debug.LogError("Custom ITD activation error.");
    //    }


    //    float _headCircunference = participantData.headCircunference;
    //    this.GetComponent<ExperimentResults>().SetTrialConditions(participantData.id_participant, participantData.blockNumber, experimentConfiguration.blocks[participantData.blockNumber - 1].blockDescription, trialNumber, _rotationGain, _typeITD, _headCircunference);
    //    //TODO check if not error
    //    GetComponent<StartAp>().StartTrial();
    //}


    public void SetTrial2(API_3DTI_Spatializer api_3DTI_Spatializer, CExperimentConfigurationOneParticipant experimentConfiguration, CParticipantData participantData, int trialNumber)
    {

        CExperimentConfigurationTrial _trialData = experimentConfiguration.blocks.Find(r => r.blockNumber == participantData.blockNumber).trials.Find(r => r.trialNumber == trialNumber);

        float _rotationGain = _trialData.rotationGain;        
        string _typeITD = _trialData.typeITD;        
        

        //Codigo de Ana:
        GetComponent<ActVideo>().ActivateVideo(_trialData.videoActivated);
        GetComponent<StartAp>().SetGain(_trialData.rotationGain);
        
        //Codigo de prueba local:       
        Set_Gain(_trialData.rotationGain);
        Activate_Video(_trialData.videoActivated);

        if (api_3DTI_Spatializer.SetSpatializationMode(_trialData.spatializationMode) == false)
        {
            Debug.LogError("Spatialisation selection error.");
        }

        if (api_3DTI_Spatializer.SetHeadRadius(GetHeadRadius(_trialData.typeITD, participantData.GetHeadRadius())) == false)
        {
            Debug.LogError("SetHeadRadius error.");
        }

        if (api_3DTI_Spatializer.SetCustomITD(_trialData.customITDActivated) == false)
        {
            Debug.LogError("Custom ITD activation error.");
        }


        float _headCircunference = participantData.headCircunference;
        string _blockDescription = experimentConfiguration.blocks.Find(r=> r.blockNumber == participantData.blockNumber).blockDescription;
        this.GetComponent<ExperimentResults>().SetTrialConditions(participantData.id_participant, participantData.blockNumber, _blockDescription, trialNumber, _rotationGain, _typeITD, _headCircunference);

        //TODO check if not error

        bool _audioStart = true;
        if (_trialData.spatializationMode == 2) { _audioStart = false; }

        GetComponent<StartAp>().StartTrial(trialNumber, _audioStart);
    }

    private float GetHeadRadius(string typeITD, float headRadius)
    {
        if (typeITD.Equals("individual+"))
            return headRadius*factor;
        else if (typeITD.Equals("individual-"))
            return headRadius/factor;
        else //typeITD.Equals("individual")
            return headRadius;
    }


    //---METODOS VACIOS---//

    private void Set_Gain(float rotationGain)
    {
        Debug.Log ("Rotation Gain: " + rotationGain);
    }

    private void Activate_Video(bool videoActivated)
    {
        Debug.Log ("Video activated: " + videoActivated);
    }
}