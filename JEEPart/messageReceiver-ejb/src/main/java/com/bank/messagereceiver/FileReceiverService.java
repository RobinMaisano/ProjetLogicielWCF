package com.bank.messagereceiver;

import java.io.StringWriter;
import java.io.UnsupportedEncodingException;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.util.Base64;
import javax.jms.Queue;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.annotation.Resource;
import javax.ejb.Stateless;
import javax.ejb.LocalBean;
import javax.inject.Inject;
import javax.jms.JMSContext;
import javax.jms.MapMessage;
import javax.jms.ObjectMessage;
import javax.jms.Session;
import javax.jms.TextMessage;
import javax.jws.WebService;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;

/**
 * Producer & File Receiver
 * @author cesi
 */
@Stateless
@WebService(
    endpointInterface = "com.bank.messagereceiver.FileReceiverServiceEndpointInterface",
    portName="FileReceiverPort",
    serviceName="FileReceiverService"
)
public class FileReceiverService implements FileReceiverServiceEndpointInterface {

    @Inject //paquetage javax.inject
    private JMSContext context; //paquetage javax.jms
    
    @Resource(lookup = "jms/messageQueue") //paquetage javax.annotation
    private Queue messageQueue; //paquetage javax.jms
    
    //Get the message from C#
    @Override
    public String getMessage(byte[] message, String key, String fileName){
        
        //debug purpose
        //System.out.println("Converted message: " + convertedMessage);
        //System.out.println("response original byte tab: " + message);
        
        // encode to string from Byte[]
        String stringifiedMessage ="";
        try {
            stringifiedMessage = Base64.getEncoder().encodeToString(new String(message, StandardCharsets.UTF_8).getBytes("UTF-8"));
        } catch (UnsupportedEncodingException e) {
            System.out.println("Error : " + e);
        }
        
        //debug purpose
        //System.out.println("response String base 64 encoder : " + stringifiedMessage );
        
        if (message.length != 0){
            try {
                sendMessage(stringifiedMessage, key, fileName);
            } catch (Exception e) {
                System.out.println("Exception raised : "+e);
            }
            return "Message in file" + fileName +", with key : "+ key +" has been sent to JMS queue." ;
        } 
        else {
            return "Empty parameter";
        }
    }
    
    //Translate the received message and send it to the JMS queue
    private void sendMessage(String message, String key, String fileName){

        String[] messageObject = new String[3];
        messageObject[2] = fileName;
        String concatMessage = message + "\n" + key + "\n" + fileName;

        try {
                        
            TextMessage msg = context.createTextMessage(concatMessage);
            
            //Send the message to the messageQueue
            context.createProducer().send(messageQueue, msg);
            
            System.out.println("Message from file named : '" + messageObject[2] + "', has been sent to the JMS queue messageQueue!");
            

        } catch (Exception ex) {
            Logger.getLogger(FileReceiverService.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
