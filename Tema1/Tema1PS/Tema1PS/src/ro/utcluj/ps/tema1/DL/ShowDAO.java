package ro.utcluj.ps.tema1.DL;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import ro.utcluj.ps.tema1.Models.Show;


public class ShowDAO {
	private String url = "jdbc:mysql://localhost/ShowManagement";
	private	String uid = "root";
	private	String pw = "";
	private Connection con;
	
	
	public ShowDAO(){
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


		public void addShow(Show show) {
			String sqlSt;
			sqlSt = "INSERT INTO spectacol (showName,regia, distributia,data_premierei,tickets,availabletickets) VALUES ('"+ show.getShowName()+"','"+ show.getRegia() +"','"+ show.getDistributia() +"','"+ show.getDataPremierei() +"','"+ show.getBilete() +"','"+ show.getBilete() +"');";
			System.out.println(sqlSt);
			doUpdate(sqlSt);
			
		}
	


	//lista spectacole
	public Object[][] getShowList(){
		String sqlSt;
		sqlSt = "SELECT * FROM spectacol;"; 
		System.out.println(sqlSt);
		Object rez[][] = new Object[100][7];
		try {
			Statement stmt = con.createStatement();
			ResultSet rst = stmt.executeQuery(sqlSt);
		
			int i=0;
			while (rst.next())
			{
				Object obj = rst.getObject(1);
				rez[i][6] = obj.toString();
				for (int j=0;j<=5;j++){
					obj = rst.getObject(j+2);
					if (obj != null){
						if (j == 3){
							rez[i][j] = obj.toString().substring(0, 10);
						}else{
							rez[i][j] = obj.toString();
						}
					}
					
				 }
				System.out.println(rez[i][0]);
				i++;
				
			}	
			rst.close();
		}catch (SQLException ex) {
			System.err.println("SQLException: " + ex);
		}
		return rez;
		
	}
	
	
	
	public void deleteShow(String showName){
		String sqlSt;
		sqlSt = "DELETE FROM spectacol WHERE showName = '"+showName+"';";
		System.out.println(sqlSt);
		doUpdate(sqlSt);
	}


	public void updateShow(String show,String showName, String regia, String distributia, String dataPremierei, String bilete) {
		String sqlSt;
		sqlSt = "UPDATE spectacol SET showName='"+showName+"',regia = '"+ regia +"',distributia = '"+ distributia+"', data_premierei = '"+ dataPremierei +"', tickets ='"+bilete+"' WHERE showName ='"+show+"';";
		System.out.println(sqlSt);
		doUpdate(sqlSt);
	}
	
	
	public int availableTickets(String showId) {
		String sqlSt;
		sqlSt = "SELECT availabletickets FROM spectacol WHERE showId = '"+showId+"'"; 
		System.out.println(sqlSt);
		int i =0;
		try {
			Statement stmt = con.createStatement();
			ResultSet rst = stmt.executeQuery(sqlSt);
			
			if (rst.next())
			{	
				Object obj = rst.getObject(1);
				if (obj != null){
					i = Integer.valueOf(obj.toString());
				}
			}	
			rst.close();
		}catch (SQLException ex) {
			System.err.println("SQLException: " + ex);
		}
		return i;
	}
	

	public void updateAvailableTickets(String showId) {
		String sqlSt;
		sqlSt = "UPDATE spectacol SET availabletickets= availabletickets-1 WHERE showId ='"+showId+"';";
		System.out.println(sqlSt);
		doUpdate(sqlSt);
		
	}
	
	
	
}