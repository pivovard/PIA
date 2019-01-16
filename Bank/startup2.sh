#!/bin/bash



echo "+++++++++++++++++++++++++"
echo "Starting SQL server"
echo "+++++++++++++++++++++++++"

docker pull mcr.microsoft.com/mssql/server:2017-latest

docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=Bank_666' --name 'sql1' -p 1401:1433 -v sql1data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest

docker exec -it sql1 mkdir /var/opt/mssql/backup

docker cp BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298.bak sql1:/var/opt/mssql/backup

docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd -S localhost \
   -U SA -P 'Bank_666' \
   -Q 'RESTORE FILELISTONLY FROM DISK = "/var/opt/mssql/backup/BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298.bak"' \
   | tr -s ' ' | cut -d ' ' -f 1-2
   
docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U SA -P 'Bank_666' \
   -Q 'RESTORE DATABASE [BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298] FROM DISK = "/var/opt/mssql/backup/BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298.bak" WITH MOVE "BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298" TO "/var/opt/mssql/data/BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298.mdf", MOVE "BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298_log" TO "/var/opt/mssql/data/BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298.ldf"'



echo "+++++++++++++++++++++++++"
echo "SQL server running"
echo "+++++++++++++++++++++++++"







echo "+++++++++++++++++++++++++"
echo "Starting bank application"
echo "+++++++++++++++++++++++++"

docker build -t bank  .
docker run -it --rm -p "80:80" bank