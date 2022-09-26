using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CParticipantData
{
    public int id_participant;
    public int blockNumber;
    public float headCircunference;


    private float headRadius;
    public void SetHeadRadius()
    {
        headRadius = (headCircunference / (2 * Mathf.PI));
    }

    public float GetHeadRadius()
    {
        return headRadius;
    }

    public CParticipantData(int _id_participant, int _blockNumber, float _headCircunference)
    {
        id_participant = _id_participant;
        blockNumber = _blockNumber;
        headCircunference = _headCircunference;
        SetHeadRadius();
    }
}