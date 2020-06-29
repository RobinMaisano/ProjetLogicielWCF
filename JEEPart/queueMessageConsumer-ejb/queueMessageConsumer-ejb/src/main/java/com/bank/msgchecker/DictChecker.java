package com.bank.msgchecker;

import java.util.Arrays;
import java.util.ArrayList;
/**
 * Establish the connection with the database and check whether or not the file is in french
 * 
 */
public class DictChecker implements IChecker{

    // Call the database and establish a french check
    @Override
    public boolean check(String text) {
        System.out.println(text + "dict checker");
        ArrayList<String> dictionnary = createFakeDictionnary();
        // cpt : total number of words in the document
        // goodWords : total number of french words in the document
        int cpt = 0;
        int goodWords = 0;
        
        //simple pre-test
        if( dictionnary.contains("3")){
            cpt++;
            goodWords++;
        }
        //TODO : ratio and return true or false
        if(goodWords >= 1){
            return true;
        }else{
            return false;
        }
    };
    
    public ArrayList<String>createFakeDictionnary(){
        ArrayList<String> fakeDictionnary = new ArrayList<>();
        
        for (int i = 0; i<20; i++){
            fakeDictionnary.add((String.valueOf(i))); 
        }
        return fakeDictionnary;
    }
    
}
