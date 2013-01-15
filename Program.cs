using System;
using Nancy.Hosting.Self;
using Topshelf;

namespace Nancy.Demo.Hosting.TopShelf
{
    public class NancySelfHost
    {
        private NancyHost _nancyHost;

        public void Start() {
            _nancyHost = new NancyHost(new Uri("http://localhost:8888/nancy/"));
            _nancyHost.Start();
            Console.WriteLine("Nancy now listening - http://localhost:8888/nancy/. Press ctrl-c to stop");
        }

        public void Stop()
        {
            _nancyHost.Stop();
            Console.WriteLine("Stopped. Good bye!");
        }
    }

    public class Program
    {
        public static void Main()
        {
            HostFactory.Run(x =>                                    //1
            {
                x.Service<NancySelfHost>(s =>                       //2
                {
                    s.ConstructUsing(name => new NancySelfHost());  //3
                    s.WhenStarted(tc => tc.Start());                //4
                    s.WhenStopped(tc => tc.Stop());                 //5
                });

                x.UseLog4Net();
                x.RunAsLocalSystem();                               //6
                x.SetDescription("Sample Topshelf Host");           //7
                x.SetDisplayName("Stuff");                          //8
                x.SetServiceName("stuff");                          //9
              
            });                                                     //10
        }
    }
}
