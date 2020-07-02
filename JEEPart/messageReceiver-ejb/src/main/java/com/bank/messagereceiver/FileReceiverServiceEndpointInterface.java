package com.bank.messagereceiver;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;

/**
 * Receive a message from C#
 * 
 */
@WebService(name="FileReceiverEndp")
public interface FileReceiverServiceEndpointInterface {
    @WebMethod(operationName="messageReader")
    @WebResult(name="messageReceived")
    String getMessage(@WebParam(name = "message") byte[] message, @WebParam(name = "key") String key, @WebParam(name = "fileName") String fileName);
    //String getMessage(@WebParam(name = "message") String message);
}
