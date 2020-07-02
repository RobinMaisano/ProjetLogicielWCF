package com.bank.msgchecker;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;
import java.nio.charset.StandardCharsets;
import java.util.Base64;

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
        String secret ="";
        
        System.out.println("\n");

        System.out.println("Start to process a file with key : " + splitInfo[1]);
        
        //revert from String to byte[]
        byte[] originalByte = Base64.getDecoder().decode(splitInfo[0]);
        
        //debug purpose
        //System.out.println("response original byte tab base 64 decoded: " + originalByte);
        //System.out.println("Converted message decoded to string BEFORE : " + splitInfo[0]);
        
        //then revert to a readable string
        try {
            splitInfo[0] = new String(originalByte, "UTF-8");
            
            //System.out.println("Converted message decoded to string: " + splitInfo[0]);   
            
            if(checkFrench(splitInfo[0].toLowerCase())){
                isFrench = true ;
                
                if(!(checkContent(splitInfo[0]).equals(""))){
                    isSecretFound = true;
                    secret = checkContent(splitInfo[0]);
                }
                
                if(isFrench){
                    System.out.println("\n");
                    System.out.println("*******************************************************************************");
                    System.out.println("The following file is in french : ");
                    System.out.println("\n");
                    System.out.println(splitInfo[0]);
                    System.out.println("The file name is  : " + splitInfo[2] );
                    System.out.println("Decoded with the key : " + splitInfo[1] );
                    System.out.println("*******************************************************************************\n");
                  
                  if(isSecretFound){
                      System.out.println("*******************************************************************************");
                      System.out.println("The secret information has been found : "+secret);
                      System.out.println("*******************************************************************************");
                  }else{
                       System.out.println("The secret information wasn't in this file.");
                  }
                }else {
                    System.out.println("File '"+splitInfo[2]+"' is french : " + isFrench);
                }
            }
        } catch (UnsupportedEncodingException e) {
            System.out.println("Error during translation from encoded base 64 to string : "+ e.getMessage());
        }
        
         System.out.println("Processing of a file ended\n");

        return true;
    }
    
    //check if the text is in french
    public boolean checkFrench (String message){
        ichecker = new DictChecker();        
        return ichecker.check(message);
    }
        
    //check if the text contains the secret
    public String checkContent (String message){
        ContentChecker c = new ContentChecker();
        return c.checkSecret(message);
    }
    
}
