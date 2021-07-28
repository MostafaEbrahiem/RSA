using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RSA_project
{
    class big_integer
    {

       public struct divres
        {
            public string q;
            public string r;
        };
            

        



        //complexity based on worst senario

        public string read_file(string[]read_str,char Tp)
        {
            string f_res, num1, num2;//
            int j;//
                                  //Θ(1)
            j = 1;//
            List<string> res = new List<string>();//
            for (int i = 0; i < Convert.ToInt32(read_str[0]); i++)// body order(O(N^1.6)+N+N)=O(N^1.6) ||forloop order(N)||N*N^1.6=O(N^2.6)
            {
                Stopwatch time = new Stopwatch();//Θ(1)
                time.Start();//
                j++;//Θ(1)
                num1 = read_str[j];//Θ(1)
                j++;//Θ(1)
                num2 = read_str[j];//Θ(1)
                if (Tp=='M')// O(N^1.6) 
                {
                    res.Add(findmul(num1, num2));//// O(N^1.6) 
                }
                else if(Tp=='S')
                {
                    res.Add(findSum(num1, num2));//O(N)
                }
                else
                {
                    res.Add(findDiff(num1, num2));//O(N)
                }
               
                res.Add(""); //Θ(1)
                j++; //Θ(1)
                time.Stop(); //Θ(1)
                Console.WriteLine(time.Elapsed); //Θ(1)
            }           

            f_res =string.Join(Environment.NewLine, res);//O(N)
            f_res = f_res.Substring(0, f_res.Length - 1);//O(N)

            return f_res; //Θ(1)
        }


        public bool isSmaller(string str1, string str2)     //O(N)  or  Ω(1)//
        {
           
            int n1 = str1.Length, n2 = str2.Length;            //
            if (n1 < n2)                                       //
                return true;                                   //  Θ(1)
            if (n2 < n1)                                        // 
                return false;                                    //

            for (int i = 0; i < n1; i++)                   // O(n1)    Ω(1)
                if (str1[i] < str2[i])                   // Θ(1)
                    return true;                            //Θ(1)
                else if (str1[i] > str2[i])                  //Θ(1)
                    return false;                            // Θ(1)

            return false;                                     //Θ(1)            
            
        }

        public string findDiff(string str1, string str2)               //f(n)=N+N+N+1....||f(n)=9N||then O(N)// 
        {
          
            
            bool check = false;                       //Θ(1) 
            if (isSmaller(str1, str2))                       //O(N) 
            {
                string t = str1;                               //Θ(1) 
                str1 = str2;                              //Θ(1) 
                str2 = t;                                  //Θ(1) 
                check = true;                                   //Θ(1)   
            }

            string str = "";                    //Θ(1) 
 
            int n1 = str1.Length, n2 = str2.Length;                      //Θ(1)         

            char[] ch1 = str1.ToCharArray();                    //Θ(n1)
            Array.Reverse(ch1);                    //Θ(n1)
            str1 = new string(ch1);                 // O(1)
            char[] ch2 = str2.ToCharArray();             // Θ(n2)
            Array.Reverse(ch2);                        // Θ(n2)
            str2 = new string(ch2);                   //  O(1)

            int carry = 0;                            ////Θ(1)

            for (int i = 0; i < n2; i++)                   //Θ(n2)    //
            {

                int sub = ((int)(str1[i] - '0') -
                        (int)(str2[i] - '0') - carry);       //Θ(1)

                if (sub < 0)                //Θ(1)
                {
                    sub = sub + 10;                   //Θ(1) 
                    carry = 1;                        //Θ(1)
                }
                else
                    carry = 0;                     //Θ(1) 

                str += (char)(sub + '0');                 //Θ(1)
            }


            for (int i = n2; i < n1; i++)                  ////Θ(n2)   //  
            {
                int sub = ((int)(str1[i] - '0') - carry);            //Θ(1)

                if (sub < 0)                //Θ(1)
                {
                    sub = sub + 10;                 //Θ(1)
                    carry = 1;                       //Θ(1)
                }
                else  
                    carry = 0;                       //Θ(1)

                str += (char)(sub + '0');                 //Θ(1)
            }

            char[] ch3 = str.ToCharArray();                  //O(N)
            Array.Reverse(ch3);                              //O(N)

            string ss = new string(ch3);                     //Θ(1)
            ss = deletezero(ss);                              //O(N)
            if (check)                                        //Θ(1)
                ss = '-' + ss;                                //Θ(1)
            // int c1 = System.Environment.TickCount;
            //Console.WriteLine(c1 - c);
           
            return ss;                                        //Θ(1)
            

        }


        public string findSum(string str1, string str2)             //f(n)=N+N+N+1....||f(n)=7N||then O(N)//
        {
            bool check1 = false;                  //Θ(1) 
            if (str1[0] == '-' && str2[0] == '-')             //Θ(1)
            {
                
                str1 = str1.Substring(1, str1.Length - 1);           //Θ(1)   
                str2 = str2.Substring(1, str2.Length - 1);          //Θ(1)
                check1 = true;                                 //Θ(1)
            } 
            else if (str1[0] == '-')//O(N+N)=O(N)
            {
                str1 = str1.Substring(1, str1.Length - 1);   //Θ(N)
                return findDiff(str2, str1);                //O(N)  

            }
            else if (str2[0] == '-')//O(N+N)=O(N)
            {
                str2 = str2.Substring(1, str2.Length - 1);         //O(N)
                return findDiff(str1, str2);                //   O(N)
            }





            if (str1.Length > str2.Length)         //Θ(1)//
            {
                string t = str1;                 //Θ(1)
                str1 = str2;                     //Θ(1)
                str2 = t;                        //Θ(1)
            }
            string str = "";    //Θ(1)
            int n1 = str1.Length, n2 = str2.Length;         //Θ(1)


            char[] ch = str1.ToCharArray();          // O(n1)
            Array.Reverse(ch);                       // O(n1)
            str1 = new string(ch);                   //Θ(1)
            char[] ch1 = str2.ToCharArray();         //O(n2)
            Array.Reverse(ch1);                      //O(n2)
            str2 = new string(ch1);                  //Θ(1)

            int carry = 0;                            //Θ(1)
            for (int i = 0; i < n1; i++)              //Θ(n1)   // 
            {

                int sum = ((int)(str1[i] - '0') +
                        (int)(str2[i] - '0') + carry);           //Θ(1)
                str += (char)(sum % 10 + '0');                   //Θ(1)        


                carry = sum / 10;                               //Θ(1)
            }

            for (int i = n1; i < n2; i++)                       //Θ(n2-n1):-O(N)//
            {
                int sum = ((int)(str2[i] - '0') + carry);            //Θ(1)
                str += (char)(sum % 10 + '0');                      //Θ(1)
                carry = sum / 10;                          //Θ(1)
            }

            if (carry > 0)                                //Θ(1)
                str += (char)(carry + '0');               //Θ(1)

            char[] ch2 = str.ToCharArray();                 //Θ(n2)
            Array.Reverse(ch2);                             //Θ(n2)
            str = new string(ch2);                           //Θ(1)
            if (check1)            //Θ(1)//
                str = '-' + str;                  //Θ(1)
            return str;
        }
       public  static string deletezero(string str)           //O(N)       
        {
            int count = 0;                     //Θ(1)
            while (count < str.Length - 1)             //O(N-1):-O(N)
            {
                if (str[count] == '0')            //Θ(1)//
                    count++;               //Θ(1)
                else
                    break; 

            }
            string res = str.Substring(count, str.Length - count);       //O(N)
            return res;        //Θ(1)

        }

        public string findmul(string str1, string str2)      //t(N)=3t(N/2)+15N ||using master:- F(N)=N||N^log a=N^log3base2=N^1.6 ||then case 1 ||then n^1.6 is the complexity
        {
            ////////////////////////////////// check for negative sign
            bool check1 = false;                    //Θ(1)
            if (str1[0] == '-' && str2[0] == '-')             //O(N)
            {
                str1 = str1.Substring(1, str1.Length - 1);         //O(N)
                str2 = str2.Substring(1, str2.Length - 1);         //O(N)

            }
            else if (str1[0] == '-') //Θ(N)
            {
                str1 = str1.Substring(1, str1.Length - 1);              //Θ(N-1):-Θ(N)
                check1 = true;                          //Θ(1)
            }
            else if (str2[0] == '-')                 //Θ(N)
            {
                str2 = str2.Substring(1, str2.Length - 1);                   //Θ(N)
                check1 = true;                        //Θ(1)
            }
            /////////////////////////////////////////////


            int l1 = str1.Length;                   //Θ(1)
            int l2 = str2.Length;                   //Θ(1)
            int length = l1;                        //Θ(1)

            //////////////////////////////////////////////fill the smaller string with zeros and get the bigger string length
            if (l1 < l2)                         //O(N)//
            {
                for (int i = 0; i < l2 - l1; i++)              //O(N)//
                    str1 = '0' + str1;                         //Θ(1)
                length = l2;                                   //Θ(1)
            }
            else if (l1 > l2)                         //O(N)
            {
                for (int i = 0; i < l1 - l2; i++)      //O(N)
                    str2 = '0' + str2;               //Θ(1)

            }
            /////////////////////////////////////////////////////////

            //////////////////////////////////////////////base case
            if (length == 1) //Θ(1)
            {
                int num1 = Int32.Parse(str1);          //Θ(1)
                int num2 = Int32.Parse(str2);             //Θ(1)
                int num = num1 * num2;                //Θ(1)
                return num.ToString();               //Θ(1)
            }
            //////////////////////////////////////////////

            int len1 = length / 2;            //Θ(1)
            int len2 = (length - len1);            //Θ(1)

            //////////////////divide str1 and str2 into 2 substring
            string a, b, c, d, m1, m2, z;

            b = str1.Substring(0, len1);             //
            a = str1.Substring(len1, len2);          //all O(N/2) :-O(N)
            d = str2.Substring(0, len1);             //
            c = str2.Substring(len1, len2);          //

            ///////////////////// Karatsuba algorithm
            string w, sum1, sum2;           //Θ(1)
            m1 = findmul(a, c);             // O(N^1.6)     
            m2 = findmul(b, d);             // O(N^1.6)     
            sum1 = findSum(a, b);            //O(N)    
            sum2 = findSum(c, d);            //O(N)
            z = findmul(sum1, sum2);         // O(N^1.6)      
            z = findDiff(z, m1);               //O(N)                
            w = findDiff(z, m2);               //O(N)                      
            for (int i = 0; i < 2 * (length - (length / 2)); i++)
                m2 = m2 + "0";         //Θ(1)


            for (int i = 0; i < length - (length / 2); i++)
                w = w + "0";           //Θ(1)

            string res;                //Θ(1)
            res = findSum(m2, w);             //O(N)
            res = findSum(res, m1);           //O(N)
            /////////////////////////////////////////////////////////

            res = deletezero(res);            //O(N)
            if (check1)                       //Θ(1)
                res = '-' + res;                 //Θ(1)
            return res;                       //Θ(1)

        }

        public divres s = new divres();

        public divres finddiv(string str1, string str2)
        {
            
            ////////////base case
            if(isSmaller(str1,str2))
            {
                s.q = "0";
                s.r = str1;
                return(s);
            }
            s = finddiv(str1,findSum(str2,str2));
            s.q = findSum(s.q,s.q);
            if(isSmaller(s.r,str2))
            {
                return (s);
            }
            else
            {
                s.q = findSum(s.q,"1");
                s.r = findDiff(s.r, str2);

                return (s);
            }
          
            ////////////





        }




    }
}
