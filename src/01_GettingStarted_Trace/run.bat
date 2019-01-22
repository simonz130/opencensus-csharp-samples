@echo off

echo run zipkin in docker
docker run -d -p 9411:9411 openzipkin/zipkin

echo run the sample
dotnet 01_GettingStarted_Trace.dll