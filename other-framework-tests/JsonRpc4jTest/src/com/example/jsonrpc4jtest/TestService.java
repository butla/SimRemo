package com.example.jsonrpc4jtest;

import java.io.IOException;

public interface TestService
{
    String test();
    
    String testString(String arg);
    
    DaneA testA();
    
    DaneB testB();
    
    int[] testArray();

    public int testInt();
    
    public double testDouble();
    
    void testVoid();
    
    public void testException() throws IOException;
}
