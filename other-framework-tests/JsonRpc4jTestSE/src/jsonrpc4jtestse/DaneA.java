/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package jsonrpc4jtestse;

import com.fasterxml.jackson.annotation.JsonTypeInfo;

/**
 *
 * @author Butla
 */
//@JsonTypeInfo(use=JsonTypeInfo.Id.CLASS, include=JsonTypeInfo.As.PROPERTY, property="@class")
public class DaneA
{
    public int liczbaA = 13;
    public String tekstA = "domyslny";
}
