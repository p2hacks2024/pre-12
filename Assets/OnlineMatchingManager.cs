using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class OnlineMatchingManager : MonoBehaviourPunCallbacks
{/*
    bool isEnterRoom; // �����ɓ����Ă邩�ǂ����̃t���O
    bool isMatching; // �}�b�`���O�ς݂��ǂ����̃t���O

    public void OnMatchingButton()
    {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // �����_���}�b�`���O
        PhotonNetwork.JoinRandomRoom();
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        isEnterRoom = true;
    }

    // ���s�����ꍇ
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 1 }, TypedLobby.Default);
    }

    // ������l�Ȃ�V�[����ς���
    private void Update()
    {
        if (isMatching) return;

        if (isEnterRoom)
        {
            if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                isMatching = true;
                Debug.Log("�}�b�`���O����");
                GameManager.instance.StartGame();
            }
        }
    }*/
}