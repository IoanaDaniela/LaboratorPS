package ro.utcluj.ps.tema1.Models;

import ro.utcluj.ps.tema1.GUI.LoginForm;

public class Control {

	@SuppressWarnings("deprecation")
	public static void main(String args[]){
		
		LoginForm logIn = new LoginForm("Teatrul National Cluj-Napoca");
		
		logIn.show();
	}
	
}
