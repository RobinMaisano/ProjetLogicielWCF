package com.bank.messagereceiver;

import java.io.StringWriter;
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
    //public String getMessage(String message, String key, String fileName){
    public String getMessage(String message){

        System.out.println("response : " + message);
        /*if (message.length() != 0){
            try {
                sendMessage(message, key, fileName);
                
            } catch (Exception e) {
            System.out.println("Exception raised : "+e);
            }
            return "Message : " + message + ". Key : " + key + ". File name : " + fileName ;
        } 
        else {
            return "Empty parameter";
        }*/
        
        
        return "error";
    }
    
    //Translate the received message and send it to the JMS queue
    private void sendMessage(String message, String key, String fileName){
        //utilisation de l'API JAX-B de gestion de flux pour marshaller (transformer) l'objet //Payment en chaine XML
        JAXBContext jaxbContext;
        String[] messageObject = new String[3];
        messageObject[0] = message;
        messageObject[1] = key;
        messageObject[2] = fileName;

        try {
            //obtention d'une instance JAXBContext associée au type Payment annoté avec JAX-B
            jaxbContext = JAXBContext.newInstance(String.class);
            //création d'un Marshaller pour transfomer l'objet Java en flux XML
            /*Marshaller jaxbMarshaller = jaxbContext.createMarshaller();
            
            StringWriter writer = new StringWriter();
            
            //transformation de l'objet en flux XML stocké dans un Writer
            jaxbMarshaller.marshal(messageObject, writer);
            String xmlMessage = writer.toString();
            //affichage du XML dans la console de sortie
            System.out.println(xmlMessage);*/
            
            
            TextMessage msg = context.createTextMessage(messageObject[0]);
            //ObjectMessage o = context.createObjectMessage(messageObject);
            //MapMessage mapMessage = context.createMapMessage();
            //mapMessage.setString("Message", message);
            //mapMessage.setString("Key", key);
            //mapMessage.setString("fileName", fileName);
            
            //Send the message to the messageQueue
            context.createProducer().send(messageQueue, msg);
            
            //context.createProducer().send(messageQueue, o);
            //context.createProducer().send(messageQueue, mapMessage);
            System.out.println("Message : " + messageObject[0] + " has been sent to the queue messageQueue!");
            

        } catch (Exception ex) {
            Logger.getLogger(FileReceiverService.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
