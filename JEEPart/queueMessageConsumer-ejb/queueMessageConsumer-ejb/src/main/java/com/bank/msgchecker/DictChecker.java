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
        //words with apostrophe or coma or dot
        String[] apostropheLikeWords;
        
        // goodWords : total number of french words in the document
        int goodWords = 0;

        // verify if a word is a french word
        for (String singleWord : singleWordArray) {
            //System.out.println("single word " + i + " : " + singleWord[i]);
            //FOR LATER deal with the apostrophe, coma and dot
            
            /*if (singleWord.contains("'")){
                System.out.println("APOSTROPHE BEFORE: " + singleWord);
                apostropheLikeWords = singleWord.split("'");
                singleWord = apostropheLikeWords[1];
                System.out.println("APOSTROPHE FOUND AFTER : " + singleWord);
            }*/
            
            if(singleWord.contains(",")){
                //System.out.println("COMA FOUND BEFORE: " + singleWord);
                try {
                    apostropheLikeWords = singleWord.split(",");
                    singleWord = apostropheLikeWords[0];
                } catch (Exception e) {
                    //System.out.println("Exception when trying to delete the coma");
                }
                
                //System.out.println("COMA FOUND AFTER: " + singleWord);
            }
            
            /*if(singleWord.contains(".")){
                System.out.println("DOT FOUND BEFORE: " + singleWord);
                apostropheLikeWords = singleWord.split(".");
                singleWord = apostropheLikeWords[0];
                System.out.println("DOT FOUND AFTER: " + singleWord);
            }*/
            
            if (dictionnary.contains(singleWord)) {
                //System.out.println(singleWord + " IS a french word\n");
                goodWords++;
            } else {
                //System.out.println(singleWord + " ISN'T a french word\n");
            }
        }
        
        System.out.println("Number of french words found : " + goodWords + ", out of : "+singleWordArray.length+" total words" );

        //ratio to test the percentage of french words found in the text
        double ratio =  ((float) goodWords / singleWordArray.length) * 100;
        
        System.out.println("Ratio of french words found : " + (int) ratio + "%");
        
        // if there are 60% or more of french words, the text is correctly decrypted and in french
        if(ratio >= 60){
            return true;
        }else{
            return false;
        }
    };
}
