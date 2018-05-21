#include <iostream>
#include <vector>
#include <winsock2.h>
#include <windows.h>
#include <sstream>
#define ACT_SIZE 1
#define ID_SIZE 1
#define STATUS_SIZE 18
#define MAX_A_SIZE 6
#define COORD_SIZE 4
#define MAX_SIZE 16

using namespace std;
void writeProtocolA(SOCKET socket, string protocol, sockaddr_in &si_other, int slen) //Envía protocolo de acción del jugador
{
    //sockaddr_in si_other;
    char buffer[protocol.size()];
    protocol.copy(buffer, protocol.size(), 0);
    sendto(socket, buffer, protocol.size(), 0, (sockaddr *) &si_other, slen);
}

void writeProtocolU(SOCKET socket, string protocol, sockaddr_in &si_other, int slen) //Envía posición de los jugadores
{
    char buffer[protocol.size()];
    protocol.copy(buffer, protocol.size(), 0);
    sendto(socket, buffer, protocol.size(), 0, (sockaddr *) &si_other, slen);
}

string intToStr(int a)
{
    stringstream ss;
    ss << a;
    return ss.str();
}

/*string readProtocolA(SOCKET socket, sockaddr_in &si_other, int slen)
{
    string protocol_A;

    protocol_A = 'A';

    char* buffer;

    buffer = new char[ID_SIZE+1];
    recvfrom(socket, buffer, ID_SIZE, 0, (sockaddr *) &si_other, &slen);
    buffer[ID_SIZE] = '\0';
    protocol_A += buffer;
    delete[] buffer;

    buffer = new char[STATUS_SIZE+1];
    recvfrom(socket, buffer, STATUS_SIZE, 0, (sockaddr *) &si_other, &slen);
    buffer[STATUS_SIZE] = '\0';
    protocol_A += buffer;


    delete[] buffer;
    buffer = NULL;
    return protocol_A;
}*/

void writeProtocolS(SOCKET socket, string& characters, sockaddr_in &si_other, int slen) //Envía tipos de jugadores a todos.
{
    char* buffer;
    string protocol;
    protocol = 's';
    protocol += intToStr(characters.size());
    protocol += characters;

    buffer = new char[protocol.size()];
    cout<<"enviando :"<<protocol<<endl;
    protocol.copy(buffer, protocol.size(), 0);

    sendto(socket,buffer,protocol.size(),0, (sockaddr *) &si_other, slen);

    delete[] buffer;
    buffer = NULL;
}

void writeProtocolR(SOCKET socket, int id, sockaddr_in &si_other, int slen) //Envía id del jugador al cliente.
{
    char* buffer;
    string protocol;
    protocol = 'r';
    protocol += intToStr(id);

    buffer = new char[protocol.size()];
    protocol.copy(buffer,protocol.size(), 0);

    cout<<"sending protocol: "<<protocol<<endl;
    sendto(socket,buffer,protocol.size(),0, (sockaddr *)&si_other,slen);


    delete[] buffer;
    buffer = NULL;
}

int main(int argc, char* argv[])
{
    WSADATA wsa;
    SOCKET master, new_socket, s;
    vector<SOCKET> client_socket;
    sockaddr_in server, address, si_other;
    int activity, valread, port, slen;
    char *buffer;
    string characters;
    fd_set readfds;
    string protocol;

    vector<sockaddr_in> players;

    slen = sizeof(si_other);

    cout<<"ingrese puerto: ";
    cin>>port;
    if(WSAStartup(MAKEWORD(2,2), &wsa) != 0)
    {
        cout<<"Error"<<endl;
        exit(EXIT_FAILURE);
    }

    master = socket(AF_INET, SOCK_DGRAM, 0);
    if( master == INVALID_SOCKET)
    {
        cout<<"Error 2"<<endl;
        exit(EXIT_FAILURE);
    }

    server.sin_family = AF_INET;
    server.sin_addr.s_addr = INADDR_ANY;
    server.sin_port = htons(port);

    if(bind(master, (sockaddr*) &server, sizeof(server)) == SOCKET_ERROR)
    {
        cout<<"Error 3"<<endl;
        exit(EXIT_FAILURE);
    }

    string last_read = "";
    while(true)
    {
        buffer = new char[MAX_SIZE+1];
        cout<<"escuchando..."<<endl;

        valread = recvfrom(master, buffer, MAX_SIZE, 0, (sockaddr *) &si_other, &slen);
        buffer[valread] = '\0';

        cout<<"recibido: "<<buffer<<endl;

        if(valread == ACT_SIZE || valread == MAX_SIZE || valread == MAX_A_SIZE || valread == ACT_SIZE + ID_SIZE)
        {
            if(valread == MAX_A_SIZE && buffer[0] == 'A' && last_read != buffer)
            {
                last_read = buffer;
                protocol = buffer;
                for(unsigned int i = 0; i < players.size(); i++)
                {
                    writeProtocolA(master, protocol, players[i], slen);
                }
            }
            else if(valread == MAX_SIZE && buffer[0] == 'U' && last_read != buffer)
            {
                protocol = buffer;
                for(unsigned int i = 0; i < players.size(); i++)
                {
                    writeProtocolU(master, protocol, players[i], slen);
                }
            }
            else if(valread == ACT_SIZE && buffer[0] == 'S') //Send all type of players
            {
                writeProtocolS(master, characters, si_other, slen);
            }
            else if(valread == ACT_SIZE + ID_SIZE && buffer[0] == 'R') //Register player
            {
                cout<<buffer[0]<<" "<<buffer[1]<<endl;
                players.push_back(si_other);
                characters += buffer[1];
                writeProtocolR(master, players.size()-1, si_other, slen);
            }
        }
        delete[] buffer;
    }

    return 0;
}
