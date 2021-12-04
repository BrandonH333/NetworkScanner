Run Project:
1) Get MailTrap account
https://mailtrap.io/

2) Update the following values within EmailConstants.cs with values from MailTrap
SmtpEndpoint = "smtp.mailtrap.io"
SmtpPort = 
SslEnabled = false
SslUsername = ""
SslPassword = ""

3) Create Twilio account
https://www.twilio.com/try-twilio

4) Update the following values within PhoneConstants.cs with the values form Twilio
AccountSid = ""
AuthToken = ""
FromNumber = ""

