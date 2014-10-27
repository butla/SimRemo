package com.example.jsonrpc4jtest;

import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.UnknownHostException;

import android.util.Log;

import com.googlecode.jsonrpc4j.JsonRpcServer;
import com.googlecode.jsonrpc4j.StreamServer;

public class ServerThread extends Thread
{
    public void run()
    {
        TestServiceImpl service = new TestServiceImpl();
        JsonRpcServer serverBase = null;     
        
        try
        {
            serverBase = new JsonRpcServer(service, TestService.class);
        }
        catch(Exception ex)
        {
            ex.toString();
        }
            
        int maxThreads = 50;
        int port = 1666;
        InetAddress bindAddress;
        try
        {
            bindAddress = InetAddress.getByName("0.0.0.0");
            StreamServer server = new StreamServer(serverBase, maxThreads, port, 0, bindAddress);
            server.start();
            
//            ServerSocket sock = new ServerSocket(1666);
//            Socket clientSoc= sock.accept();
//            int received = clientSoc.getInputStream().read();
//            System.out.println(received);
        }
        catch (UnknownHostException e)
        {
            // TODO Auto-generated catch block
            e.printStackTrace();            
            Log.e("Moje testy", "Unknown host");
        }
        catch (IOException e)
        {
            // TODO Auto-generated catch block
            e.printStackTrace();
            Log.e("Moje testy", "IOException");
        }
    }
}
