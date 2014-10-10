/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package uniserialjavagson;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

/**
 *
 * @author Butla
 */
public class UniSerialJavaGson
{

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args)
    {
        Object[] tablica = new Object[] { 28, "jakis tekst", new DaneA(), new DaneB() };
        
        Gson g = new GsonBuilder(). create();
        
        String serialized = g.toJson(tablica);
        System.out.println(serialized);
    }
    
}
