namespace JobsProjectApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forvalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "cityName", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "countryName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Countries", "countryName", c => c.String());
            AlterColumn("dbo.Cities", "cityName", c => c.String());
        }
    }
}
