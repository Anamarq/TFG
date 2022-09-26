using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAp : MonoBehaviour
{
    public RotationGain Rotgain;
    private GameObject quest;
    private bool on = false;
    private bool endTrial = false;
    private GameObject Inicio;
    //private GameObject trainingCanvas;
    private AudioSource audioSourceRight;
    private AudioSource audioSourceLeft;
    private float newGain=1;
    private bool actButton;
    private GameObject arrowRight;
    private GameObject arrowLeft;
    private bool trainingActivated;
    private bool audioStart;

    void Awake()
    {
        actButton = false; //desactivate buttons

        quest = GameObject.FindGameObjectWithTag("Question");
        quest.SetActive(false);
        
        audioSourceRight = 
            GameObject.FindGameObjectWithTag("AudioSourceRight")
            .GetComponent<AudioSource>();
        audioSourceLeft = 
            GameObject.FindGameObjectWithTag("AudioSourceLeft").GetComponent<AudioSource>();


        Inicio = GameObject.FindGameObjectWithTag("Inicio");
        Inicio.SetActive(false);

        //trainingCanvas = GameObject.FindGameObjectWithTag("training");

        arrowRight = GameObject.FindGameObjectWithTag("ArrowRight"); 
        arrowRight.SetActive(false);

        arrowLeft = GameObject.FindGameObjectWithTag("ArrowLeft");
        arrowLeft.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        if (actButton==true && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))  //Buttton pressed, after turning and watching to the speaker
        {
            quest.SetActive(true); //Active question
        }
        /*
        if (Input.GetKeyUp(KeyCode.Space)) { OnClickStartButton(); }
        if (Input.GetKeyUp(KeyCode.Return)) {            
            answerYes();
        }*/

    }
    public void StartTrial(int _trialNumber, bool _audioStart)
    {

        //on = true;
        Inicio.SetActive(true);

        if (_trialNumber<0) {
            trainingActivated = true;
        } else
        {
            trainingActivated = false;
        }
        audioStart = _audioStart;
    }

    public void answerYes()
    {
        Debug.Log("YES");
        actButton = false; //desactive button question
        quest.SetActive(false);
        arrowRight.SetActive(false);//desact arrow
        arrowLeft.SetActive(false);

        this.GetComponent<ExperimentResults>().EndTrial("yes");
        EndTrial();
    }
    public void answerNO()
    {
        Debug.Log("NO");
        actButton = false; //desactive button question
        quest.SetActive(false);
        arrowRight.SetActive(false);
        arrowLeft.SetActive(false);
        this.GetComponent<ExperimentResults>().EndTrial("no");
        EndTrial();

    }
    public void SetGain(float gain)
    {
        newGain = gain;
    }
    public void OnClickStartButton()
    {
        Inicio.SetActive(false);
        actButton = true; //active button question
        float decisionNumber = Random.Range(0, 2);
        Debug.Log("Random decision: " + decisionNumber);

         bool boolValue = (decisionNumber == 0);


        if (boolValue)
        {
            if (audioStart) { audioSourceRight.Play(); }
            arrowRight.SetActive(true);
        }
        else 
        {
            if (audioStart) { audioSourceLeft.Play();}
            arrowLeft.SetActive(true); 
        }
        
        
        
        
        Rotgain.setGain(newGain);

        this.GetComponent<ExperimentResults>().StarTrial();
    }

    private void EndTrial()
    {
        //trainingCanvas.SetActive(false);
        audioSourceRight.Stop();
        audioSourceLeft.Stop();
        GetComponent<ExperimentLogic>().NextTrial();
        //on = false;
    }
}