package com.bank.queuemessageconsumer;

import java.util.logging.Level;
import java.util.logging.Logger;
import javax.ejb.ActivationConfigProperty;
import javax.ejb.MessageDriven;
import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.MessageListener;

/**
 * MsgConsumer
 */
@MessageDriven(activationConfig = {
    @ActivationConfigProperty(propertyName = "destinationLookup", propertyValue = "jms/messageQueue")
    ,
        @ActivationConfigProperty(propertyName = "destinationType", propertyValue = "javax.jms.Queue")
})
public class MDBMessageProcessor implements MessageListener {
    
    public MDBMessageProcessor() {
    }
    
    @Override
    public void onMessage(Message message) {
        
        try {
            //on extrait le paiment du corps du message. - getBody est une méthode JMS 2.0
            //String paymentMessage = message.getBody(String.class);
           System.out.println("message test : " + message.getBody(String.class));
           //System.out.println("message.toString : " + message.toString()); 
            //for(int i = 0; i<=40;i++){
               // System.out.println(paymentMessage);
            //}
            
        } catch (JMSException ex) {
            //Logger.getLogger(MDBMessageProcessor.class.getName()).log(Level.SEVERE, null, ex);
        }

        
    }
    
}
