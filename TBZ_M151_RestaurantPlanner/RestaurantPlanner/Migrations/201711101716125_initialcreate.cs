namespace RestaurantPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gericht",
                c => new
                    {
                        GerichtId = c.Int(nullable: false, identity: true),
                        GerichtName = c.String(nullable: false),
                        GerichtPreis = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GerichtId);

            CreateTable(
                    "dbo.Getraenk",
                    c => new
                    {
                        GetraenkId = c.Int(nullable: false, identity: true),
                        GetraenkName = c.String(nullable: false),
                        GetraenkPreis = c.Double(nullable: false),
                        HeissesGetraenk = c.Boolean(nullable: false),
                        AlkoholischesGetraenk = c.Boolean(nullable: false),
                        GetraenkMenge = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.GetraenkId);

            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        MenuName = c.String(nullable: false),
                        Gericht_GerichtId = c.Int(),
                        Getraenk_GetraenkId = c.Int(),
                    })
                .PrimaryKey(t => t.MenuId)
                .ForeignKey("dbo.Gericht", t => t.Gericht_GerichtId)
                .ForeignKey("dbo.Getraenk", t => t.Getraenk_GetraenkId)
                .Index(t => t.Gericht_GerichtId)
                .Index(t => t.Getraenk_GetraenkId);
            
            CreateTable(
                "dbo.Tageskarte",
                c => new
                    {
                        TageskarteId = c.Int(nullable: false, identity: true),
                        Ablaufdatum = c.DateTime(nullable: false),
                        MenuZugehoerigkeit_MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TageskarteId)
                .ForeignKey("dbo.Menu", t => t.MenuZugehoerigkeit_MenuId)
                .Index(t => t.MenuZugehoerigkeit_MenuId);
            
            CreateTable(
                "dbo.TageskarteGericht",
                c => new
                    {
                        Tageskarte_TageskarteId = c.Int(nullable: false),
                        Gericht_GerichtId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tageskarte_TageskarteId, t.Gericht_GerichtId })
                .ForeignKey("dbo.Tageskarte", t => t.Tageskarte_TageskarteId, cascadeDelete: true)
                .ForeignKey("dbo.Gericht", t => t.Gericht_GerichtId, cascadeDelete: true)
                .Index(t => t.Tageskarte_TageskarteId)
                .Index(t => t.Gericht_GerichtId);
            
            CreateTable(
                "dbo.GetraenkTageskarte",
                c => new
                    {
                        Getraenk_GetraenkId = c.Int(nullable: false),
                        Tageskarte_TageskarteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Getraenk_GetraenkId, t.Tageskarte_TageskarteId })
                .ForeignKey("dbo.Getraenk", t => t.Getraenk_GetraenkId, cascadeDelete: true)
                .ForeignKey("dbo.Tageskarte", t => t.Tageskarte_TageskarteId, cascadeDelete: true)
                .Index(t => t.Getraenk_GetraenkId)
                .Index(t => t.Tageskarte_TageskarteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tageskarte", "MenuZugehoerigkeit_MenuId", "dbo.Menu");
            DropForeignKey("dbo.GetraenkTageskarte", "Tageskarte_TageskarteId", "dbo.Tageskarte");
            DropForeignKey("dbo.GetraenkTageskarte", "Getraenk_GetraenkId", "dbo.Getraenk");
            DropForeignKey("dbo.Menu", "Getraenk_GetraenkId", "dbo.Getraenk");
            DropForeignKey("dbo.TageskarteGericht", "Gericht_GerichtId", "dbo.Gericht");
            DropForeignKey("dbo.TageskarteGericht", "Tageskarte_TageskarteId", "dbo.Tageskarte");
            DropForeignKey("dbo.Menu", "Gericht_GerichtId", "dbo.Gericht");
            DropIndex("dbo.GetraenkTageskarte", new[] { "Tageskarte_TageskarteId" });
            DropIndex("dbo.GetraenkTageskarte", new[] { "Getraenk_GetraenkId" });
            DropIndex("dbo.TageskarteGericht", new[] { "Gericht_GerichtId" });
            DropIndex("dbo.TageskarteGericht", new[] { "Tageskarte_TageskarteId" });
            DropIndex("dbo.Tageskarte", new[] { "MenuZugehoerigkeit_MenuId" });
            DropIndex("dbo.Menu", new[] { "Getraenk_GetraenkId" });
            DropIndex("dbo.Menu", new[] { "Gericht_GerichtId" });
            DropTable("dbo.GetraenkTageskarte");
            DropTable("dbo.TageskarteGericht");
            DropTable("dbo.Getraenk");
            DropTable("dbo.Tageskarte");
            DropTable("dbo.Menu");
            DropTable("dbo.Gericht");
        }
    }
}
