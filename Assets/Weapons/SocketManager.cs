using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager : MonoBehaviour
{
    List<AttachSocket> _attachSockets;

    //returns false if can't find the socket
    public bool FindAndAttachToSocket(ISocketInterface socketInteface)
    {
        InitSockets(); 
        foreach (AttachSocket socket in _attachSockets)
        {
            if(socket.IsForSocket(socketInteface))
            {
                socket.Attach(socketInteface);
                return true;
            }
        }

        return false;
    }

    void InitSockets()
    {
        if (_attachSockets != null)
            return;

        _attachSockets = new List<AttachSocket>();
        AttachSocket[] attachSockets = GetComponentsInChildren<AttachSocket>();
        _attachSockets.AddRange(attachSockets);
    }
}
