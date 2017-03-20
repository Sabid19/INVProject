namespace POSWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialidentitymodels : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ComCod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ComCod", c => c.String());
        }
    }
}
