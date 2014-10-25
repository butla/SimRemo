/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package uniserialjava;

import com.fasterxml.jackson.annotation.JsonTypeInfo;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Dictionary;
import java.util.HashMap;
import java.util.Hashtable;

/**
 *
 * @author Butla
 */
public class UniSerialJava
{

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) throws JsonProcessingException, IOException
    {
        ObjectMapper mapper = new ObjectMapper();
            
        //mapper.enableDefaultTyping(); // defaults for defaults (see below); include as wrapper-array, non-concrete types
        mapper.enableDefaultTyping(
                ObjectMapper.DefaultTyping.NON_FINAL,
                JsonTypeInfo.As.PROPERTY); // all non-final types
        
        int[] tablicaIntow = new int[] {1, 2, 3};
        Object[] tablica = new Object[] { 28, "jakis tekst",  new DaneB(), tablicaIntow};
        
//        int[] tablica2 = new int[] { 28, 33, 33 ,33};
//        tablica[2] = tablica2;
        
        //String[] tablica = new String[] { "bla", "alb"};
        
//        ArrayList<Object> tablica = new ArrayList<Object>();
//        tablica.add(28);
//        tablica.add("jakis tekst");
//        tablica.add(new DaneA());
//        tablica.add(new DaneB());
        
//        HashMap<Integer, String> tablica = new HashMap<>();
//        tablica.put(1, "pierwszy");
//        tablica.put(2, "drugi");
//        tablica.put(3, "trzeci");
        
        Call call = new Call();
        call.parameters = tablica;
        
        String serialized = mapper.writeValueAsString(call);
        System.out.println(serialized);
        
        Object bla = mapper.readValue(serialized, Object.class);
        System.out.println(bla.getClass().getName());
    }
    
}
