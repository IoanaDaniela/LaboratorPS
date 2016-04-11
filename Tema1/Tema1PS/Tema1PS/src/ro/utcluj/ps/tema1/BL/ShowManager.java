package ro.utcluj.ps.tema1.BL;

import ro.utcluj.ps.tema1.DL.ShowDAO;
import ro.utcluj.ps.tema1.DL.TicketDAO;
import ro.utcluj.ps.tema1.Models.Show;
import ro.utcluj.ps.tema1.Models.Ticket;

public class ShowManager {

	
	
	
	
	public void addShow(Show spectacol){
		ShowDAO connection = new ShowDAO();
		connection.addShow(spectacol);
	}
	
	public Object[][] getShowsList(){
		ShowDAO connection = new ShowDAO();
		return connection.getShowList();
	}
	
	public void deleteShow(String show){
		ShowDAO connection = new ShowDAO();
		connection.deleteShow(show);
	}
	
	public void updateShow(String show,String showName, String regia, String distributia, String dataPremierei, String bilete){
		ShowDAO connection = new ShowDAO();
		connection.updateShow(show, showName, regia, distributia, dataPremierei, bilete);
	}
	
	public String[] getTicketsList(String show){
		TicketDAO connection = new TicketDAO();
		return connection.getTicketsList(show);
	}

	public boolean checkPlace(String showId, String row, String place) {
		TicketDAO connection = new TicketDAO();
		return connection.checkPlace(showId,row,place);
	}
	
	public boolean addTicket(Ticket bilet) {
		TicketDAO connectionTicket = new TicketDAO();
		ShowDAO connectionShow = new ShowDAO();
		if (connectionShow.availableTickets( bilet.getShowId() )>0){
			connectionTicket.addTicket(bilet);
			connectionShow.updateAvailableTickets(bilet.getShowId());
			return true;
		}
		return false;
		
	}

	
	
}
