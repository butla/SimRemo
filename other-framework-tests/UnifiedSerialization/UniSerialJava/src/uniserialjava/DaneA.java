/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package uniserialjava;

import com.fasterxml.jackson.annotation.JsonTypeInfo;
import java.util.ArrayList;

/**
 *
 * @author Butla
 */
//@JsonTypeInfo(use=JsonTypeInfo.Id.CLASS, include=JsonTypeInfo.As.PROPERTY, property="@class")
public class DaneA
{
    public int numberA = 13;
    public String stringA = "domyslny";
    
    public ArrayList<Integer> bla;
    
    public DaneA()
    {
        bla = new ArrayList<>();
        bla.add(666);
    }
}
