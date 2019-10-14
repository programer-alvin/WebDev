using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicWeb
{
    public class ContentBuilder
    {
        static String nameandvalue = @"name(\r*)(\n*)(\t*)( *):(\r*)(\n*)(\t*)( *)'(\r*)(\n*)(\t*)( *)([A-Za-z]{1,}((\r*)(\n*)(\t*)( *)[A-Za-z])*)(\r*)(\n*)(\t*)( *)'";
        static String choicebracket = @"\{(\r*)(\n*)(\t*)( *)" + nameandvalue + @"(\r*)(\n*)(\t*)( *)\}(\r*)(\n*)(\t*)( *),(\r*)(\n*)(\t*)( *)";
        static String choiceblock = @"name(\r*)(\n*)(\t*)( *):(\r*)(\n*)(\t*)( *)'(\r*)(\n*)(\t*)( *)([A-Za-z]{1,}((\r*)(\n*)(\t*)( *)[A-Za-z])*)(\n*)(\t*)( *)'(\n*)(\t*)( *)," +
            @"(\r*)(\n*)(\t*)( *)choices(\r*)(\n*)(\t*)( *):(\r*)(\n*)(\t*)( *)\[" +
            @"(\r*)(\n*)(\t*)( *)(" + choicebracket + "){1,}" +
            @"\]";
        static String relatedblock = @"related(\r*)(\n*)(\t*)( *):(\r*)(\n*)(\t*)( *)\[(\r*)(\n*)(\t*)( *)" +
            @"(\{(\r*)(\n*)(\t*)( *)" +
            choiceblock +
            @"(\r*)(\n*)(\t*)( *)\}(\r*)(\n*)(\t*)( *),*(\r*)(\n*)(\t*)( *)){0,}" +
            @"\]";
        String mainblock = choiceblock + "(\r*)(\n*)(\t*)( *),(\r*)(\n*)(\t*)( *)" +
            relatedblock;
        List<String> checkbox_IDs = new List<string>();//keeps track of check box ids to avoid duplicate ids

        



            ////lbl_test.Text = text;
            //analyzeText(readFile("menu.data"));
        

        public String readFile(string full_file_path)
        {
            //string mappath = Server.MapPath("~/");//@"D:\Project\The Restaurant\"
            //string mappath = @"D:\Project\The Restaurant\";
            string text = File.ReadAllText(full_file_path);
            return text;
        }

        public string analyzeText(String text)
        {
            if (text == "")
            {
                throw new Exception("File should not be empty");
            }
            int number_of_opening_brace = text.Split('[').Length - 1;
            int number_of_closing_brace = text.Split(']').Length - 1;
            int number_of_opening_bracket = text.Split('{').Length - 1;
            int number_of_closing_bracket = text.Split('}').Length - 1;
            if (number_of_opening_brace != number_of_closing_brace)
            {
                throw new ArgumentException("Data not well fomated. Kindly make sure braces balance");
            }
            if (number_of_opening_bracket != number_of_closing_bracket)
            {
                throw new ArgumentException("Data not well fomated. Kindly make sure brackets balance");
            }
            //building patterns for retrieving details

            //developWholeBlockContent(string text);
            String page_contents = "<!DOCTYPE html><html><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><link rel=\"stylesheet\" href=\"main.css\"><link rel=\"stylesheet\" href=\"style.css\"><title>The Restaurant</title></head><body><header><h1>The Restaurant</h1></header><div><div class=\"dropdown\"><h2>Restaurant Menu</h2><div class=\"name\">";
            List<String> main_block_contents = getStringsMatchingThePattern(text, mainblock);
            if (main_block_contents.Count == 0)
            {
                throw new ArgumentException("No valid Main Block was found");
            }
            foreach (String main_block_content in main_block_contents)
            {
                page_contents += developMainSection(main_block_content);
            }
            page_contents += "</div></div></div><!--load javascript at botom so as to capture all html tags--><script type=\"text/javascript\" src=\"dropdownscript.js\"></script><!--load dropdown script--><script type=\"text/javascript\" src=\"relatedmenuscript.js\"></script><!--load related script--></body></html>";
            //lbl_test.Text = page_contents;
            return page_contents;

        }

        public string developMainSection(string text)
        {
            String content = "";//holds the content
                                //obtain first choice since it is the header
            List<String> choice_block_contents = getStringsMatchingThePattern(text, choiceblock);
            if (choice_block_contents.Count == 0)
            {
                throw new ArgumentException("No valid Choise Block was found");//prevented from occuring by pattern
            }
            content += developChoicesSection(choice_block_contents[0]);//the first block 
            List<String> related_block_contents = getStringsMatchingThePattern(text, relatedblock);//obtain related section
            if (related_block_contents.Count == 0)
            {
                throw new ArgumentException("No valid Related Block was found");//prevented from occuring by pattern
            }
            foreach (String related_block_content in related_block_contents)
            {
                content += developRelatedSection(related_block_content);
            }
            return content;

        }

        public string developRelatedSection(String text)
        {
            String content = "<div class=\"related\" >" +
                        "<p> You might also want:</p>" +
                           "<div class=\"name\">";
            List<String> choice_block_contents = getStringsMatchingThePattern(text, choiceblock);
            for (int i = 0; i < choice_block_contents.Count; i++)
            {
                content += developChoicesSection(choice_block_contents[i]);
            }
            content += "</div>" +
                    "</div>";
            return content;
        }

        public string developChoicesSection(string text)
        {
            String content = "";

            List<String> namesandvalues = getStringsMatchingThePattern(text, nameandvalue);//obtain related section
            if (namesandvalues.Count == 0)
            {
                throw new ArgumentException("No valid Name Block was found"); 
            }
            String choicecheckboxes = "";//hold choice checkboxes developed
            for (int i = 0; i < namesandvalues.Count; i++)
            {
                //first name will be used to make check box the others are for choices
                if (i == 0)
                {
                    //create check box and its label
                    content += createCheckBoxAndLabel(namesandvalues[i], "dropdown");
                }
                else
                {
                    //create choise check boxes
                    choicecheckboxes += createCheckBoxAndLabel(namesandvalues[i], "choice");
                }
            }
            content += "<div class=\"choices\" >" +
                        "<div class=\"name\" >" +
                        choicecheckboxes +
                        "</div>" +
                    "</div > ";
            //lbl_test.Text = content;
            return content;
        }

        public String createCheckBoxAndLabel(string text, string type)
        {
            string checkbox_content = "";
            string quote = "'";
            char[] charArr = quote.ToCharArray();
            string name = text.Split(charArr[0], charArr[0])[1];
            //create check box and add name to the ids

            string unique_id = getUniqueID(name);
            checkbox_content = "<input type =\"checkbox\" id =\"checkbox-toggle-" + unique_id + "\" class=\"" + type + "-chk\">" +
                            "<label for=\"checkbox-toggle-" + unique_id + "\">" + name + "</label>";
            checkbox_IDs.Add(unique_id);//adding id to the list
            return checkbox_content;
        }

        public string getUniqueID(string name)
        {
            if (!checkbox_IDs.Contains(name))
            {
                return name;
            }
            else
            {
                //the id exists so add 1 to make unique then call the method again
                name += "1";
                getUniqueID(name);
            }
            return name;

        }

        public List<String> getStringsMatchingThePattern(string text, string regularexp)
        {
            List<string> matches = new List<string>();//list for storing the matches
            MatchCollection mathcollection = Regex.Matches(text.Trim(), regularexp);//
            foreach (Match match in mathcollection) //for every match in path collection
            {
                matches.Add(match.ToString());//add the match to the list
            }
            return matches;//return values of the list
        }


        public class NameChoices//holds a name and its choices
        {
            String name;
            List<String> choices;
            NameChoices()
            {
                name = null;
                choices = new List<String>();
            }
            public void setName(string name)
            {
                this.name = name;
            }

            public void addChoice(String choice)
            {
                choices.Add(choice);
            }
        }
    }
}