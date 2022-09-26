using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CExperimentDataCollection
{
    public string participantId;   
    //public DateTime startDate;
    //public DateTime finishDate;
    //public double timeTaken;
    public List<CExperimentTrial> trials;

    public CExperimentDataCollection()
    {
        //startDate = DateTime.Now;
        //timeTaken = 0.0d;
        participantId = "";
        //description = "";
        trials = new List<CExperimentTrial>();
    }
}
public class CExperimentBlocks
{

}
public class CExperimentTrial
{
    public int participantId;
    public int trialNumber;
    public int blockNumber;
    public string blockDescription;
    public DateTime startDate;
    public DateTime finishDate;
    public double timeTaken;    
    //public bool videoActivated;
    //public bool audioActivated;
    public float rotationGain;
    //public int spatializationMode;
    public string typeITD;
    public string questionAnswer;
    public float headCircunference;
}


public class ExperimentResults : MonoBehaviour
{
    System.Diagnostics.Stopwatch trialTimer;

    CExperimentTrial experimentTrialData;
    bool initiated;
    // Start is called before the first frame update
    void Awake()
    {
        trialTimer = new System.Diagnostics.Stopwatch();
        initiated = false;
    }

  
    public void SetTrialConditions(int _participantId, int _blockNumber, string _blockDescription, int _trialNumber, float _rotationGain, string _typeITD, float _headCircunference)
    {        
        experimentTrialData = new CExperimentTrial();        
        experimentTrialData.participantId = _participantId;
        experimentTrialData.blockNumber = _blockNumber;
        experimentTrialData.blockDescription = _blockDescription;
        experimentTrialData.trialNumber = _trialNumber;
        experimentTrialData.rotationGain = _rotationGain;
        experimentTrialData.typeITD = _typeITD;
        experimentTrialData.headCircunference = _headCircunference;
        initiated = true;

    }
    public void StarTrial()
    {
        if (initiated)
        {
            double timeTaken = GetRestartTimer(trialTimer);
            experimentTrialData.startDate = DateTime.Now;
        }        
    }

    public void EndTrial(string _questionAnswer)
    {
        if (initiated)
        {
            experimentTrialData.timeTaken = GetRestartTimer(trialTimer);
            experimentTrialData.finishDate = DateTime.Now;
            experimentTrialData.questionAnswer = _questionAnswer;
            if (experimentTrialData.trialNumber>0) { SaveTrialsFile(); }            
            initiated = false;
        }
    }

    ////////////
    // TIMER  //
    ////////////
    private double GetRestartTimer(System.Diagnostics.Stopwatch _timer)
    {
        double totalTime = 0.0f;
        if (_timer.IsRunning)
        {
            _timer.Stop();
            totalTime = _timer.Elapsed.TotalSeconds;
        }
        _timer.Reset();
        _timer.Start();
        return totalTime;
    }


    private void StopTimer(System.Diagnostics.Stopwatch _timer)
    {
        if (_timer.IsRunning)
        {
            _timer.Stop();
            _timer.Reset();
        }
    }

    /////////////////////////////
    // Write Trial File
    /////////////////////////////
    private void SaveTrialsFile()
    {
        FileStream file;
        StreamWriter writer;
        string fileName = "Exp_Results_Data";
        if (FileExists(fileName + ".csv"))
        {
            OpenFile(fileName, out file, out writer);
        }
        else
        {
            CreateFile(fileName, out file, out writer);
            WriteFileHeader(writer);      //Write header     
        }
        WriteFileDataLines(writer);
        writer.Close();
    }

    private void WriteFileHeader(StreamWriter writer)
    {
        //Write header
        writeFileLine(writer, "ParticipantID;BlockNumber;trialNumber;Begin;End;Time taken (sec);BlockDescription;RotationGain;TypeITD;HeadCircunference (m);Question;");
    }

    private void WriteFileDataLines(StreamWriter writer)
    {
        string line;
        line = experimentTrialData.participantId.ToString() + ";";
        line += experimentTrialData.blockNumber.ToString() + ";";
        line += experimentTrialData.trialNumber.ToString() + ";";
        line += experimentTrialData.startDate + ";";
        line += experimentTrialData.finishDate + ";";
        line += experimentTrialData.timeTaken.ToString().Replace('.', ',') + ";";
        line += experimentTrialData.blockDescription + ";";
        line += experimentTrialData.rotationGain.ToString().Replace('.', ',') + ";";
        line += experimentTrialData.typeITD + ";";
        line += experimentTrialData.headCircunference + ";";
        line += experimentTrialData.questionAnswer.ToString() + ";";
        writeFileLine(writer, line);

    }

    ////////////////////
    /// FILE CREATION
    ////////////////////
    private string DoubleToStringExcel(double value)
    {
        return value.ToString().Replace('.', ',');
    }

    private void CreateFile(string fileName, out FileStream _file, out StreamWriter _writer)
    {
        string basePath = "";
        //if (hom3r.state.platform == THom3rPlatform.Android) { basePath = Application.persistentDataPath + "/"; }
        int i = 1;
        while (File.Exists(basePath + fileName + "_" + i.ToString() + ".csv"))
        {
            i++;
        }
        // If repeated we add a number at the end
        string temp = "";
        if (i > 1) { temp = "_" + i.ToString(); }
        // create file
        FileStream file = File.Open(basePath + fileName + temp + ".csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter writer = new StreamWriter(file);

        _file = file;
        _writer = writer;
    }

    private void writeFileLine(StreamWriter writer, string line)
    {
        writer.WriteLine(line);
    }


    private void OpenFile(string fileName, out FileStream _file, out StreamWriter _writer)
    {
        string basePath = "";
        //if (hom3r.state.platform == THom3rPlatform.Android) { basePath = Application.persistentDataPath + "/"; }       
        FileStream file = File.Open(basePath + fileName + ".csv", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(file);

        _file = file;
        _writer = writer;
    }

    private bool FileExists(string fileName)
    {
        string basePath = "";
        //if (hom3r.state.platform == THom3rPlatform.Android) { basePath = Application.persistentDataPath + "/"; }
        return File.Exists(basePath + fileName);
    }
}
