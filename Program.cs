using System;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Mail;


namespace ConsoleApp28
{


    class MyEvent
    {


        public void CreateFileA(object sender)

        {


            FileStream File1 = new FileStream("a.doc", FileMode.Create);
            StreamWriter sw = new StreamWriter(File1);
            Console.WriteLine("Please, type the first line of the letter:");
            string FirstLine = Console.ReadLine();
            Console.WriteLine("Please, type the main body:");
            string SecondLine = Console.ReadLine();
            sw.WriteLine(FirstLine);
            sw.WriteLine(SecondLine);
            sw.Close();
            File1.Close();


        }

        public void CorrectFileA()
        {
            string line;
            char[] separator = { ' ', '!' };
            List<string> words;
            words = new List<string>();
            using (StreamReader MyFile = new StreamReader("a.doc"))
            {
                while ((line = MyFile.ReadLine()) != null)
                {
                    Console.WriteLine(line);


                    string[] words2 = line.Split(separator);
                    foreach (string words3 in words2)
                    {
                        words.Add(words3);
                    }
                }
                MyFile.Close();

                int l = words.IndexOf("Hi");
                Console.WriteLine("Please, type receiver name:");
                string name = Convert.ToString(Console.ReadLine());



                words.Insert((l + 1), name + '!');
                Console.WriteLine("Please, type the sender's name:");
                string name1 = Convert.ToString(Console.ReadLine());

                words.Add('!' + name1);




                FileStream File2 = new FileStream("b.doc", FileMode.Create);
                StreamWriter sw1 = new StreamWriter(File2);
                foreach (string words3 in words)
                {
                    sw1.Write(words3 + " ");
                }
                sw1.Close();
                File2.Close();

            }
        }

        public void MailSender()
        {
            Console.WriteLine("Please, Type the email address:");
            string email = Console.ReadLine();
            Console.WriteLine("Please, type the sender name");
            string name = Console.ReadLine();
            Console.WriteLine("Please, type the receiver address:");
            string receiver = Console.ReadLine();
            Console.WriteLine("Please, type the password to the email box:");
            string password = Console.ReadLine();

            MailAddress from = new MailAddress(email, name);
            MailAddress to = new MailAddress(receiver);
            MailMessage m = new MailMessage(from, to);
            m.Attachments.Add(new Attachment("b.doc"));
            m.Subject = "Don't replay";
            m.Body = "<h2>Don't replay on this email. Email was created automatically</h2>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(email, password);
            smtp.EnableSsl = true;
            smtp.Send(m);

        }

    }
    class Program
    {
        static void Main()
        {
            MyEvent evt = new MyEvent();
            Console.WriteLine("Hi, I am Evulet, I will help you to create and send emails. Do you want to send email? Y/N");
            char request = Convert.ToChar(Console.ReadLine());
            if ((request != 0) && (request == 'Y'))
            {

                evt.CreateFileA(request);
                evt.CorrectFileA();
                evt.MailSender();
                Console.WriteLine($"Email was sent ");

            }
            else
            {
                Console.WriteLine("Apologise, this program cannot help to you in your request. Please, follow the instruction");
            }







        }


    }

}
