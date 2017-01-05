using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            SingleResponsibility sr = new SingleResponsibility();
            sr.demo();
        }

        /*
            public class B {}
            public class A{
                private B _b;
                public B B {
            get {
            if(_b == null){
                    // iniate b
                }  else{
                return _b
                }      
        }  
                 
                
            }
         */
    }
}
