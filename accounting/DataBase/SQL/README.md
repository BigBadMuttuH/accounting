#### PostgreSQL zip install

- Download postgresql from https://www.enterprisedb.com/download-postgresql-binaries;
- Unzip
- Init DataBase
```PowerShell
initdb.exe -U postgres -A password -W -E UTF-8 -D "E:\pgSQL\Data\"
```
 - Run/stop server
 ```PowerShell
 pg_ctl -D "E:\pgSQL\data" -l "E:\pgSQL\data\log.txt" start
 pg_ctl -D "E:\pgSQL\data" -l "E:\pgSQL\data\log.txt" stop
 ```

 - Russian code page
 ```SQL
postgres=# \! chcp 125
```
or
```SQL
set client_encoding='win1251';
 ```
