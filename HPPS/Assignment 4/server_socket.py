
import socket
import time
import os
import datetime

SERVER_IP = "127.0.0.1"
SERVER_PORT = 5678
MSG_MAX_SIZE = 256

months = ["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"]

supported_headers = ["Host","If-Modified-Since","Accept-Language"]

def work_on_headers(string_message):
    lines = string_message.split("\n")

    msg = lines[0]

    for l in lines[1:]:
        head, val = l.split(": ")
        if not head in supported_headers:
            return "Unknown header: " + head
        
        


def make_sendable(str):
    return bytearray(str.encode())

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
                    response = make_sendable("404 Not Found\n\nNo data received")
                else:
                    string_message = bytes_message.decode('utf-8')
                    # response = bytearray(
                    #     f"Message '{string_message}' received".encode())
                    split = string_message.split("\n\n")
                    headers, body = split[0], split[1:]

                    # Hmm is this the correct way?
                    lines = string_message.split("\n")

                    request, headers = lines[0], lines[1:]

                    try:
                        _type, loc, http = request.split(" ")
                    except:
                        connection.sendall(make_sendable("400 Bad Request"))
                        continue
                    
                    modified_time = NONE
                    has_host = False
                    for head in headers:
                        h = head.split(": ")
                        if not h[0] in supported_headers:
                            connection.sendall(make_sendable("400 Bad Request - Unkown Header " + str(h[0])))
                            continue
                        if h[0] == "Host":
                            if has_host:
                                connection.sendall(make_sendable("400 Bad Request - Only one Host header is allowed"))
                                continue
                            has_host = True
                            if "schauser" in h[1] or "utecht" in h[1]:
                                connection.sendall(make_sendable("420 Good Request"))
                                continue

                        if h[0] == "Accept-Language":
                            lan = h[1].split(";")[0]
                            if not "en" in lan:
                                if lan[:2] == "de":
                                    connection.sendall(make_sendable("406 Nur English kanst gebrauchen worden sein"))
                                    continue
                                if lan[:2] == "da":
                                    connection.sendall(make_sendable("406 Hovhov du, vi snakker kun engelsk"))   
                                    continue
                                if lan[:2] == "no":
                                    connection.sendall(make_sendable("406 Håvhåv do, vi snakkær kun engølsk"))   
                                    continue
                                if lan[:2] == "sv":
                                    connection.sendall(make_sendable("406 Hävhäv dö, vi snakkär kun engëlsk"))   
                                    continue
                                if lan[:2] == "es":
                                    connection.sendall(make_sendable("¡406! Dos cerversas por favor"))   
                                    continue
                                connection.sendall(make_sendable("406 Unfortunately, only English is supported at the moment."))   
                                continue

                        if h[0] == "If-Modified-Since":
                            dayname = h[1][:3]
                            day, month, year, = h[1][5:16].split(" ")
                            hour, minute, second = h[1][16:16+9].split(":") 
                            now = datetime.datetime(int(year), months.index(month), int(day), int(hour), int(minute), int(second))
                            modified_time = time.mktime(now.timetuple())


                    if not has_host:
                        connection.sendall(make_sendable("400 Bad Request\n\nA Host header is required, albeit ignored"))
                        continue

                    if _type != "GET":
                        connection.sendall(make_sendable("404 Not Found\n\nOnly GET requests are supported"))
                        continue
                    if http != "HTTP/1.1":
                        connection.sendall(make_sendable("505 HTTP Version Not Supported\n\nOnly HTTP/1.1 is supported"))
                        continue
                    
                    if loc[-1] == "/":
                        if not os.path.isdir(loc):
                            connection.sendall(make_sendable("404 Not Found\n\nThe requested folder is not found"))
                            continue
                        files = os.listdir(loc)
                        
                        if "index.html" in files:
                            if modified_time != NULL:
                                if os.path.getmtime(loc+"/index.html") < modified_time:
                                    connection.sendall(make_sendable("304 - file is too old"))
                                    continue
                            cont = open(loc+"/index.html", "r").read()
                            connection.sendall(make_sendable(cont))
                            continue
                        else:
                            connection.sendall(make_sendable("404 Not Found\n\nFound files:"+str(files)))
                            continue
                    else:
                        if not os.path.isfile(loc):
                            connection.sendall(make_sendable("404 Not Found\n\nThe requested file is not found"))
                            continue

                        #Check if file is actually html

                        cont = open(loc, "r").read()
                        connection.sendall(make_sendable(cont))
                        continue
                        
                    

                    # response = bytearray(work_on_headers(headers).encode())
                    
                # Uncomment this line to add a delay to message processing
                # time.sleep(10)
                print(f"Server responded to '{connection_address}'")

                # Respond to the request
                connection.sendall(response)


if __name__ == "__main__":
    global_start_time = datetime.datetime(2022, 1, 3)
    receive_from_client()