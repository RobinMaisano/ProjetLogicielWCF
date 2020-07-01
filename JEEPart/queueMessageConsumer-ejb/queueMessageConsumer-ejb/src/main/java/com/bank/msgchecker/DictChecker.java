package com.bank.msgchecker;

import DAO.DAOConnection;
import java.util.ArrayList;
/**
 * Establish the connection with the database and check whether or not the file is in french
 * 
 */
public class DictChecker implements IChecker{

    // Call the database and establish a french check
    @Override
    public boolean check(String text) {
        
        //establish the connection and get the data
        DAOConnection d = new DAOConnection();
        d.getConnection();
        ArrayList<String> dictionnary = d.getDictionnary();
        
        // get every single word of the text
        String[] singleWordArray = text.split(" ");
        String[] apostropheWords;
        
        // goodWords : total number of french words in the document
        int goodWords = 0;

        
        // verify if a word is a french word
        for (String singleWord : singleWordArray) {
            //System.out.println("single word " + i + " : " + singleWord[i]);
            //deal with the apostrophe
            if (singleWord.contains("'")){
                apostropheWords = singleWord.split("'");
                singleWord = apostropheWords[1];
            }
            if (dictionnary.contains(singleWord)) {
                System.out.println(singleWord + " IS a french word\n");
                goodWords++;
            } else {
                System.out.println(singleWord + " ISN'T a french word\n");
            }
        }
        
        System.out.println("Number of french words found : " + goodWords + ", out of : "+singleWordArray.length+" total words" );

        //ratio to test the percentage of french words found in the text
        double ratio =  ((float) goodWords / singleWordArray.length) * 100;
        
        System.out.println("Ratio of french words found : " + ratio);
        
        // if there are 70% or more of french words, the text is correctly decrypted and in french
        //TODELETE test 
        //return true;
        
        if(ratio >= 70){
            return true;
        }else{
            return false;
        }
    };
}
