namespace MVCTema2PS.Migrations
{
    using MVCTema2PS.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCTema2PS.Models.ShowDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVCTema2PS.Models.ShowDBContext context)
        {
            new Show
            {
                ShowName = "Moarte si reincarnare intr-un cowboy",
                Director = "Andrei Majeri",
                Distribution = "Elena Ivanca,Romina Marei, Sanziana Tarta",
                ReleaseDate = DateTime.Parse("2016-02-19"),
                Tickets = 100,
                AvailableTickets = 100
            };

            new Show
            {
                ShowName = "Clasa Noastra",
                Director = "Bocsardi Laszlo",
                Distribution = "Sanziana Tarta, Irina Wintze, Anca Hanu, Cristian Rigman, Radu Largeanu,Miron Maxim",
                ReleaseDate = DateTime.Parse("2015-12-11"),
                Tickets = 100,
                AvailableTickets = 100
            };

            new Show
            {
                ShowName = "BZZAP!",
                Director = "Razvan Muresa",
                Distribution = "Matei Rotaru, Cristian Grosu , Radu Largeanu , Diana Buluga, Alexandra Tarce,Patricia Brad ",
                ReleaseDate = DateTime.Parse("2016-01-29"),
                Tickets = 100,
                AvailableTickets = 100
            };

            new Show
            {
                ShowName = "RICHARD AL III-LEA SE INTERZICE",
                Director = "Matei Rotaru",
                Distribution = "Cristian Grosu, Alexandra Tarce, Miron Maxim, Radu Largeanu,Patricia Brad, Diana Buluga, Cristian Rigman, Silvius Iorga",
                ReleaseDate = DateTime.Parse("2015-09-18"),
                Tickets = 100,
                AvailableTickets = 100
            };

            new Show
            {
                ShowName = "TZARA ARDE SI DADA SE PIAPTANA (Fantoma de la Elsinore)",
                Director = "Stefana Pop-Curseu",
                Distribution = "Filip Odangiu,Rares Stoica, Catalin Codreanu,Cristian Grosu",
                ReleaseDate = DateTime.Parse("2016-02-09"),
                Tickets = 100,
                AvailableTickets = 100
            };

        }
        
    }
}
