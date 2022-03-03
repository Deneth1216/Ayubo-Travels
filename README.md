# Ayubo-Travels
A C-sharp based system to maintain a car rental service
Ayubo Travels System
How to get started

Before starting the program, there are two things to complete.

    Update the SQL Server with the given Database File.
    Update the Database_Functions.cs file with the new connection string.

Importing Database to SQL Server

    Open Microsoft SQL Server Management Studio
    Connect the SQL Server with the Software by pressing connect
    Right-Click on the Database folder on the sidebar
    Click on Import Data-tier Application
    Click next on the intial pop-up
    In the next screen, select the Ayubotravels.bacpac folder in this project zip file.
    Follow the on screen instructions to update the database.

Updating the Connection string

    Head over to the project Zip Extracts

    Go to Krypton Test Folder

    Then Open Ayubo Travels.sln file with Visual Studio.

    Navigate to Database_Functions.cs class.

    From the View Tab in the topbar, find server explorer and open it

    Click on Connect to Database Icon and Copy your Server Name to the Popup

    Select the Imported Database form the dropdown and connect the database.

    Click on the added database on the Server Explorer Panel

    Find and copy the connection string from the properties panel

    Paste it in the following method in the database_functions.cs.

      public SqlConnection conOpen()
      {
          sql = @"Data Source=DESKTOP-K0VCCF8\SQLEXPRESS;Initial Catalog=AyuboTravels;Integrated Security=True";
          sqlCon = new SqlConnection(sql);
          sqlCon.Open();
          return sqlCon;
      }

    The new function should like like the following

      public SqlConnection conOpen()
      {
          sql = @"Connection sting that you copied";
          sqlCon = new SqlConnection(sql);
          sqlCon.Open();
          return sqlCon;
      }

    Setup Complete. Save the changes and close the editor.

Navigate to the following path to get started with the software.

    Krypton Test/KeyptonTest/Bin/Debug/KryptoonTest.exe

This will start the software from the Login Page. You can use the register function and use the software.
System Test User

For Testing purposes, you can also use the following login details, which has the administrator privillages to create a revenue statement PDF and also view order history.

    UserName : Deneth
    Password : Deneth1234


Thank you and Best Regards!

D.S.
