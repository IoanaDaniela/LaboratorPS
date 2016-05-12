namespace MVCTema2PS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        ShowID = c.Int(nullable: false, identity: true),
                        ShowName = c.String(),
                        Director = c.String(),
                        Distribution = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Tickets = c.Int(nullable: false),
                        AvailableTickets = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShowID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Shows");
        }
    }
}
