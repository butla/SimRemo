package com.example.jsonrpc4jtest;

import java.io.IOException;

public interface TestService
{
    String test();
    
    String testString(String arg);
    
    DaneA testA();
    
    DaneB testB();
    
    int[] testArray();

    int testInt();
    
    double testDouble();
    
    void testVoid();
    
    void testException() throws IOException;
    
    String testInputA(DaneA arg);
}
