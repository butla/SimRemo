package com.example.jsonrpc4jtest;

import java.io.IOException;

import android.util.Log;

public class TestServiceImpl implements TestService
{

    @Override
    public String test()
    {
        return "dzia³a!";
    }

    @Override
    public String testString(String arg)
    {
        return arg + " dzia³a!";
    }

    @Override
    public DaneA testA()
    {
        return new DaneA();
    }

    @Override
    public DaneB testB()
    {
        return new DaneB();
    }
    
    public int[] testArray()
    {
        return new int[]{1,2,3};
    }

    @Override
    public int testInt()
    {
        return 13;
    }

    @Override
    public double testDouble()
    {
        return 5.25;
    }

    @Override
    public void testVoid()
    {
        Log.i("MOJE SUPER RPC", "Wywolano pusta metode");
    }
    
    public void testException() throws IOException
    {
        throw new IOException("Wiadomosc w exceptionie jakas");
    }

    @Override
    public String testInputA(DaneA arg)
    {
        return arg.tekstA;
    }

    @Override
    public String testPolymorphism(DaneA arg)
    {
        return arg.getClass().getName();
    }
}
