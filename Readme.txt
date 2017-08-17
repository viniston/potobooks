This is the templete we use for our application development. Its based on asp.net webforms (framework 4.6) and uses SQL server 2014 database.
Please find the database at Database folder. You can find redundant tables and stored procedures. 
Try to configure this website in local IIS (http://127.0.0.50) pointing to physical path '\Phtobook.com\Photo.Presentation.Website'. Applicationpool should run under NETWORKSERVICE to get the database works as it is configured with Integrated windows authentication.

We have website which is the presentation layer. 'Photo.Business' does the business logic, security, Logging, entities etc

Try to familarize the framework structure and add on top of this. 


