using System;
using System.IO;
using System.Collections;
using System.Xml;
using System.Net.Mail;
using System.Text.RegularExpressions;

// Author: Zackery Jaouni
// Last updated: 7/6/2021
// Description: Attempts to parse an XML file for email address and
// seperates the valid and invalid email addresses into seperate lists
// Updated: Now checks each record for a first name and last name in addition
// to an email address.  
namespace XML_Technical_Assignment
{
    public class EmailValidator
    {
        private XmlDocument recordsDoc = new XmlDocument(); // Represents a XML file to parse for email adresses
        private ArrayList usersArray = new ArrayList(); // Array list of users in a XML file
        private ArrayList validEmails = new ArrayList(); // Array list of valid emails in a XML file
        private ArrayList invalidEmails = new ArrayList(); // Array list of invalid email in a XML file

        // Constructor that takes a name of a XML file as a parameter.
        // Attempts to find the XML file in the directory and parse it.
        // If the file can be parsed the method createListsOfEmails is
        // called to create a lists of valid and invalid emails.
        // If the file cannot be found or could not be parsed an error 
        // message is displayed.
        public EmailValidator(String fileName)
        {
            if(isFileInDirectory(fileName))
            {
                try
                {
                    recordsDoc.Load(fileName);
                    interateThroughUsers();
                    Console.Write("Number of users: "+usersArray.Count);
                    createListsOfEmails();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: " + fileName + " could not be parsed as an xml file " + e); 
                }
            }
        }

        // Determine if a file can be located in the current directory and return true.
        // If the file could not be located returns false and an error message is displayed.
        // The parameter fileName is the name of the file to locate.
        public Boolean isFileInDirectory(String fileName)
        {
            if (File.Exists(fileName))
            {
                Console.WriteLine(fileName + " was successfully found in the directory.");
                return true;
            }
            Console.WriteLine("Error: "+ fileName + " could not be located in the directory!");
            return false;
        }

        // Iterates through each user in the XML file and creates a array of users.
        // Each user should have an Element labeled firstName, lastName, and emailAddress.
        // If an element is missing or blank in the XML file the array will set the value as N/a
        public void interateThroughUsers()
        {
            XmlNodeList userNodes = recordsDoc.SelectNodes("/users/user");
            foreach (XmlNode user in userNodes)
            {
               UserInfo u = new UserInfo(user);
               usersArray.Add(u);
            }
        }

        // Iterates through each record of a XML file.
        // For each email address the method isValidEmail is used to determine if the   
        // email address should be added to validEmails or invalidEmails.
        // (Note: Only email addresses contained in a data elements named, 
        // emailAddress, are considered for the lists)
        public void createListsOfEmails()
        { 
            XmlNodeList emailList = recordsDoc.GetElementsByTagName("emailAddress");
            //for (int i=0; i < emailList.Count; i++)
            foreach (UserInfo user in usersArray)
            {
                //String email = user[2];
                if(isValidEmail(user.getEmailAddress()))
                {
                    validEmails.Add(user);
                }
                else
                {
                    invalidEmails.Add(user);
                }
            }
        }

        // Determines if an email address is in valid syntax.
        // The parameter email is the name of the email address to validate.
        public Boolean isValidEmail(String email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                Regex repeatedSpecial = new Regex(@"\.{2,}");  // Pattern for repeated dot . 
                if(repeatedSpecial.IsMatch(email) || !endOfEmailValidator(email))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // Determines if an email address has the ends in any two letters.
        // The parameter email is the name of the email address to validate.
        public Boolean endOfEmailValidator(String email)
        {
            MailAddress address = new MailAddress(email);
            Regex endDomain = new Regex(@"\.[a-zA-Z]{2,}$"); // Pattern for ending in any two or more letters after the dot
            if(endDomain.IsMatch(email)) //Check that the last two characters of an email address is two letters
            {
                return true;
            }
            return false;
        }

        // Outputs the list of valid Emails
        public void displayValidEmails()
        {
            Console.WriteLine("There are "+ validEmails.Count + " valid emails");
            Console.WriteLine("The list of valid emails:");
            foreach(UserInfo user in validEmails)
            {
                Console.WriteLine("First Name: " + user.getFirstName() + ", Last Name: " + 
                user.getLastName() +", Email Address: " + user.getEmailAddress());
            }  
        }

        // Outputs the list of invalid Emails
        public void displayInvalidEmails()
        {
            Console.WriteLine("There are "+ invalidEmails.Count + " invalid emails");
            Console.WriteLine("The list of invalid emails:");
            foreach(UserInfo user in invalidEmails)
            {
                Console.WriteLine("First Name: " + user.getFirstName() + ", Last Name: " + 
                user.getLastName() +", Email Address: " + user.getEmailAddress());
            }
        }

        // Prompts the user to enter an XML file to search for in the directory.
        // If the XML file can be succussefully parsed then the lists of
        // valid and invalid emails are displayed. 
        // If the file could not be located or could not be parsed an appropriate
        // error message is displayed
        static void Main(string[] args)
        {
            Console.Write("Enter the name of an XML file to read: ");
            String fileName = Console.ReadLine();           
            EmailValidator eV = new EmailValidator(fileName);
            Console.WriteLine();
            eV.displayValidEmails();
            Console.WriteLine();
            eV.displayInvalidEmails();
        }
    }    

    // Holds the info for a user from an XmlNode.
    // The infromation for a user includes first name, last name,
    // and email address.
    public class UserInfo
    {
        XmlNode user;
        String firstName;
        String lastName;
        String email;
        public UserInfo(XmlNode user)
        {
            this.user = user;
            setFirstName();
            setLastName();
            setEmailAddress();
        }
        // Finds the lastName for a user in the XML file.
        // If the user does not have a section for last name or if the
        // last name is left blank then the last name is set as N/a
        public void setFirstName()
        {
            try{
                firstName = user.SelectSingleNode("firstName").InnerText;
                if(isBlank(firstName))
                { 
                    firstName = "N/a";
                }
            }
            catch(Exception){
              firstName = "N/a";
            }
        }

        // Finds the first name for a user in the XML file.
        // If the user does not have a section for first name or if the
        // first name is left blank then the frist name is set as N/a
        public void setLastName()
        {
            try{
                lastName = user.SelectSingleNode("lastName").InnerText;
                Regex blank = new Regex(@"^\s*$");
                if(isBlank(lastName))
                { 
                    lastName = "N/a";
                }
            }
            catch(Exception){
              lastName = "N/a";
            }
        }

        // Finds the email address for a user in the XML file.
        // If the user does not have a section for email address or if the
        // email address is left blank then the email address is set as N/a
        public void setEmailAddress()
        {
            try{
                email = user.SelectSingleNode("emailAddress").InnerText;
                if(isBlank(email))
                { 
                    email = "N/a";
                }
            }
            catch(Exception){
              email = "N/a";
            }
        }

        // Determines if a string only contains white space.
        public Boolean isBlank(String s)
        {
            Regex blank = new Regex(@"^\s*$");
            if(blank.IsMatch(s))
            { 
                return true;
            }
            return false;
        }

        // Returns the first name 
        public String getFirstName()
        {
            return firstName;
        }

        // Returns the last name 
        public String getLastName()
        {
            return lastName;
        }

        // Returns the email address
        public String getEmailAddress()
        {
            return email;
        }
    }
}