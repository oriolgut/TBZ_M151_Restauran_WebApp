namespace RestaurantPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class corrected_references : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GetraenkTageskarte", newName: "TageskarteGetraenk");
            DropForeignKey("dbo.Menu", "Gericht_GerichtId", "dbo.Gericht");
            DropForeignKey("dbo.Menu", "Getraenk_GetraenkId", "dbo.Getraenk");
            DropIndex("dbo.Menu", new[] { "Gericht_GerichtId" });
            DropIndex("dbo.Menu", new[] { "Getraenk_GetraenkId" });
            DropPrimaryKey("dbo.TageskarteGetraenk");
            CreateTable(
                "dbo.MenuGericht",
                c => new
                    {
                        Menu_MenuId = c.Int(nullable: false),
                        Gericht_GerichtId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Menu_MenuId, t.Gericht_GerichtId })
                .ForeignKey("dbo.Menu", t => t.Menu_MenuId, cascadeDelete: true)
                .ForeignKey("dbo.Gericht", t => t.Gericht_GerichtId, cascadeDelete: true)
                .Index(t => t.Menu_MenuId)
                .Index(t => t.Gericht_GerichtId);
            
            CreateTable(
                "dbo.GetraenkMenu",
                c => new
                    {
                        Getraenk_GetraenkId = c.Int(nullable: false),
                        Menu_MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Getraenk_GetraenkId, t.Menu_MenuId })
                .ForeignKey("dbo.Getraenk", t => t.Getraenk_GetraenkId, cascadeDelete: true)
                .ForeignKey("dbo.Menu", t => t.Menu_MenuId, cascadeDelete: true)
                .Index(t => t.Getraenk_GetraenkId)
                .Index(t => t.Menu_MenuId);
            
            AddPrimaryKey("dbo.TageskarteGetraenk", new[] { "Tageskarte_TageskarteId", "Getraenk_GetraenkId" });
            DropColumn("dbo.Menu", "Gericht_GerichtId");
            DropColumn("dbo.Menu", "Getraenk_GetraenkId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menu", "Getraenk_GetraenkId", c => c.Int());
            AddColumn("dbo.Menu", "Gericht_GerichtId", c => c.Int());
            DropForeignKey("dbo.GetraenkMenu", "Menu_MenuId", "dbo.Menu");
            DropForeignKey("dbo.GetraenkMenu", "Getraenk_GetraenkId", "dbo.Getraenk");
            DropForeignKey("dbo.MenuGericht", "Gericht_GerichtId", "dbo.Gericht");
            DropForeignKey("dbo.MenuGericht", "Menu_MenuId", "dbo.Menu");
            DropIndex("dbo.GetraenkMenu", new[] { "Menu_MenuId" });
            DropIndex("dbo.GetraenkMenu", new[] { "Getraenk_GetraenkId" });
            DropIndex("dbo.MenuGericht", new[] { "Gericht_GerichtId" });
            DropIndex("dbo.MenuGericht", new[] { "Menu_MenuId" });
            DropPrimaryKey("dbo.TageskarteGetraenk");
            DropTable("dbo.GetraenkMenu");
            DropTable("dbo.MenuGericht");
            AddPrimaryKey("dbo.TageskarteGetraenk", new[] { "Getraenk_GetraenkId", "Tageskarte_TageskarteId" });
            CreateIndex("dbo.Menu", "Getraenk_GetraenkId");
            CreateIndex("dbo.Menu", "Gericht_GerichtId");
            AddForeignKey("dbo.Menu", "Getraenk_GetraenkId", "dbo.Getraenk", "GetraenkId");
            AddForeignKey("dbo.Menu", "Gericht_GerichtId", "dbo.Gericht", "GerichtId");
            RenameTable(name: "dbo.TageskarteGetraenk", newName: "GetraenkTageskarte");
        }
    }
}
