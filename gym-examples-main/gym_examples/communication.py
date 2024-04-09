import socket
import threading

def handle_client(client_socket):
    while True:
        # 클라이언트로부터 데이터 수신
        data = client_socket.recv(1024)
        if not data:
            break
        #print("수신한 데이터:", data.decode())


        received_data = data.decode().split(',')
        #print("수신한 데이터:", received_data[:7])

        # 수신한 데이터를 int와 float로 변환하여 저장
        converted_data = [int(received_data[i].strip()) if i < 5 else float(received_data[i].strip()) for i in range(7)]
        print("변환한 데이터:", converted_data)
        
        
        # 클라이언트에게 데이터 전송
        #message = input("전송할 메시지를 입력하세요: ")
        message = "Done"
        
        client_socket.send(message.encode())

    # 클라이언트 소켓 종료
    client_socket.close()

def start_server():
    # 호스트 및 포트 지정
    HOST = '127.0.0.1'
    PORT = 12345

    # 소켓 생성
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    # 포트에 바인딩
    server_socket.bind((HOST, PORT))

    # 클라이언트의 연결 요청 대기
    server_socket.listen()

    print("서버가 시작되었습니다.")

    while True:
        # 클라이언트 연결 수락
        client_socket, client_address = server_socket.accept()
        print(f"{client_address}에서 연결됨")

        # 클라이언트 핸들링을 위한 스레드 시작
        client_thread = threading.Thread(target=handle_client, args=(client_socket,))
        client_thread.start()

# 서버 시작
start_server()