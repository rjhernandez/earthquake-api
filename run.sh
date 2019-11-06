#!/bin/sh
docker build . -t earthquake
docker run --name earthquake-api --rm -it -p 8000:80 earthquake
