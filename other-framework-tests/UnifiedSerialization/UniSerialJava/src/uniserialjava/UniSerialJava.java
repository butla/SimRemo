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
        
        Object[] tablica = new Object[] { 28, "jakis tekst", new DaneA(), new DaneB() };
        
        String serialized = mapper.writeValueAsString(tablica);
        System.out.println(serialized);
        
        Object bla = mapper.readValue(serialized, Object.class);
        System.out.println(bla.getClass().getName());
    }
    
}
