#!/usr/bin/env bash

wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update; \
sudo apt-get install -y apt-transport-https && \
sudo apt-get update && \
sudo apt-get install -y aspnetcore-runtime-5.0 && \
sudo apt-get install -y dotnet-sdk-5.0 && \
cd eBankingTests

FILE=chromedriver
if [ ! -f "$FILE" ]; then
    echo "$FILE does not exist."
    wget -O chromedriver_linux64.zip https://chromedriver.storage.googleapis.com/78.0.3904.105/chromedriver_linux64.zip && \
    unzip -o chromedriver_linux64.zip && \
    chmod 774 chromedriver
    rm chromedriver_linux64.zip
else 
    echo "$FILE exists, skipping download."
fi
