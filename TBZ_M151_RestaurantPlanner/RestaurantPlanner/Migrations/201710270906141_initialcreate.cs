namespace RestaurantPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Menus",
                    c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        MenuName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MenuId);

            CreateTable(
                "dbo.Gerichte",
                c => new
                    {
                        GerichtId = c.Int(nullable: false, identity: true),
                        GerichtName = c.String(nullable: false),
                        GerichtPreis = c.Double(nullable: false),
                        MenuZugehoerigkeit_MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GerichtId)
                .ForeignKey("dbo.Menus", t => t.MenuZugehoerigkeit_MenuId, cascadeDelete: true)
                .Index(t => t.MenuZugehoerigkeit_MenuId);
            
            CreateTable(
                "dbo.Tageskarten",
                c => new
                    {
                        TageskarteId = c.Int(nullable: false, identity: true),
                        Ablaufdatum = c.DateTime(nullable: false),
                        MenuZugehoerigkeit_MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TageskarteId)
                .ForeignKey("dbo.Menus", t => t.MenuZugehoerigkeit_MenuId, cascadeDelete: true)
                .Index(t => t.MenuZugehoerigkeit_MenuId);
            
            CreateTable(
                "dbo.Getraenke",
                c => new
                    {
                        GetraenkId = c.Int(nullable: false, identity: true),
                        GetraenkName = c.String(nullable: false),
                        GetraenkPreis = c.Double(nullable: false),
                        HeissesGetraenk = c.Boolean(nullable: false, defaultValue: false),
                        AlkoholischesGetraenk = c.Boolean(nullable: false, defaultValue: false),
                        GetraenkMenge = c.Double(nullable: false),
                        MenuZugehoerigkeit_MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GetraenkId)
                .ForeignKey("dbo.Menus", t => t.MenuZugehoerigkeit_MenuId, cascadeDelete: true)
                .Index(t => t.MenuZugehoerigkeit_MenuId);
            
            CreateTable(
                "dbo.TageskarteGericht",
                c => new
                    {
                        TageskarteGerichtId = c.Int(nullable: false, identity: true),
                        Tageskarte_TageskarteId = c.Int(nullable: false),
                        Gericht_GerichtId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TageskarteGerichtId })
                .ForeignKey("dbo.Gerichte", t => t.Gericht_GerichtId)
                .ForeignKey("dbo.Tageskarten", t => t.Tageskarte_TageskarteId, cascadeDelete: true)
                .Index(t => t.Gericht_GerichtId)
                .Index(t => t.Tageskarte_TageskarteId);          
            
            CreateTable(
                "dbo.TageskarteGetraenk",
                c => new
                    {
                        TageskarteGetraenkId = c.Int(nullable: false, identity: true),
                        Getraenk_GetraenkId = c.Int(nullable: false),
                        Tageskarte_TageskarteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TageskarteGetraenkId })
                .ForeignKey("dbo.Getraenke", t => t.Getraenk_GetraenkId, cascadeDelete: true)
                .ForeignKey("dbo.Tageskarten", t => t.Tageskarte_TageskarteId)
                .Index(t => t.Getraenk_GetraenkId)
                .Index(t => t.Tageskarte_TageskarteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tageskarten", "MenuZugehoerigkeit_MenuId", "dbo.Menus");
            DropForeignKey("dbo.GetraenkTageskarten", "Tageskarte_TageskarteId", "dbo.Tageskarten");
            DropForeignKey("dbo.GetraenkTageskarten", "Getraenk_GetraenkId", "dbo.Getraenke");
            DropForeignKey("dbo.Getraenke", "MenuZugehoerigkeit_MenuId", "dbo.Menus");
            DropForeignKey("dbo.TageskarteGerichte", "Gericht_GerichtId", "dbo.Gerichte");
            DropForeignKey("dbo.TageskarteGerichte", "Tageskarte_TageskarteId", "dbo.Tageskarten");
            DropForeignKey("dbo.Gerichte", "MenuZugehoerigkeit_MenuId", "dbo.Menus");
            DropIndex("dbo.GetraenkTageskarten", new[] { "Tageskarte_TageskarteId" });
            DropIndex("dbo.GetraenkTageskarten", new[] { "Getraenk_GetraenkId" });
            DropIndex("dbo.TageskarteGerichte", new[] { "Gericht_GerichtId" });
            DropIndex("dbo.TageskarteGerichte", new[] { "Tageskarte_TageskarteId" });
            DropIndex("dbo.Getraenke", new[] { "MenuZugehoerigkeit_MenuId" });
            DropIndex("dbo.Tageskarten", new[] { "MenuZugehoerigkeit_MenuId" });
            DropIndex("dbo.Gerichte", new[] { "MenuZugehoerigkeit_MenuId" });
            DropTable("dbo.GetraenkTageskarten");
            DropTable("dbo.TageskarteGerichte");
            DropTable("dbo.Getraenke");
            DropTable("dbo.Tageskarten");
            DropTable("dbo.Menus");
            DropTable("dbo.Gerichte");
        }
    }
}
