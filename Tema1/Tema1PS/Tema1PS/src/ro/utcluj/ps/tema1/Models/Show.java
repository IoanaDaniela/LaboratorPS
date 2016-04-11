package ro.utcluj.ps.tema1.Models;

public class Show {
	private String showName;
	private String regia;
	private String distributia;
	private String dataPremierei;
	private String bilete;
	
	
	public Show(String showName, String regia, String distributia,
			String dataPremierei, String bilete) {
		super();
		this.showName = showName;
		this.regia = regia;
		this.distributia = distributia;
		this.dataPremierei = dataPremierei;
		this.bilete = bilete;
	}


	public String getShowName() {
		return showName;
	}


	public String getRegia() {
		return regia;
	}


	public String getDistributia() {
		return distributia;
	}


	public String getDataPremierei() {
		return dataPremierei;
	}


	public String getBilete() {
		return bilete;
	}
	
	
	
	
}
