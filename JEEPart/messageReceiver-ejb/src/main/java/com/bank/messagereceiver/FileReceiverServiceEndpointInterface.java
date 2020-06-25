/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.bank.messagereceiver;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;

/**
 *
 * @author cesi
 */
@WebService(name="FileReceiverEndp")
public interface FileReceiverServiceEndpointInterface {
    @WebMethod(operationName="messageReader")
    @WebResult(name="messageReceived")
    String getMessage(@WebParam(name = "message") String message, @WebParam(name = "key") String key, @WebParam(name = "fileName") String fileName);
}
