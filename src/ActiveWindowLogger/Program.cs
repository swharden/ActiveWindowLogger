using ActiveWindowLogger;

StateMonitor monitor = new();
monitor.LineLogged += (s, e) => Console.Write(e);
monitor.StartBlocking();