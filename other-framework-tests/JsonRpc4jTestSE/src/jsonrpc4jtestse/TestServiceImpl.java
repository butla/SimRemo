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
        dane.liczbaA += 1;
        dane.tekstA += " dodałem coś!";
        return dane;
    }

    @Override
    public Object[] testArray(Object[] array)
    {
        int a = (int)array[0];
        a++;
        array[0] = a;
//        Object[] array = new Object[] {28, "jakis tekst", new DaneA(), new DaneB()};
        return array;
    }

    @Override
    public int[] testArray2(int[] array)
    {
        int[] nowa = new int[array.length+1];
        for(int i=0; i< array.length; i++)
        {
            nowa[i] = array[i];
        }
        nowa[array.length] = 555;
        return nowa;
    }
}

