package ro.utcluj.ps.tema1.GUI;

import java.awt.Choice;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.List;
import java.awt.TextField;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;

import ro.utcluj.ps.tema1.BL.ShowManager;
import ro.utcluj.ps.tema1.Models.Ticket;


public class EmployeeForm extends JFrame implements ActionListener{
	
	private static final long serialVersionUID = 1L;

	private JLabel chooseShowLabel,listaBileteLabel, addTicketLabel, rowLabel, placeLabel;
	private TextField rowTextField, placeTextField; 
	private Choice showChoice;
	private JPanel employeePanel;
	private List listaBilete;
	private JButton showTicketsButton, submitButton;
	private Object[][] spectacole;
	
	
	@SuppressWarnings("deprecation")
	public EmployeeForm(String title){
		super(title);
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		int screenHeight =(int)screenSize.getHeight();
		setSize(800,screenHeight);
		
		employeePanel = new JPanel(null);
		employeePanel.setLayout( null );
		employeePanel.setBounds(0,0,800,screenHeight);
		employeePanel.setBackground(new Color(130,130,60));
		getContentPane().add(employeePanel);
		
		
		chooseShowLabel = new JLabel("Choose Show:");
		chooseShowLabel.setFont(new Font(null,3,25));
		chooseShowLabel.setBounds(20,10,200,50);
		chooseShowLabel.setForeground(Color.WHITE);
		
		
		showChoice = new Choice();
		ShowManager sM = new ShowManager();
		spectacole = sM.getShowsList();
		int i=0;
		while(spectacole[i][0]!=null){
			showChoice.add(spectacole[i][0].toString());
			i++;
		}
		showChoice.setBounds(20, 60, 600, 30);
		
		listaBileteLabel = new JLabel("Thickets List:");
		listaBileteLabel.setFont(new Font(null,3,25));
		listaBileteLabel.setBounds(320,100,200,30);
		listaBileteLabel.setForeground(Color.WHITE);
		
		addTicketLabel = new JLabel("Add Ticket:");
		addTicketLabel.setFont(new Font(null,3,25));
		addTicketLabel.setBounds(20,100,200,30);
		addTicketLabel.setForeground(Color.WHITE);
		
		rowLabel = new JLabel("row:");
		rowLabel.setFont(new Font(null,3,15));
		rowLabel.setBounds(20,150,100,30);
		rowLabel.setForeground(Color.WHITE);
		
		rowTextField = new TextField(40);
		rowTextField.setFont(new Font(null,3,15));
		rowTextField.setBounds(120, 150, 100, 30);
		
		placeLabel = new JLabel("place:");
		placeLabel.setFont(new Font(null,3,15));
		placeLabel.setBounds(20,200,100,30);
		placeLabel.setForeground(Color.WHITE);
		
		placeTextField = new TextField(40);
		placeTextField.setFont(new Font(null,3,15));
		placeTextField.setBounds(120, 200, 100, 30);
		
		submitButton = new JButton("Submit");
		submitButton.setFont(new Font(null,3,15));
		submitButton.setBounds(20, 250, 200, 30);
		submitButton.addActionListener(this);
		
		
		showTicketsButton = new JButton("ShowTickets");
		showTicketsButton.setFont(new Font(null,3,15));
		showTicketsButton.setBounds(320, 550, 300, 30);
		showTicketsButton.addActionListener(this);
		
		listaBilete = new List();
		listaBilete.setBounds(320, 140, 290,400);
		
		employeePanel.add(submitButton);
		employeePanel.add(rowTextField);
		employeePanel.add(placeTextField);
		employeePanel.add(rowLabel);
		employeePanel.add(placeLabel);
		employeePanel.add(addTicketLabel);
		employeePanel.add(showTicketsButton);
		employeePanel.add(listaBileteLabel);
		employeePanel.add(listaBilete);
		employeePanel.add(chooseShowLabel);
		employeePanel.add(showChoice);
		
		show();
	}
	
	

	@SuppressWarnings("deprecation")
	@Override
	public void actionPerformed(ActionEvent e) {
		String command = e.getActionCommand();
		if(command.equals("ShowTickets")){
			int index = showChoice.getSelectedIndex();
			if (index < 0){
				JOptionPane.showMessageDialog(null, "Niciun specatcol selectat!");
			}else{
				employeePanel.remove(listaBilete);
				String show = spectacole[index][6].toString();
				ShowManager sM= new ShowManager();
				String[] bilete = sM.getTicketsList(show);
				int i = 0;
				listaBilete.removeAll();
				while(bilete[i]!= null){
					listaBilete.add(bilete[i]);
					i++;
				}
				employeePanel.add(listaBilete);
				employeePanel.revalidate();
				employeePanel.repaint();
				show();
				
			}
		
            
		}else if (command.equals("Submit")){
			int index = showChoice.getSelectedIndex();
			if (index < 0){
				JOptionPane.showMessageDialog(null, "Niciun specatcol selectat!");
			}else{
				String showId = spectacole[index][6].toString();
				String place = placeTextField.getText();
				String row = rowTextField.getText();
				ShowManager sM= new ShowManager();
				if(!sM.checkPlace(showId,row,place)){
					JOptionPane.showMessageDialog(null, "Loc indisponibil!");
				}else{
					Ticket bilet = new Ticket(showId, row, place);
					if(sM.addTicket(bilet)){ 
						JOptionPane.showMessageDialog(null, "Bilet adaugat!");
					}else{
						JOptionPane.showMessageDialog(null, "Nu mai sunt locuri pentru acestu spectacol!");
					}
				}
				System.out.println("Id SHow: " + showId);
				listaBilete.removeAll();
				employeePanel.remove(listaBilete);
				String[] bilete = sM.getTicketsList(showId);
				int i = 0;
				while(bilete[i]!= null){
					listaBilete.add(bilete[i]);
					i++;
				}
				employeePanel.add(listaBilete);
				employeePanel.revalidate();
				employeePanel.repaint();
				show();
				
				
			}
		
            
		}
		
	}
	
	
	
}
