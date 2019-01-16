
docker pull mcr.microsoft.com/mssql/server:2017-latest

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Bank_666" --name "sql2" -p 1401:1433 -v sql1data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest
   
docker exec -it sql2 mkdir /var/opt/mssql/backup


docker cp BankContex.bak sql2:/var/opt/mssql/backup

docker exec -it sql2 /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Bank_666" -Q "RESTORE FILELISTONLY FROM DISK = '/var/opt/mssql/backup/BankContext.bak'"
   
docker exec -it sql2 /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Bank_666" -Q "RESTORE DATABASE BankContext FROM DISK = '/var/opt/mssql/backup/BankContext.bak' WITH MOVE 'BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298' TO '/var/opt/mssql/data/BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298.mdf', MOVE 'BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298_log' TO '/var/opt/mssql/data/BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298.ldf'"


docker cp BankContext.sql sql2:/var/opt/mssql/backup

docker exec -it sql2 /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Bank_666" -i /var/opt/mssql/backup/BankContext.sql

docker exec -it sql2 /opt/mssql-tools/bin/sqlcmd  -S localhost -U SA -P "Bank_666"  -Q "SELECT * FROM Payment"