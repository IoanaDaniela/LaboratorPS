package ro.utcluj.ps.tema1.Models;

public class User {
	private String username;
	private String password;
	private String usertype;
	
	
	public User(String username, String password, String usertype){
		this.username = username;
		this.password = password;
		this.usertype = usertype;
	}


	public String getUsername() {
		return username;
	}


	public void setUsername(String username) {
		this.username = username;
	}


	public String getPassword() {
		return password;
	}


	public void setPassword(String password) {
		this.password = password;
	}


	public String getUsertype() {
		return usertype;
	}


	public void setUsertype(String usertype) {
		this.usertype = usertype;
	}
	
	
	
}
