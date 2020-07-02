package com.bank.msgchecker;

/**
 * Check if the file contains the secret information
 * 
 */
public class ContentChecker {

    public String checkSecret(String text) {
        
        // get every single word of the text
        String[] singleWordArray = text.split(" ");
        String informationSecrete;
        
        for(int i =0; i< singleWordArray.length; i++){
            if(singleWordArray[i].equals("l'information") && singleWordArray[i+1].equals("secrète")){
                informationSecrete = singleWordArray[i+2] + " " + singleWordArray[i+3] + " " + singleWordArray[i+4];
                //System.out.println("The secrete information is : " + informationSecrete);
                return informationSecrete;
            }
        }
        
        return "";
    };
    
}
