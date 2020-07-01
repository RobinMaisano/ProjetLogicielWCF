package com.bank.msgchecker;

import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;

/**
 *  Call the methods to check if the file is in french and if it contains the secret information
 * 
 */
public class FileChecker{
    
    private IChecker ichecker;
    
    public boolean checkFile (String message){
        boolean isFrench = false;
        boolean isSecretFound = false;
        
        //split message, key and fileName
        String[] splitInfo = message.split("\\n+");
        
        try {
            splitInfo[0] = URLDecoder.decode(new String(splitInfo[0].getBytes("ISO-8859-1"), "UTF-8"), "UTF-8");
        } catch (UnsupportedEncodingException e) {
            System.out.println("Error during translation for ISO-8859-1 to UTF-8");
        }
            
        if(checkFrench(splitInfo[0].toLowerCase())){
            isFrench = true ;
            isSecretFound = checkContent(splitInfo[0]);
        }

        System.out.println("Is french : " + isFrench);
        System.out.println("Is the secret found :  "+isSecretFound);
        
        //TODO : add the response to C#
        
        return true;
    }
    
    //check if the text is in french
    public boolean checkFrench (String message){
        ichecker = new DictChecker();        
        return ichecker.check(message);
    }
        
    //check if the text contains the secret
    public boolean checkContent (String message){
        ichecker = new ContentChecker();
        return ichecker.check(message);
    }
    
}
