REM Curl test

curl -X GET "http://localhost:5105/sqltests/roster/all" -H "accept: */*"

pause

curl -X GET "https://YOURSITE.azurewebsites.net/sqltests/roster/all" -H "accept: */*"

pause
