# Submission to the GDC for the XML-Technical Assignment 
# by Zackery Jaouni
The main program is XML_Email_Validator.cs 
When running the main method of the program:
    1.  It prompts the user for a file name.
    1.  Searches directory for a file with the indicated name.
        *   If the file cannot be found an error message will be displayed.
    1.  If the file exits the program will attempt to parse the file as an XML Document.
        *   If the file cannot be parsed and error message is displayed. This can occur from the desired file having permissions set to prevent access, if the file contains errors, the file type not being an XML, etc.
    1.  For each email address in the XML file the program will validate for valid syntax.
        *   Email addresses should be contained in elements named **emailAddress**.  If the element name is not **emailAddress** it will not be used.
        *   The C# library **using System.Net.Mail** and a regular expression for repeating dots **.** is used to verify if the email addresses have valid syntax.
    1.  The email addresses are seperated into a list of valid email addresses and invalid email addresses.
    1.  The program outputs the total number of valid email addresses followed by a list of the valid email addresses.
    1.  The program outputs the total number of invalid email addresses followed by a list of the invalid email addresses.

The Practice_Record.xml is a test file for the XML_Email_Validator.cs and contains 2 valid email addresses and 10 invalid email addresses

