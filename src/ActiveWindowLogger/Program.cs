using ActiveWindowLogger;

StateMonitor monitor = new("./activity-logs");
monitor.LineLogged += (s, e) => Console.Write(e);
monitor.RunForever();