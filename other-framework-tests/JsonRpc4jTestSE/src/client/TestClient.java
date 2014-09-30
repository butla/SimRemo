/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package client;

import com.fasterxml.jackson.annotation.JsonTypeInfo;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.googlecode.jsonrpc4j.JsonRpcClient;
import com.googlecode.jsonrpc4j.JsonRpcHttpClient;
import com.googlecode.jsonrpc4j.ProxyUtil;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.Socket;
import java.net.URL;
import jsonrpc4jtestse.MapperCreator;
import jsonrpc4jtestse.DaneA;
import jsonrpc4jtestse.DaneB;
import jsonrpc4jtestse.TestService;
import jsonrpc4jtestse.TestServiceImpl;
import org.apache.commons.io.input.TeeInputStream;
import org.apache.commons.io.output.TeeOutputStream;

/**
 *
 * @author Butla
 */
public class TestClient
{
    public static void main(String[] args) throws MalformedURLException, IOException
    {
        try (Socket clientSocket = new Socket("localhost", 1420))
        {
            // ustawienie kopiowania strumieni komunikacyjnych
            ByteArrayOutputStream responseCopyStream = new ByteArrayOutputStream();            
            TeeInputStream responseCopier = new TeeInputStream(
                    clientSocket.getInputStream(),
                    responseCopyStream);
            
            ByteArrayOutputStream requestCopyStream = new ByteArrayOutputStream();            
            TeeOutputStream requestCopier = new TeeOutputStream(
                    clientSocket.getOutputStream(),
                    requestCopyStream);            
            
            // ustawienie poprawnego serilizatora
            JsonRpcClient client = new JsonRpcClient(MapperCreator.get());
            
            // stworzenie klienta
            TestService testService = ProxyUtil.createClientProxy(
                    ClassLoader.getSystemClassLoader(),
                    TestService.class,
                    false,
                    client,
                    responseCopier,
                    requestCopier);
            
//            System.out.println(testService.test());
//            System.out.println(testService.test("blablabla"));
            
            // uruchomienie zdalnej metody
            try
            {
                DaneA data = new DaneB();                
                DaneA data2 = testService.testDataA(data);
            }
            catch(Exception e)
            {
                e.printStackTrace();
            }
            
            // wypisanie przechwyconych wiadomosci
            System.out.println(requestCopyStream.toString());
            System.out.println(responseCopyStream.toString());
            
            // wyczyszczenie strumieni
            requestCopyStream.reset();
            responseCopyStream.reset();
            
            // uruchomienie zdalnej metody
            try
            {              
                Object[] genericArray = testService.testArray();
                System.out.println("Kolejne typy elementów w otrzymanej tablicy.");
                for(Object obj : genericArray)
                {
                    System.out.println(obj.getClass().getName());
                }
            }
            catch(Exception e)
            {
                e.printStackTrace();
            }
            
            System.out.println(requestCopyStream.toString());
            System.out.println(responseCopyStream.toString());
        }
    }
}
