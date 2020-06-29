package com.bank.messagereceiver;

import javax.xml.ws.Endpoint;


/**
 * 
 * 
 */
//a simple class to test the webservice TODELETE
public class WebServicePublisher {
    
    public static final String URI = "http://localhost:12080/displayMessage";
    
    public static void main (String[] args){
        FileReceiverService file = new FileReceiverService();
       /* 
        Endpoint endpoint = Endpoint.publish(URI, file);
        
        boolean status = endpoint.isPublished();
        System.out.println("Webservice disponible ? " + status);*/
        System.out.println("hello");
        
    }
    
}
