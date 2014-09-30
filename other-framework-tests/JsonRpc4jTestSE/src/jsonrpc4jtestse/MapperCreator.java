/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package jsonrpc4jtestse;

import com.fasterxml.jackson.annotation.JsonTypeInfo;
import com.fasterxml.jackson.databind.ObjectMapper;

/**
 *
 * @author Butla
 */
public class MapperCreator
{
    public static ObjectMapper get()
    {
        ObjectMapper mapper = new ObjectMapper();
            
        //mapper.enableDefaultTyping(); // defaults for defaults (see below); include as wrapper-array, non-concrete types
        mapper.enableDefaultTyping(
                ObjectMapper.DefaultTyping.NON_FINAL,
                JsonTypeInfo.As.PROPERTY); // all non-final types
        return mapper;
    }
}
