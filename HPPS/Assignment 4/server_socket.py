
import socket
import time

SERVER_IP = "127.0.0.1"
SERVER_PORT = 5678
MSG_MAX_SIZE = 256


supported_headers = ["Date","If-Modified-Since"]

def work_on_headers(string_message):
    lines = string_message.split("\n")

    msg = lines[0]

    for l in lines[1:]:
        head, val = l.split(": ")
        if not head in supported_headers:
            return "Unknown header: " + head
        
        
        if head == "If-Modified-Since":
            dayname = val[:3]
            day, month, year, = val[5:18].split(" ")
            hour, minute, second = val[18:18+8].split(":") 


def receive_from_client():
    """
    Run a simple server socket that will accept a communications, and respond
    to them.
    :return: No return
    """
    # Here we setup a socket for our clients to connect to
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server_socket:
        # Bind the socket to a given IP and port
        server_socket.bind((SERVER_IP, SERVER_PORT))
        # Set it up as a listening port for inbound communication
        server_socket.listen()

        # This while loop will continue forever. This allows us to continue
        # accepting ongoing communications, even from different clients.
        while True:
            # Accept client requests. Note that each request is accepted on a
            # new socket identified by the 'connection' variable.
            connection, connection_address = server_socket.accept()
            with connection:
                print(f"Server messaged by {connection_address}")

                bytes_message = connection.recv(MSG_MAX_SIZE)
                if not bytes_message:
                    response = bytearray("No data received".encode())
                else:
                    string_message = bytes_message.decode('utf-8')
                    # response = bytearray(
                    #     f"Message '{string_message}' received".encode())

                    headers, body = string_message.split("\n\n")
                    response = bytearray(work_on_headers(headers).encode())
                # Uncomment this line to add a delay to message processing
                # time.sleep(10)
                print(f"Server responded to '{connection_address}'")

                # Respond to the request
                connection.sendall(response)


if __name__ == "__main__":
    receive_from_client()