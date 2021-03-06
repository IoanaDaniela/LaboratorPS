package ro.utcluj.ps.tema1.GUI;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.TextField;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTable;

import ro.utcluj.ps.tema1.BL.ShowManager;
import ro.utcluj.ps.tema1.BL.UserManager;
import ro.utcluj.ps.tema1.Models.Show;



public class AdminForm extends JFrame implements ActionListener{

	
	
	private static final long serialVersionUID = 1L;
	private JMenuBar menuBar;
	private JMenu meniu;
	private JMenuItem addEmployee, addShow , showList;
	private JPanel adminPanel;
	
	//ADD EMPLOYEE
	private JPanel addEmployeePanel; 
	private JLabel usernameLabel;
	private JLabel password1Label;
	private JLabel password2Label;
	private TextField usernameTextField;
	private TextField password1TextField;
	private TextField password2TextField;
	private JButton addEmployeeButton;
	
	
	//ADD SHOW
	private JPanel addShowPanel;
	private JLabel showNameLabel,  filmDirectorLabel, filmDistributionLabel, releasedOnLabel, availableTicketsLabel;
	private TextField showNameTextField,  filmDirectorTextField, filmDistributionTextField, releasedOnTextField, availableTicketsTextField;
	private JButton addShowButton;
	
	//SHOWS LIST
	private JPanel showsListPanel, updateShowPanel;
	private JLabel showNameUpdateLabel,  filmDirectorUpdateLabel, filmDistributionUpdateLabel, releasedOnUpdateLabel, availableTicketsUpdateLabel;
	private TextField showNameUpdateTextField,  filmDirectorUpdateTextField, filmDistributionUpdateTextField, releasedOnUpdateTextField, availableTicketsUpdateTextField;
	private JButton updateShowButton, deleteShowButton, saveChangesButton;
	private JTable tabel;
	private JScrollPane scrollPane;
	private Object[][] elemente;
	private String[] coloane = {"Show Name:","Show Director:","Show Distribution:", "Released on:", "TotalTickets:", "AvailableTickets:"};
	
	
	@SuppressWarnings("deprecation")
	public AdminForm(String title){
		super(title);
		
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		int screenHeight =(int)screenSize.getHeight();
		setSize(800,screenHeight);
		
		adminPanel = new JPanel(null);
		adminPanel.setLayout( null );
		adminPanel.setBounds(0,0,800,screenHeight);
		getContentPane().add(adminPanel);
		
	
		//MENIU
		menuBar = new JMenuBar();	 
		meniu = new JMenu("Menu");
				
		addEmployee = new JMenuItem("Add Employee");
		addEmployee.addActionListener(this);
		
		addShow = new JMenuItem("Add Show");
		addShow.addActionListener(this);
				
		showList = new JMenuItem("Shows List");
		showList.addActionListener(this);
		
		
		meniu.add(addEmployee);
		meniu.add(addShow);
		meniu.add(showList);
		
		menuBar.add(meniu);
		menuBar.setBounds(0,0,800,20);
		 
		
		//ADD EMPLOYEE
		addEmployeePanel = new JPanel(null);
		addEmployeePanel.setLayout(null);
		addEmployeePanel.setBounds(0, 20, 800, screenHeight-20);
		addEmployeePanel.setBackground(new Color(130,130,60));
		
		usernameLabel = new JLabel("Username:");
		usernameLabel.setFont(new Font(null,3,25));
		usernameLabel.setBounds(10,10, 200, 50);
		usernameLabel.setForeground(Color.WHITE);
		
		usernameTextField = new TextField(40);
		usernameTextField.setFont(new Font(null,3,25));
		usernameTextField.setBounds(210, 10, 300, 50);
	
		password1Label = new JLabel("Password:");
		password1Label.setFont(new Font(null,3,25));
		password1Label.setBounds(10,90,200,50);
		password1Label.setForeground(Color.WHITE);

		password1TextField = new TextField(40);
		password1TextField.setFont(new Font(null,3,25));
		password1TextField.setBounds(210, 90, 300, 50);
		password1TextField.setEchoChar('*');
		
		password2Label = new JLabel("Password:");
		password2Label.setFont(new Font(null,3,25));
		password2Label.setBounds(10,170,200,50);
		password2Label.setForeground(Color.WHITE);

		password2TextField = new TextField(40);
		password2TextField.setFont(new Font(null,3,25));
		password2TextField.setBounds(210, 170, 300, 50);
		password2TextField.setEchoChar('*');
		
		
		addEmployeeButton = new JButton("AddEmployee");
		addEmployeeButton.setFont(new Font(null,3,25));
		addEmployeeButton.setBounds(210, 250, 300, 50);
		addEmployeeButton.addActionListener(this);
		
		addEmployeePanel.add(usernameLabel);
		addEmployeePanel.add(usernameTextField);
		addEmployeePanel.add(password1Label);
		addEmployeePanel.add(password2Label);
		addEmployeePanel.add(password1TextField);
		addEmployeePanel.add(password2TextField);
		addEmployeePanel.add(addEmployeeButton);
		addEmployeePanel.hide();
		
		adminPanel.add(addEmployeePanel);
		adminPanel.add(menuBar);
		this.add(adminPanel);
		
		
		//ADD SHOW
		addShowPanel = new JPanel(null);
		addShowPanel.setLayout(null);
		addShowPanel.setBounds(0, 20, 800, screenHeight-20);
		addShowPanel.setBackground(new Color(130,130,60));
				
		showNameLabel = new JLabel("Show Name:");
		showNameLabel.setFont(new Font(null,3,25));
		showNameLabel.setBounds(10,10, 300, 50);
		showNameLabel.setForeground(Color.WHITE);
				
		showNameTextField = new TextField(40);
		showNameTextField.setFont(new Font(null,3,25));
		showNameTextField.setBounds(310, 10, 300, 50);
			
		filmDirectorLabel = new JLabel("Show Director:");
		filmDirectorLabel.setFont(new Font(null,3,25));
		filmDirectorLabel.setBounds(10,90,300,50);
		filmDirectorLabel.setForeground(Color.WHITE);

		filmDirectorTextField = new TextField(40);
		filmDirectorTextField.setFont(new Font(null,3,25));
		filmDirectorTextField.setBounds(310, 90, 300, 50);

				
		filmDistributionLabel = new JLabel("Show Distribution:");
		filmDistributionLabel.setFont(new Font(null,3,25));
		filmDistributionLabel.setBounds(10,170,300,50);
		filmDistributionLabel.setForeground(Color.WHITE);

		filmDistributionTextField = new TextField(40);
		filmDistributionTextField.setFont(new Font(null,3,25));
		filmDistributionTextField.setBounds(310, 170, 300, 50);

		
		releasedOnLabel = new JLabel("Released on:");
		releasedOnLabel.setFont(new Font(null,3,25));
		releasedOnLabel.setBounds(10,250,300,50);
		releasedOnLabel.setForeground(Color.WHITE);
		
		releasedOnTextField = new TextField(40);
		releasedOnTextField.setText("YYYY-MM-DD");
		releasedOnTextField.setFont(new Font(null,3,25));
		releasedOnTextField.setBounds(310, 250, 300, 50);

		
		availableTicketsLabel = new JLabel("Available Tickets:");
		availableTicketsLabel.setFont(new Font(null,3,25));
		availableTicketsLabel.setBounds(10,330,300,50);
		availableTicketsLabel.setForeground(Color.WHITE);
		
		availableTicketsTextField = new TextField(40);
		availableTicketsTextField.setFont(new Font(null,3,25));
		availableTicketsTextField.setBounds(310, 330, 300, 50);
		
				
		addShowButton = new JButton("AddShow");
		addShowButton.setFont(new Font(null,3,25));
		addShowButton.setBounds(310, 410, 300, 50);
		addShowButton.addActionListener(this);
				
		addShowPanel.add(showNameLabel);
		addShowPanel.add(showNameTextField);
		addShowPanel.add(filmDistributionLabel);
		addShowPanel.add(filmDistributionTextField);
		addShowPanel.add(filmDirectorLabel);
		addShowPanel.add(filmDirectorTextField);
		addShowPanel.add(releasedOnLabel);
		addShowPanel.add(releasedOnTextField);
		addShowPanel.add(availableTicketsLabel);
		addShowPanel.add(availableTicketsTextField);
		addShowPanel.add(addShowButton);
		addShowPanel.hide();
		adminPanel.add(addShowPanel);
		
		
		//SHOWS LIST
		showsListPanel = new JPanel(null);
		showsListPanel.setLayout(null);
		showsListPanel.setBounds(0, 20, 800, screenHeight-20);
		showsListPanel.setBackground(new Color(130,130,60));
						
		showNameUpdateLabel = new JLabel("Show Name:");
		showNameUpdateLabel.setFont(new Font(null,3,15));
		showNameUpdateLabel.setBounds(10,10, 300, 30);
		showNameUpdateLabel.setForeground(Color.WHITE);
						
		showNameUpdateTextField = new TextField(40);
		showNameUpdateTextField.setFont(new Font(null,3,15));
		showNameUpdateTextField.setBounds(310, 10, 300, 30);
					
		filmDirectorUpdateLabel = new JLabel("Show Director:");
		filmDirectorUpdateLabel.setFont(new Font(null,3,15));
		filmDirectorUpdateLabel.setBounds(10,50,300,30);
		filmDirectorUpdateLabel.setForeground(Color.WHITE);

		filmDirectorUpdateTextField = new TextField(40);
		filmDirectorUpdateTextField.setFont(new Font(null,3,15));
		filmDirectorUpdateTextField.setBounds(310, 50, 300, 30);

						
		filmDistributionUpdateLabel = new JLabel("Show Distribution:");
		filmDistributionUpdateLabel.setFont(new Font(null,3,15));
		filmDistributionUpdateLabel.setBounds(10,100,300,30);
		filmDistributionUpdateLabel.setForeground(Color.WHITE);

		filmDistributionUpdateTextField = new TextField(40);
		filmDistributionUpdateTextField.setFont(new Font(null,3,15));
		filmDistributionUpdateTextField.setBounds(310, 100, 300, 30);

				
		releasedOnUpdateLabel = new JLabel("Released on:");
		releasedOnUpdateLabel.setFont(new Font(null,3,15));
		releasedOnUpdateLabel.setBounds(10,150,300,30);
		releasedOnUpdateLabel.setForeground(Color.WHITE);
				
		releasedOnUpdateTextField = new TextField(40);
		releasedOnUpdateTextField.setText("YYYY-MM-DD");
		releasedOnUpdateTextField.setFont(new Font(null,3,15));
		releasedOnUpdateTextField.setBounds(310, 150, 300, 30);

				
		availableTicketsUpdateLabel = new JLabel("Available Tickets:");
		availableTicketsUpdateLabel.setFont(new Font(null,3,15));
		availableTicketsUpdateLabel.setBounds(10,200,300,30);
		availableTicketsUpdateLabel.setForeground(Color.WHITE);
				
		availableTicketsUpdateTextField = new TextField(40);
		availableTicketsUpdateTextField.setFont(new Font(null,3,15));
		availableTicketsUpdateTextField.setBounds(310, 200, 300, 30);
				
						
		deleteShowButton = new JButton("DeleteShow");
		deleteShowButton.setFont(new Font(null,3,15));
		deleteShowButton.setBounds(100, 311, 250, 30);
		deleteShowButton.addActionListener(this);
		
		updateShowButton = new JButton("UpdateShow");
		updateShowButton.setFont(new Font(null,3,15));
		updateShowButton.setBounds(450, 311, 250, 30);
		updateShowButton.addActionListener(this);
		
		
		saveChangesButton = new JButton("SaveChanges");
		saveChangesButton.setFont(new Font(null,3,15));
		saveChangesButton.setBounds(310, 250, 300, 30);
		saveChangesButton.addActionListener(this);
		
		
		updateShowPanel = new JPanel(null);
		updateShowPanel.setLayout(null);
		updateShowPanel.setBounds(0, 340, 800, (screenHeight-20)/2);
		updateShowPanel.setBackground(new Color(130,130,60)); 
						
		updateShowPanel.add(showNameUpdateLabel);
		updateShowPanel.add(showNameUpdateTextField);
		updateShowPanel.add(filmDistributionUpdateLabel);
		updateShowPanel.add(filmDistributionUpdateTextField);
		updateShowPanel.add(filmDirectorUpdateLabel);
		updateShowPanel.add(filmDirectorUpdateTextField);
		updateShowPanel.add(releasedOnUpdateLabel);
		updateShowPanel.add(releasedOnUpdateTextField);
		updateShowPanel.add(availableTicketsUpdateLabel);
		updateShowPanel.add(availableTicketsUpdateTextField);
		updateShowPanel.add(saveChangesButton);
		updateShowPanel.hide();
		
		showsListPanel.add(updateShowPanel);
		showsListPanel.add(deleteShowButton);
		showsListPanel.add(updateShowButton);
		showsListPanel.hide();
		adminPanel.add(showsListPanel);
		
		ShowManager sM = new ShowManager();
		elemente = sM.getShowsList();
		tabel = new JTable(elemente, coloane);
		tabel.setBounds(3, 10, 780, 300);
		scrollPane = new JScrollPane(tabel);
		tabel.setFillsViewportHeight(true);
		scrollPane.setBounds(3, 10, 780, 300);
		showsListPanel.add(scrollPane);
		
		
		
		
		show();
	}
	
	
	@SuppressWarnings("deprecation")
	@Override
	public void actionPerformed(ActionEvent e) {
		String command = e.getActionCommand();
		
		if(command.equals("Add Employee")){
			addEmployeePanel.show();
			addShowPanel.hide();
			showsListPanel.hide();
			show();
			
		}else if(command.equals("AddEmployee")){
			String pass1=password1TextField.getText();
			String pass2=password2TextField.getText();
			String username = usernameTextField.getText();
			if (!pass1.equals(pass2)){
				JOptionPane.showMessageDialog(null, "Parolele nu coincid!");
			}else{
				UserManager uM = new UserManager();
				uM.addUser(username, pass1);
				JOptionPane.showMessageDialog(null, "Inregistrat cu succes!");
			}
		}else if(command.equals("Add Show")){
			showsListPanel.hide();
			addEmployeePanel.hide();
			addShowPanel.show();
			show();
		}else if(command.equals("AddShow")){
			String showName =showNameTextField.getText();
			String regia =filmDirectorTextField.getText();
			String distributia = filmDistributionTextField.getText();
			String dataPremierei = releasedOnTextField.getText();
			String bilete =availableTicketsTextField.getText();
			dataPremierei.replace("-", "");
			
			ShowManager sM = new ShowManager();
			Show show = new Show(showName, regia, distributia, dataPremierei, bilete);
			sM.addShow(show);
			
			
			JOptionPane.showMessageDialog(null, "Inregistrat cu succes!");
			showNameTextField.setText("");
			filmDirectorTextField.setText("");
			filmDistributionTextField.setText("");
			releasedOnTextField.setText("YYYY-MM-DD");
			availableTicketsTextField.setText("");
			
		}else if(command.equals("Shows List")){
			
			
			ShowManager sM = new ShowManager();
			elemente = sM.getShowsList();
			showsListPanel.remove(scrollPane);
			tabel = new JTable(elemente, coloane);
			tabel.setBounds(3, 10, 780, 300);
			scrollPane = new JScrollPane(tabel);
			tabel.setFillsViewportHeight(true);
			scrollPane.setBounds(3, 10, 780, 300);
			showsListPanel.add(scrollPane);
			showsListPanel.revalidate();
			showsListPanel.repaint();	
			
			addEmployeePanel.hide();
			addShowPanel.hide();
			showsListPanel.show();
			show();
		}else if(command.equals("DeleteShow")){
			int index = tabel.getSelectedRow();
			if (index<0){
				JOptionPane.showMessageDialog(null, "Niciun specatcol selectat!");
			}else{
				ShowManager sM = new ShowManager();
				sM.deleteShow(String.valueOf(elemente[index][0]));
				
				showsListPanel.remove(scrollPane);
				elemente = sM.getShowsList();
				tabel = new JTable(elemente, coloane);
				tabel.setBounds(3, 10, 780, 300);
				scrollPane = new JScrollPane(tabel);
				tabel.setFillsViewportHeight(true);
				scrollPane.setBounds(3, 10, 780, 300);
				showsListPanel.add(scrollPane);
				
				showsListPanel.revalidate();
				showsListPanel.repaint();
				show();
			}
			
		}else if(command.equals("UpdateShow")){
			int index = tabel.getSelectedRow();
			if (index<0){
				JOptionPane.showMessageDialog(null, "Niciun specatcol selectat!");
			}else{
				
				
				showNameUpdateTextField.setText(elemente[index][0].toString());
				filmDirectorUpdateTextField.setText(elemente[index][1].toString());
				filmDistributionUpdateTextField.setText(elemente[index][2].toString());
				releasedOnUpdateTextField.setText(elemente[index][3].toString());
				availableTicketsUpdateTextField.setText(elemente[index][4].toString());
				updateShowPanel.show();

				
				showsListPanel.revalidate();
				showsListPanel.repaint();
				show();
			}
			
		}else if(command.equals("SaveChanges")){
			int index = tabel.getSelectedRow();
			if (index<0){
				JOptionPane.showMessageDialog(null, "Niciun specatcol selectat!");
			}else{
				
				String showName =showNameUpdateTextField.getText();
				String regia =filmDirectorUpdateTextField.getText();
				String distributia = filmDistributionUpdateTextField.getText();
				String dataPremierei = releasedOnUpdateTextField.getText();
				String bilete =availableTicketsUpdateTextField.getText();
			   	dataPremierei.replace("-", "");
	
				ShowManager sM = new ShowManager();
				sM.updateShow(elemente[index][0].toString(), showName, regia, distributia, dataPremierei, bilete);
				
				JOptionPane.showMessageDialog(null, "Modificat cu succes!");
				
				updateShowPanel.hide();
				showsListPanel.remove(scrollPane);
				elemente = sM.getShowsList();
				tabel = new JTable(elemente, coloane);
				tabel.setBounds(3, 10, 780, 300);
				scrollPane = new JScrollPane(tabel);
				tabel.setFillsViewportHeight(true);
				scrollPane.setBounds(3, 10, 780, 300);
				showsListPanel.add(scrollPane);
				
				showsListPanel.revalidate();
				showsListPanel.repaint();
				show();
			}
			
		}
		
	}

	
		
	
}
