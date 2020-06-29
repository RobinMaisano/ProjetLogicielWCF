
package DAO;
import java.sql.*;  
/**
 *
 * @author 
 */
public class DAOConnection {
    public static void main(String args[]){  
       try (Connection conn = DriverManager.getConnection(
                "jdbc:oracle:thin:@localhost:1521:orcl2", "system", "Cesi123!")) {

            if (conn != null) {
                System.out.println("Connected to the database!");
            } else {
                System.out.println("Failed to make connection!");
            }

        } catch (SQLException e) {
            System.err.format("SQL State: %s\n%s", e.getSQLState(), e.getMessage());
        } catch (Exception e) {
            e.printStackTrace();
        }

    }  
}
