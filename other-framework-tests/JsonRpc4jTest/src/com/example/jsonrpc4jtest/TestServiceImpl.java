package com.example.jsonrpc4jtest;

public class TestServiceImpl implements TestService
{

    @Override
    public String test()
    {
        return "dzia�a!";
    }

    @Override
    public String testString(String arg)
    {
        return arg + " dzia�a!";
    }

}
