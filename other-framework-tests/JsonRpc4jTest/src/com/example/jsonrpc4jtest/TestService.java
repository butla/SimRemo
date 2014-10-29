package com.example.jsonrpc4jtest;

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
}
