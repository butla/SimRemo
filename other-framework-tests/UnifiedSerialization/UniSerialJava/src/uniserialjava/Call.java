/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package uniserialjava;

import java.util.ArrayList;

/**
 *
 * @author Butla
 */
public class Call
{
    public ArrayList<String> someStrings;
    public int[] someInts;
    public Object[] parameters;
    
    public Call()
    {
        someInts = new int[]{1,2,3};
        someStrings = new ArrayList<>();
        someStrings.add("a");
        someStrings.add("b");
        someStrings.add("c");
    }
}
