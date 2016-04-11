package ro.utcluj.ps.tema1.DL;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import ro.utcluj.ps.tema1.Models.Ticket;

public class TicketDAO {
	private String url = "jdbc:mysql://localhost/ShowManagement";
	private	String uid = "root";
	private	String pw = "";
	private Connection con;
	
	
	public TicketDAO(){
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
	
		

		
		




	//lista bilete
	public String[] getTicketsList(String show){
		String sqlSt;
		sqlSt = "SELECT * FROM ticket WHERE showId = '"+show+"' ORDER BY rand, loc;"; 
		System.out.println(sqlSt);
		String[] rez = new String[100];
		try {
			Statement stmt = con.createStatement();
			ResultSet rst = stmt.executeQuery(sqlSt);
		
			int i=0;
			while (rst.next())
			{	
				rez[i] = String.valueOf(i+1) + ". ";
				Object obj = rst.getObject(2);
				if (obj != null){
					rez[i] +="RAND: " + obj.toString();
				}
				obj = rst.getObject(3);
				if (obj != null){
					rez[i] +="   LOC: " + obj.toString();
				}	
				i++;
			}	
			rst.close();
		}catch (SQLException ex) {
			System.err.println("SQLException: " + ex);
		}
		return rez;
		
	}


	public boolean checkPlace(String showId, String row, String place) {
		String sqlSt;
		sqlSt = "SELECT * FROM ticket WHERE showId ='"+showId+"' AND loc = '"+place+"' AND rand = '"+row+"';";
		
		try {
			Statement stmt = con.createStatement();
			ResultSet rst = stmt.executeQuery(sqlSt);
		
			if (rst.next()){
				return false;
			}else{
				return true;
			}
		
		}catch (SQLException ex) {
			System.err.println("SQLException: " + ex);
		}
		return false;
		
	}
	
	public void addTicket(Ticket bilet) {
		String sqlSt;
		sqlSt = "INSERT INTO ticket (rand, loc, showId) VALUES ('"+bilet.getRow()+"','"+bilet.getPlace()+"','"+bilet.getShowId()+"');";
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


	
	
	
	
}
