# Submission to the GDC for the XML-Technical Assignment 
## by Zackery Jaouni
## Description:
The purpose of this program is to be able to take a file name from a user and determine if the file is an XML document that exists in the directory.  If the file is an XML document it can be parsed for all email addresses contained in the file and seperate them into two lists of valid and invalid syntax.<br> <br>
## Main program: XML_Email_Validator.cs<br>
**When running the main method of the program:**<br>
    1. It prompts the user for a file name.<br>
    2. Searches directory for a file with the indicated name.<br>
        - If the file cannot be found an error message will be displayed.<br>
    3. If the file exits the program will attempt to parse the file as an XML Document.<br>
         - If the file cannot be parsed and error message is displayed. This can occur from the desired file having permissions set to prevent access, if the file contains errors, the file type not being an XML, etc.<br>
    4. For each email address in the XML file the program will validate for valid syntax.<br>
        - Email addresses should be contained in elements named **emailAddress**.  If the element name is not **emailAddress** it will not be used.<br>
        - The C# library **using System.Net.Mail** and a regular expression for repeating dots **.** is used to verify if the email addresses have valid syntax.<br>
        - The email address should end with a top-level domanin such as .com, .edu, .uk, .org, etc. If the email address does not have the appropriate ending it will be marked invalid.<br> 
        **Important note:** If any number of spaces are included after an email address it will be marked invalid!<br>
    5. The email addresses are seperated into a list of valid email addresses and invalid email addresses.<br>
    6. The program outputs the total number of valid email addresses followed by a list of the valid email addresses.<br>
    7. The program outputs the total number of invalid email addresses followed by a list of the invalid email addresses.<br> <br>

## Other files:
**Practice_Record.xml** is a test file for the XML_Email_Validator.cs and contains 3 valid email addresses and 8 invalid email addresses <br>

**Empty.xml** file can be used to test the results of an XML file that contains no email addresses.  In this case the file can be parsed so no error messages are displayed and both the valid and invalid lists of email addresses are empty.