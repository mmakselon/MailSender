namespace MailSender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOnModelCreating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmailMessages", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.EmailMessages", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailMessages", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.EmailMessages", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
