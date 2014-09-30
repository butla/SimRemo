package com.example.jsonrpc4jtest;

import java.io.IOException;
import java.net.InetAddress;
import java.net.UnknownHostException;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;

import com.googlecode.jsonrpc4j.JsonRpcServer;
import com.googlecode.jsonrpc4j.StreamServer;


public class MainActivity extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        
        startServer();
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
    
    public void startServer()
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
        int port = 1420;
        InetAddress bindAddress;
        try
        {
            bindAddress = InetAddress.getByName("localhost");
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
}
