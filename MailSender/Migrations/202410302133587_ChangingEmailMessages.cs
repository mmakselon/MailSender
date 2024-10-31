namespace MailSender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingEmailMessages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmailAccountParams", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.EmailAccountParams", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailAccountParams", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.EmailAccountParams", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
