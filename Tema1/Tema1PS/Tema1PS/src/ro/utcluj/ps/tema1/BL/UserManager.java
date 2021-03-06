package ro.utcluj.ps.tema1.BL;

import ro.utcluj.ps.tema1.DL.UserDAO;
import ro.utcluj.ps.tema1.Models.RandomPasswordGenerator;
import ro.utcluj.ps.tema1.Models.User;

public class UserManager {
	
	
	public UserManager(){
		
	}
	
	
	public User login(String username, String password){
         UserDAO connection = new UserDAO();
         return connection.getUser(username, crypt(password));
    }
	
	
	public void addUser(String username, String password){
		UserDAO connection = new UserDAO();
		connection.addUser(username, crypt(password));
	}
	
	
	public String resetPassword(String username) {
		
		int noOfCAPSAlpha = 1;
        int noOfDigits = 1;
        int noOfSplChars = 1;
        int minLen = 8;
        int maxLen = 12;
 
        char[] pswd = RandomPasswordGenerator.generatePswd(minLen, maxLen,noOfCAPSAlpha, noOfDigits, noOfSplChars);
        String newPassword = new String(pswd);
        System.out.println("Len = " + pswd.length + ", " + newPassword);
        
        UserDAO connection = new UserDAO();
        if(connection.resetPassword(username,crypt(newPassword))){
        	return newPassword;
        }
		return "";
		
		
	}

	
	static String crypt(String md5) {
		   try {
		        java.security.MessageDigest md = java.security.MessageDigest.getInstance("MD5");
		        byte[] array = md.digest(md5.getBytes());
		        StringBuffer sb = new StringBuffer();
		        for (int i = 0; i < array.length; ++i) {
		          sb.append(Integer.toHexString((array[i] & 0xFF) | 0x100).substring(1,3));
		       }
		        return sb.toString();
		    } catch (java.security.NoSuchAlgorithmException e) {
		    }
		    return null;
	}
	
	
	
}
