using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System.Threading;
using System.Net.Sockets;

public class communication : MonoBehaviour
{
    Player player;
    private float[] sending_bin_code = new float[7];


    private TcpClient clientSocket;
    private Thread receiveThread;

    private float frame_timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.inst;

        string serverHost = "127.0.0.1";
        int serverPort = 12345;

        // 소켓 생성
        clientSocket = new TcpClient();

        // 서버에 연결
        clientSocket.Connect(serverHost, serverPort);
        Debug.Log("서버에 연결됨");

        // 데이터를 받는 스레드 시작
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.Start();

        frame_timer = 0.0f;

    }

    void OnDestroy()
    {
        // 소켓과 스레드 종료
        clientSocket.Close();
        receiveThread.Abort();
    }

    void ReceiveData()
    {
        while (true)
        {
            // 서버로부터 데이터 수신
            byte[] bytesReceived = new byte[1024];
            int byteCount = clientSocket.GetStream().Read(bytesReceived, 0, bytesReceived.Length);
            string dataReceived = Encoding.ASCII.GetString(bytesReceived, 0, byteCount);
            Debug.Log("수신한 데이터: " + dataReceived);
        }
    }


    // Update is called once per frame
    void Update()
    {

        if(frame_timer <= 0.01)
        {

            for (int i = 0; i < 7; i++)
            {
                sending_bin_code[i] = 0;
            }



            if (player.vertical > 0)
                sending_bin_code[0] = 1;
            else if (player.vertical < 0)
                sending_bin_code[1] = 1;

            if (player.horizontal > 0)
                sending_bin_code[3] = 1;
            else if (player.horizontal < 0)
                sending_bin_code[2] = 1;

            if (player.fire > 0)
                sending_bin_code[4] = 1;

            sending_bin_code[5] = player.transform.position.x;
            sending_bin_code[6] = player.transform.position.y;
            //sending_bin_code[7] = player.transform.position.z;

            string codeString = "";

            foreach (float code in sending_bin_code)
            {
                codeString += code.ToString() + ", ";
            }

            Debug.Log(codeString);

            byte[] bytesToSend = Encoding.ASCII.GetBytes(codeString);
            clientSocket.GetStream().Write(bytesToSend, 0, bytesToSend.Length);
            Debug.Log("데이터 전송 완료");

            frame_timer += 0.001f;

        }
        else
        {
            frame_timer = 0.0f;
        }
    }


        // up down left right 

}
