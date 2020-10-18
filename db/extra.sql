create trigger tr_dataUserHistory_ins
on [dbo].[UserDataHistory]
after insert, update
as
begin
set nocount on;
