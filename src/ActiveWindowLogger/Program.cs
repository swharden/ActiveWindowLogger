using ActiveWindowLogger;

Logger monitor = new();
monitor.LineLogged += (s, e) => Console.Write(e);
monitor.StartBlocking();