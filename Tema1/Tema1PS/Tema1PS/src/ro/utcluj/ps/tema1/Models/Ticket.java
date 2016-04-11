package ro.utcluj.ps.tema1.Models;

public class Ticket {
	private String showId;
	private String row;
	private String place;
	
	

	
	public Ticket(String showId, String row, String place) {
		super();
		this.showId = showId;
		this.row = row;
		this.place = place;
	}
	
	
	public String getShowId() {
		return showId;
	}
	public String getRow() {
		return row;
	}
	public String getPlace() {
		return place;
	}
	
	
	
	
	
}
