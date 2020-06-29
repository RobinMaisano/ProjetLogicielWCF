package com.bank.msgchecker;

/**
 *  Call the methods to check if the file is in french and if it contains the secret information
 * 
 */
public class FileChecker{
    
    String msg = "salut from : ";
    IChecker ichecker;
    
    public static void main (String[] args){
        boolean a;
        FileChecker f = new FileChecker();
        a = f.checkFile();
        System.out.println("a main : " + a);
        
    }
    
    
    public boolean checkFile (){
        boolean a, b;
        a = checkContent();
        b = checkFrench();
        System.out.println("a : " + a + "\nb : "+ b);
        return true;
    }
        
    public boolean checkFrench (){
        ichecker = new DictChecker();        
        return ichecker.check(msg);
    }
        
    public boolean checkContent (){
        ichecker = new ContentChecker();
        return ichecker.check(msg);
    }
    
}
