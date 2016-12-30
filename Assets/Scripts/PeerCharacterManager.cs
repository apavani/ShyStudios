using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeerCharacterManager : MonoBehaviour {

    private char messageType;
    private float posX, posY;
    private CharacterController _controller;
    private Vector3 targetPosition, movDiff, movDir;

    void OnEnable()
    {
        this._controller = GetComponent<CharacterController>();
    }

    public void ProcessMessage(byte[]data)
    {
        /*
        * P = Position
        * G = Grab
        * D = Drop
        * C = Collect
        */
        messageType = (char)data[0];
        switch (messageType)
        {

            case ('P'):
                {
                    MoveCharacter(data);
                    break;
                }
            case ('G'):
                {
                    break;
                }
            case ('D'):
                {
                    break;
                }
            case ('C'):
                {
                    break;
                }
            default:
                break;
        }
    }

    private void MoveCharacter(byte[] data)
    {
        posX = System.BitConverter.ToSingle(data, 1);
        posY = System.BitConverter.ToSingle(data, 5);
        targetPosition = new Vector3(posX, posY);
        if (targetPosition == transform.position)
            return;

        movDiff = targetPosition - transform.position;
        movDir = movDiff.normalized * 15f * Time.deltaTime;
        if (movDir.sqrMagnitude < movDiff.sqrMagnitude)
        {
            _controller.Move(movDir);
        }
        else
        {
            _controller.Move(movDiff);
        }
    }
}
