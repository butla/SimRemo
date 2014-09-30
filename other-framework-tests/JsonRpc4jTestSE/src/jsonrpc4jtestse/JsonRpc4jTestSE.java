/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jsonrpc4jtestse;

import com.googlecode.jsonrpc4j.JsonRpcServer;
import com.googlecode.jsonrpc4j.StreamServer;
import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.UnknownHostException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Butla
 */
public class JsonRpc4jTestSE {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) throws UnknownHostException, IOException, InterruptedException {
        TestServiceImpl service = new TestServiceImpl();
        JsonRpcServer serverBase = new JsonRpcServer(MapperCreator.get(), service, TestService.class);
        
        int maxThreads = 50;
        int port = 1420;
        int backlog = 0;
        InetAddress bindAddress = InetAddress.getByName("localhost");
        
        ServerSocket servSocket = new ServerSocket(port, backlog, bindAddress);

        StreamServer server = new StreamServer(serverBase, maxThreads, servSocket);
        server.start();        
        
        System.out.println("Wal ENTER to umrę!");
        System.in.read();
        server.stop();
    }
}
