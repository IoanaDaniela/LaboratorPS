package ro.utcluj.ps.tema1.GUI;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.TextField;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;

import ro.utcluj.ps.tema1.BL.UserManager;
import ro.utcluj.ps.tema1.Models.User;

public class LoginForm extends JFrame implements ActionListener{

	private static final long serialVersionUID = 1L;
	private JPanelWithBackground backgroundPanel = null;

	private AdminForm adminForm;
	private EmployeeForm employeeForm;
	
	private JLabel usernameLabel, passwordLabel;
	private TextField usernameTextField, passwordTextField;
	private JButton signInButton,forgotPasswordButton;
	
	
	@SuppressWarnings("deprecation")
	public LoginForm(String titlu){
		super(titlu);
		
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		int screenHeight =(int)screenSize.getHeight();
		setSize(800,screenHeight);
		
		
		
		try {
			backgroundPanel = new JPanelWithBackground("back.jpeg");
		} catch (IOException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		

		getContentPane().add(backgroundPanel);
		backgroundPanel.setLayout( null );
		
		backgroundPanel.setBounds(0,0,800,screenHeight);
	
		usernameLabel = new JLabel("Username:");
		usernameLabel.setFont(new Font(null,3,25));
		usernameLabel.setBounds(10,10, 200, 50);
		usernameLabel.setForeground(Color.RED);
		
		
		usernameTextField = new TextField(40);
		usernameTextField.setFont(new Font(null,3,25));
		usernameTextField.setBounds(210, 10, 300, 50);
	
		
		passwordLabel = new JLabel("Password:");
		passwordLabel.setFont(new Font(null,3,25));
		passwordLabel.setBounds(10,90,200,50);
		passwordLabel.setForeground(Color.RED);

		passwordTextField = new TextField(40);
		passwordTextField.setFont(new Font(null,3,25));
		passwordTextField.setBounds(210, 90, 300, 50);
		passwordTextField.setEchoChar('*');
		
		signInButton = new JButton("LogIn");
		signInButton.setFont(new Font(null,3,25));
		signInButton.setBounds(10, 170, 150, 50);
		signInButton.addActionListener(this);
		
		forgotPasswordButton = new JButton("ForgotPassword");
		forgotPasswordButton.setFont(new Font(null,3,10));
		forgotPasswordButton.setBounds(10, 250, 150, 30);
		forgotPasswordButton.addActionListener(this);
		forgotPasswordButton.setBackground(this.getBackground());
				
		backgroundPanel.add(forgotPasswordButton);
		backgroundPanel.add(usernameLabel);
		backgroundPanel.add(usernameTextField);
		backgroundPanel.add(passwordLabel);
		backgroundPanel.add(passwordTextField);
		backgroundPanel.add(signInButton);

		backgroundPanel.show();
		
	}


	@SuppressWarnings("deprecation")
	@Override
	public void actionPerformed(ActionEvent e) {
		String command = e.getActionCommand();
		if(command.equals("LogIn")){
			
			String username = usernameTextField.getText();
			String password = passwordTextField.getText();
			
			UserManager uM = new UserManager();
            User utilizator = uM.login(username, password);
			
            if(utilizator == null){
            	JOptionPane.showMessageDialog(null, "Username sau parola gresita!");
            }else if (utilizator.getUsertype().equals("admin")){
            	//ADMIN
            	String title = "Logged As: " + utilizator.getUsername();
            	adminForm = new AdminForm(title);
        		adminForm.show();
            }else{
            	//EMPLOYEE
            	String title = "Logged As: " + utilizator.getUsername();
            	employeeForm = new EmployeeForm(title);
        		employeeForm.show();
        		
            	
            }
            
		}else if(command.equals("ForgotPassword")){
			String username = usernameTextField.getText();
			if (username.equals("")){
				JOptionPane.showMessageDialog(null, "Introduceti Username!");
			}else{
				UserManager uM = new UserManager();
				String newPassword = uM.resetPassword(username);
				if (newPassword.equals("")){
					JOptionPane.showMessageDialog(null, "Acest user nu este inregistrat!");
				}else{
					JOptionPane.showMessageDialog(null, "Noua Parola : " + newPassword);
				}
			}
			
            
		}
	}
	

}
