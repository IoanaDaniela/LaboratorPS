package ro.utcluj.ps.tema1.GUI;


import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.Image;


import java.awt.Toolkit;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.JPanel;

@SuppressWarnings("serial")
public class JPanelWithBackground extends JPanel{
	
	
	//Adaugare imagine fundal
	private Image backgroundImage;
	
	public JPanelWithBackground(String fileName) throws IOException{
		backgroundImage = ImageIO.read(getClass().getResource(fileName));
	}
	
	public void paintComponent(Graphics g){
		super.paintComponent(g);
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
	//	int screenWidth = (int)screenSize.getWidth();
		int screenHeight =(int)screenSize.getHeight();
		g.drawImage(backgroundImage, 0, 0, 800, screenHeight, this);
	
	}
	
	//////////////////////////
	
	
	
	
	
}
