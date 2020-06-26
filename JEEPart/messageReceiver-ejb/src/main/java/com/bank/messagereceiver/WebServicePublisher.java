/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.bank.messagereceiver;

import javax.xml.ws.Endpoint;


/**
 *
 * @author cesi
 */
public class WebServicePublisher {
    
    public static final String URI = "http://localhost:12080/displayMessage";
    
    public static void main (String[] args){
        FileReceiverService file = new FileReceiverService();
        
        Endpoint endpoint = Endpoint.publish(URI, file);
        
        boolean status = endpoint.isPublished();
        System.out.println("Webservice disponible ? " + status);
        
    }
    
}
