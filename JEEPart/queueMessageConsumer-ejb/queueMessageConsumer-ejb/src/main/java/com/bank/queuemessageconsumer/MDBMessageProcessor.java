package com.bank.queuemessageconsumer;

import com.bank.msgchecker.FileChecker;
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
        boolean isFileChecked;
        FileChecker f = new FileChecker();
        
        try {

            //System.out.println("\nMessage received from the JMS Queue : " + message.getBody(String.class));
            
            isFileChecked=f.checkFile(message.getBody(String.class));
            
            //System.out.println("Is file checked : " + isFileChecked);
                        
        } catch (JMSException ex) {
            Logger.getLogger(MDBMessageProcessor.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
}
