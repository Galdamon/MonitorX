# MonitorX
This command line utility expects three arguments: a process name, its maximum lifetime (in minutes) and a monitoring frequency (in minutes, as well). When you run the program, it starts monitoring processes with the frequency specified. If a process of interest lives longer than the allowed duration, the utility kills the process and adds the corresponding record to the log.

Logs are created in the same directory as the application is run from.
Application logs its start with arguments and each process it kills. All with timestamps.
