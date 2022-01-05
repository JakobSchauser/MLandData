from time import gmtime, strftime
import time
import argparse
import socketserver
import re
from os import listdir, path
from urllib.parse import urlparse
from typing import Tuple

SERVER_IP = "127.0.0.1" #local IP
MSG_MAX_SIZE = 1024

#function for computing byte-size of strings 
def utf8len(s):
    return len(s.encode('utf-8'))

#Custom server
class MyServer(socketserver.TCPServer):
    def __init__(self, server_address: Tuple[str, int], root: str,
                 request_handler_class):
        """
        Constructor for MyServer
        :param server_address: IP address and Port number to host server at
        :param request_handler_class: Handler for responding to requests
        """
        super().__init__(server_address, request_handler_class)
        print(f"Server with port {server_address[1]} and root path {root} has started")

#Custom handler class
class MyHandler(socketserver.StreamRequestHandler):
    """
    A handler class to process all messages sent to the server
    """

    def handle(self) -> None:
        """
        This is the initial function, called by the handler for every message.
        Will always respond to every message.
        :return: No return
        """
        # We use a try/except here to make sure that even if something goes
        # wrong in our server, we always return a response
        # header = "" #for future implementation of a general header
        try:
            #initializing header
            header = ""

            #keeping track of messaging
            print(f"Server messaged by {self.client_address}")

            #If invalid servertype
            if not isinstance(self.server, MyServer):
                self.handle_error("Invalid server type to handle this request")
                return

            bytes_message = bytearray(self.request.recv(MSG_MAX_SIZE))

            #If no bytes are recieved in request
            if not bytes_message:
                self.handle_error(f"No bytes received.")
                return

            string_message = bytes_message.decode('utf-8')
            
            #printin the client message
            print("CLIENT MESSAGE: \n \n" + string_message + "\n")
            
            #splitting the request into a header-section and body
            string_message_split = string_message.split('\n\n')
            header_section = string_message_split[0]

            #checks if first line in request is a valid GET request
            GET_req_search = re.match(r'\AGET \S+', header_section)
            if not(GET_req_search):
                header +='HTTP/1.1 501 Not Implemented\n'
                response = header + '\n' + 'Only GET method is implemented'
                self.request.sendall(response.encode())
                print(f"Server responded to '{self.client_address}'")
                return
            
            #checks if valid HTTP protocol
            protocol_search = re.match(r'\AGET \S+ HTTP/1.1', header_section)
            if not(protocol_search):
                header +='HTTP/1.1 505 HTTP Version Not Supported\n'
                response = header + '\n' + 'Only HTTP/1.1 is supported'
                self.request.sendall(response.encode())
                print(f"Server responded to '{self.client_address}'")
                return

            #checks if client request has the right host
            host_search = re.search(r'Host:\s+localhost:{}'.format(args.port), header_section)
            if not(host_search):
                header += 'HTTP/1.1 400 Bad Request\n'
                response = header + '\n' + 'Wrong or missing Host'
                self.request.sendall(response.encode())
                print(f"Server responded to '{self.client_address}'")
                return
            
            #checks if more than one host is given
            host_list = re.findall(r'Host:\s+', header_section)
            if len(host_list) > 1:
                header += 'HTTP/1.1 400 Bad Request\n'
                response = header + '\n' + 'Multiple Hosts'
                self.request.sendall(response.encode())
                print(f"Server responded to '{self.client_address}'")
                return
            
            #checks if a certain connection type is requested and
            #makes a response-header accordingly
            connection_search = re.search(r'Connection:\s+',header_section)
            if connection_search:
                header = header + 'Connection: close\n'
            
            #checks if Content-Length is requested
            cont_len = False
            length_search = re.search(r'Content-Length:\s+',header_section)
            if length_search:
                cont_len = True
            
            #checks if Accept-Language is requested and responds
            #accordingly
            language_search = re.search(r'(?<=Accept-Language:\s)\S+', header_section)
            if language_search:
                lan = language_search.group(0)
                print(lan)
                header += 'Content-Language: en\n'
                if not('en') or not('*') in lan:
                    header = 'HTTP/1.1 406 Not Acceptable\n' + header
                    response = header + '\n' + 'Only supported language is english (en)'
                    self.request.sendall(response.encode())
                    print(f"Server responded to '{self.client_address}'")

            #checks if date is requested and responds accordingly
            date_search = re.search(r'Date:\s+', header_section)
            if date_search:
                date = time.strftime("%a, %d %b %Y %H:%M:%S", time.gmtime()) + "GMT"
                header += "Date: " + date + '\n'

            #finds the path from the URL
            url_search = re.search(r'(?<=GET )\S+', header_section)
            url = url_search.group(0)
            url_parse = urlparse(url, scheme='http')
            _path = args.root + url_parse.path
            
            #checks if valid path
            if path.exists(_path):
                
                #checks if path is folder
                if _path[-1] == '/':
                    
                    #tries to open index.html file
                    try:
                        file = open(_path + 'index.html', "r")
                        content = file.read()
                        file.close()

                        #if Content-Length is requested
                        if cont_len:
                            size = path.getsize(_path + 'index.html')
                            header += f'Content-Length: {size}\n'

                        header = 'HTTP/1.1 200 OK\n' + header
                        response = header + '\n' + content
                        self.request.sendall(response.encode())
                        print(f"Server responded to '{self.client_address}'")
                    
                    #if no index.html file is found
                    except Exception as NoIndex:
                        folder_list = listdir(_path)
                        folder_list_str = ' '.join([str(elem) for elem in folder_list])

                        #if Content-Length is requested
                        if cont_len:
                            size = utf8len(folder_list_str)
                            header += f'Content-Length: {size}\n'
                        
                        header = 'HTTP/1.1 200 OK\n' + header
                        response = header + '\n' + folder_list_str
                        self.request.sendall(response.encode())
                        print(f"Server responded to '{self.client_address}'")
                
                else:
                    #checks if the paths is a file
                    if path.isfile(_path):
                        file = open(_path, "r")
                        content = file.read()
                        file.close()
                        
                        #if Content-Length is requested
                        if cont_len:
                            size = path.getsize(_path)
                            header += f'Content-Length: {size}\n'

                        header = 'HTTP/1.1 200 OK\n' + header
                        response = header + '\n' + content
                        self.request.sendall(response.encode())
                        print(f"Server responded to '{self.client_address}'")

                    #if the path is not a file it is a folder
                    #with no / at the end -> Not Found
                    else:
                        header = 'HTTP/1.1 404 Not Found\n' + header
                        response = header + '\n' + 'This path is a folder and should end with /'
                        self.request.sendall(response.encode())
                        print(f"Server responded to '{self.client_address}'")
            
            #If the path is not found -> Not Found
            else:
                header = 'HTTP/1.1 404 Not Found\n' + header
                response = header + '\n'
                self.request.sendall(response.encode())
                print(f"Server responded to '{self.client_address}'")

            return
        except Exception as e:
            self.handle_error(f"An error was encountered in the server. {e}")

    def handle_error(self, error_msg: str) -> None:
        """
        Function to send error messages back to the client
        :param error_msg: An error message to print. This is not returned to
        the client, but a generic error message is
        :return: No return
        """
        print(f"{error_msg}")
        response = bytearray("Internal server error".encode())
        self.request.sendall(response)
        return

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument(
        "port",
        type=int,
        help="port number: integer")
    parser.add_argument(
        "root",
        type=str,
        help="path to folder the server delivers from")
    args = parser.parse_args()

    MyServer.allow_reuse_address = True
    my_server = MyServer((SERVER_IP, args.port),args.root, MyHandler )
    try:
        my_server.serve_forever()
    finally:
        my_server.server_close()