using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CExperimentConfigurationTrial
{
    public int trialNumber;
    public bool videoActivated;
    public bool audioActivated;
    public float rotationGain;
    public string HRTF;
    public int spatializationMode;          //0 HIGH_QUALITY,  1 HIGH_PERFORMANCE, 2 NONE
    public bool modeILD;
    //public bool distanceAttenuation;
    //public bool farLPF;
    //public float anechoicAttenuation;
    public bool customITDActivated;
    public string typeITD;
    //more
}

[System.Serializable]
public class CExperimentConfigurationBlock
{
    public int blockNumber;
    public string blockDescription;
    public List<CExperimentConfigurationTrial> trials;

    public CExperimentConfigurationBlock()
    {
        trials = new List<CExperimentConfigurationTrial>();
    }
}

[System.Serializable]
public class CExperimentConfigurationOneParticipant
{
    //public string experimentTitle;
    //public string experimentDescription;
    //public string dateTime;
    public int participantId;
    public List<CExperimentConfigurationBlock> blocks;

    public CExperimentConfigurationOneParticipant()
    {
        //Constructor
        //Init here structures or lists
        blocks = new List<CExperimentConfigurationBlock>();
    }
}

[System.Serializable]
public class CExperimentConfiguration
{
    public string experimentTitle;
    public string experimentDescription;
    public string dateTime;    
    public List<CExperimentConfigurationOneParticipant> participantList;

    public CExperimentConfiguration()
    {
        //Constructor
        //Init here structures or lists
        participantList = new List<CExperimentConfigurationOneParticipant>();
    }
}