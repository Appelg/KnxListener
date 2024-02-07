# KnxListener

You need to install .net 8 runtime:
https://dotnet.microsoft.com/en-us/download/dotnet/8.0

## Just listen:
```
cd KnxListener
dotnet run 192.168.42.232
```

## Also send a 1bit value to an group address (to trigger messages sent back on bus, e.g status of a device)
```
cd KnxListener
dotnet run 192.168.42.232 0/0/1 1
```