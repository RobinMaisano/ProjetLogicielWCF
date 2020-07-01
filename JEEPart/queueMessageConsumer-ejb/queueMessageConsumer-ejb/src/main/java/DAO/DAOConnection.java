
package DAO;
import java.sql.*;  
import java.util.ArrayList;
/**
 *  Make the connection with the database and return everything in the french words list table
 * 
 */
public class DAOConnection {
    
    //List of french words to be populated by the database result
    private ArrayList<String> frenchWordsList;
    
    //Connect to the Oracle database and populate the list of french words
    public void getConnection(){
        
        this.frenchWordsList = new ArrayList<>();
        
        try{  
        //load the driver class  
        Class.forName("oracle.jdbc.driver.OracleDriver");  

        // create the connection object  
        Connection con =DriverManager.getConnection(  
        "jdbc:oracle:thin:@localhost:1521:frenchDictio","system","Cesi123!");  

        //create the statement object  
        Statement stmt = con.createStatement();  

        //execute query  
        ResultSet rs = stmt.executeQuery("SELECT * FROM sys.french_words");  
        
        //populate the list with the database result
        while(rs.next()){
            this.frenchWordsList.add(rs.getString(1));
            //System.out.println("word : " + rs.getString(1));  
        }
        
        //close the connection object  
        con.close();  

        }catch(Exception e){ 
            System.out.println(e);
        }  

    } 
    
    // return the list of french words in an arrayList
    public ArrayList<String> getDictionnary(){
        return this.frenchWordsList;
    }
}
