package com.example.jsonrpc4jtest;

import java.io.IOException;
import java.net.InetAddress;
import java.net.UnknownHostException;

import android.util.Log;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.annotation.JsonTypeInfo;
import com.googlecode.jsonrpc4j.JsonRpcBasicServer;
import com.googlecode.jsonrpc4j.StreamServer;

public class ServerThread extends Thread
{
    public void run()
    {
        TestServiceImpl service = new TestServiceImpl();
        JsonRpcBasicServer serverBase = null;     
        
        try
        {
            serverBase = new JsonRpcBasicServer(
                    ServerThread.createMapper(),
                    service,
                    TestService.class);
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
    
    private static ObjectMapper createMapper()
    {
        ObjectMapper mapper = new ObjectMapper();
        
        //mapper.enableDefaultTyping(); // defaults for defaults (see below); include as wrapper-array, non-concrete types
        mapper.enableDefaultTyping(
                ObjectMapper.DefaultTyping.NON_FINAL,
                JsonTypeInfo.As.PROPERTY); // all non-final types
        return mapper;
    }
}
