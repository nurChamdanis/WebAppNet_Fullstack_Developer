-- getUserProfile.sql
SELECT Id, Email FROM [dbo].[Users] WHERE Email = @Email;
