/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package jsonrpc4jtestse;

public class TestServiceImpl implements TestService
{
    @Override
    public String test()
    {
        return "działa!";
    }

    @Override
    public String test(String arg)
    {
        return arg + " działa!";
    }

    @Override
    public DaneA testDataA(DaneA dane)
    {
        dane.numberA += 1;
        dane.stringA += " dodałem coś!";
        return dane;
    }

    @Override
    public Object[] testArray()
    {
        Object[] array = new Object[] {28, "jakis tekst", new DaneA(), new DaneB()};
        return array;
    }
}

