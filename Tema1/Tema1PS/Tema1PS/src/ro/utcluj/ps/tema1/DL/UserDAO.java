package ro.utcluj.ps.tema1.DL;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import ro.utcluj.ps.tema1.Models.User;

public class UserDAO {
	private String url = "jdbc:mysql://localhost/ShowManagement";
	private	String uid = "root";
	private	String pw = "";
	private Connection con;
	
	
	public UserDAO(){
		init();
	}
	
	
	//initializam conexiunea la baza de date
		private void init()
		{
			try {	
				Class.forName("com.mysql.jdbc.Driver");
			}
			catch (java.lang.ClassNotFoundException e) {
				System.err.println("ClassNotFoundException: " +e);
			}

			con = null;
			try {
				con = DriverManager.getConnection(url, uid, pw);
			}
			catch (SQLException ex) {
				System.err.println("SQLException: " + ex);
				System.exit(1);
			}	
		}
	
		
	
		public User getUser(String nume,String parola){
			User usr = null;
			String sqlSt;
			sqlSt = "SELECT *FROM User WHERE username ='"+nume+"' AND password = '"+parola+"';";
			
			try {
				Statement stmt = con.createStatement();
				ResultSet rst = stmt.executeQuery(sqlSt);
			
				if (rst.next()){
					String username="";
					String password="";
					String usertype="";
					Object obj = rst.getObject(2);
					if (obj != null){
						username = obj.toString();
					}
					
					obj = rst.getObject(3);
					if (obj != null){
						password = obj.toString();
					}
					
					obj = rst.getObject(4);
					if (obj != null){
						usertype = obj.toString();
					}
					
					System.out.println("Username: "+ username);
					System.out.println("Password: "+ password);
					System.out.println("Type: "+ usertype);
					System.out.println();
					
					usr = new User(username,password,usertype);
				}	
			
			}catch (SQLException ex) {
				System.err.println("SQLException: " + ex);
			}
			
			
			return usr;
		}


		public void addUser(String username, String password) {
			String sqlSt;
			sqlSt = "INSERT INTO User (username, password, usertype) VALUES ('"+username+"','"+password+"','employee');";
			System.out.println(sqlSt);
			doUpdate(sqlSt);
			
		}
		
		private void doUpdate(String updateStr)
		{
			try {
				Statement stmt = con.createStatement();
				stmt.executeUpdate(updateStr);
				System.out.println("Operation success!");
			}
			catch (SQLException e)
			{	System.out.println("Operation failed: "+e);
			}
		}


		public boolean resetPassword(String username, String newPassword) {
			
			String sqlSt;
			sqlSt = "SELECT *FROM User WHERE username ='"+username+"';";
			
			try {
				Statement stmt = con.createStatement();
				ResultSet rst = stmt.executeQuery(sqlSt);
			
				if (rst.next()){
					sqlSt = "UPDATE user SET password = '"+newPassword+"' WHERE username ='"+username+"';";
					System.out.println(sqlSt);
					doUpdate(sqlSt);
					return true;
				}	
			
			}catch (SQLException ex) {
				System.err.println("SQLException: " + ex);
			}	
			return false;
		}


		
	
	
	
}
