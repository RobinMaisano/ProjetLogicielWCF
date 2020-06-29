package com.bank.msgchecker;

/**
 * Check if the file contains the secret information
 * 
 */
public class ContentChecker implements IChecker{

    @Override
    public boolean check(String text) {
        System.out.println(text + "content checker");
        return true;
    };
    
}
