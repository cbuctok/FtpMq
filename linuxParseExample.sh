#!/bin/bash
for filename in *.json; do
         ARGS=$(cat $filename | jq -r '.Arguments[]')
         PROGRAM=$(cat $filename | jq -r '.Program')
         $PROGRAM $ARGS
         mkdir -p old
         mv $filename old
done
