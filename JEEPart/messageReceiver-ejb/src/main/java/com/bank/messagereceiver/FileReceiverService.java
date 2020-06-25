/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.bank.messagereceiver;

import javax.ejb.Stateless;
import javax.ejb.LocalBean;
import javax.jws.WebService;

/**
 *
 * @author cesi
 */
@Stateless
@WebService(
    endpointInterface = "com.bank.messagereceiver.FileReceiverServiceEndpointInterface",
    portName="FileReceiverPort",
    serviceName="FileReceiverService"
)
public class FileReceiverService implements FileReceiverServiceEndpointInterface {

    @Override
    public String getMessage(String message, String key, String fileName){
        if (message.length() != 0){
            return "Message : " + message + ". Key : " + key + ". File name : " + fileName ;
        } 
        else {
            return "Empty parameter";
        }
    }
}
